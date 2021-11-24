using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using AngleSharp;
using FetchAndSaveUdemyCouponsHandler.Config;
using FetchAndSaveUdemyCouponsHandler.DataStore;
using FetchAndSaveUdemyCouponsHandler.RealDiscount.Services;
using FetchAndSaveUdemyCouponsHandler.Services.DiscUdemy;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Extensions;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;
using FetchAndSaveUdemyCouponsHandler.Shared.Services;
using FetchAndSaveUdemyCouponsHandler.Shared.ViewModels;
using FetchAndSaveUdemyCouponsHandler.Udemy.Helpers;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace FetchAndSaveUdemyCouponsHandler
{
    public class Function
    {
        public async Task<List<CourseDetailsWithCouponViewModel>> FunctionHandler(FunctionArgs args,
            ILambdaContext context)
        {
            try
            {
                var httpClient = new HttpClient();
                var browsingContext = BrowsingContext.New(Configuration.Default);
                var coupons = new List<UdemyUrlWithCouponCode>();
                var coursesWithCoupon = new List<CourseDetailsWithCouponViewModel>();
                var freeCourses = new List<CourseDetailsWithCouponViewModel>();

                var couponProviders = new List<IUdemyCouponProviderService>
                {
                    new DiscUdemyCouponProviderService(),
                    new RealDiscountUdemyCouponProviderService()
                };

                #region Get Udemy course links with coupon code

                var tasks = couponProviders.Select(provider =>
                    CollectUdemyLinksWithCoupon(provider, args, httpClient, browsingContext));

                (await Task.WhenAll(tasks))
                    .Where(c => c != null && c.Any())
                    .ToList()
                    .ForEach(c => coupons.AddRange(c));

                #endregion

                #region Get course details and more context about the coupon

                foreach (var coupon in coupons.Distinct())
                {
                    var courseDetails =
                        await GetCourseDetailsAndCouponValidityAsync(coupon, httpClient, browsingContext);
                    if (courseDetails == null) continue;
                    if (courseDetails.IsAlreadyAFreeCourse)
                    {
                        freeCourses.Add(courseDetails);
                    }
                    else
                    {
                        coursesWithCoupon.Add(courseDetails);
                    }
                }

                #endregion

                await SaveToRepositoryAsync(coursesWithCoupon, freeCourses);

                return coursesWithCoupon;
            }
            catch (Exception e)
            {
                LoggerUtils.Error("an error occured while executing the lambda", e);
            }

            return null;
        }

        private static async Task SaveToRepositoryAsync(List<CourseDetailsWithCouponViewModel> coursesWithCoupon,
            List<CourseDetailsWithCouponViewModel> freeCourses)
        {
            const string pathSegment = "udemy/v2";
            try
            {
                var configurationService = new ParameterStoreConfigurationService(RegionEndpoint.APSouth1);
                var configResult = await configurationService.GetAsync(ConfigurationKeys.Branch,
                    ConfigurationKeys.Owner,
                    ConfigurationKeys.Repository, ConfigurationKeys.Token);

                if (!configResult.IsSuccess) return;

                IGithubService githubService = new GithubService(
                    configResult.Config[ConfigurationKeys.Owner],
                    configResult.Config[ConfigurationKeys.Repository],
                    configResult.Config[ConfigurationKeys.Branch],
                    configResult.Config[ConfigurationKeys.Token]
                );
                var jsonContent = new CourseDetailsFile
                {
                    CoursesWithCoupon = coursesWithCoupon
                        .OrderByDescending(c => c.CouponData.DiscountPercentage)
                        .ToDictionary(c => c.CourseDetails.CourseUri, c => c),
                    FreeCourses = freeCourses.ToDictionary(c => c.CourseDetails.CourseUri, c => c)
                }.ToJson();

                var now = DateTime.Now.ToString("yyyy-MM-dd-HH-ss");
                var createFileResult =
                    await githubService.CreateFileAsync($"{pathSegment}/{now}.json", jsonContent, $"add {now}.json");
                if (!createFileResult.IsSuccess)
                {
                    LoggerUtils.Error($"unable to create {now}.json in GitHub. {createFileResult.GetFormattedError()}");
                    return;
                }

                var meta = new MetaFile
                {
                    LastSynced = now
                };
                var updateOrCreateFileResult =
                    await githubService.UpdateOrCreateFileAsync($"{pathSegment}/meta.json", meta.ToJson(), $"added new contents {now}.json");
                if (!updateOrCreateFileResult.IsSuccess)
                {
                    LoggerUtils.Error(
                        $"an error occured while updating the meta file {updateOrCreateFileResult.GetFormattedError()}");
                }
            }
            catch (Exception e)
            {
                LoggerUtils.Error("an error occured while saving parsed result in the GitHub repo", e);
            }
        }

        private static async Task<CourseDetailsWithCouponViewModel> GetCourseDetailsAndCouponValidityAsync(
            UdemyUrlWithCouponCode coupon, HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            try
            {
                var getCourseDetailsResult =
                    await UdemyHelper.GetCourseDetailsAsync(coupon.Url, coupon.IsAlreadyAFreeCourse, httpClient,
                        browsingContext);
                if (!getCourseDetailsResult.IsSuccess) return null;

                if (coupon.IsAlreadyAFreeCourse)
                {
                    return new CourseDetailsWithCouponViewModel
                    {
                        CourseDetails = getCourseDetailsResult.CourseDetails,
                        IsAlreadyAFreeCourse = true,
                    };
                }

                if (string.IsNullOrWhiteSpace(coupon.CouponCode))
                {
                    LoggerUtils.Error($"coupon code is empty for a premium course {coupon.Url}.");
                    return null;
                }

                LoggerUtils.Info($"checking coupon validity from Udemy for {coupon.Url}");
                var isCouponValidResult = await UdemyHelper.IsCouponValid(
                    getCourseDetailsResult.CourseDetails.CourseId,
                    coupon.CouponCode, httpClient);
                if (!isCouponValidResult.IsSuccess)
                {
                    LoggerUtils.Error($"an error occured while checking coupon validity for {coupon.Url}");
                    return null;
                }

                LoggerUtils.Info($"successfully parsed coupon validity from Udemy for {coupon.Url}");

                return new CourseDetailsWithCouponViewModel
                {
                    CourseDetails = getCourseDetailsResult.CourseDetails,
                    CouponData = isCouponValidResult.CouponData
                };
            }
            catch (Exception e)
            {
                LoggerUtils.Error(
                    $"an error occured while resolving course details and coupon code validity {coupon}",
                    e);
            }

            return null;
        }

        private static async Task<List<UdemyUrlWithCouponCode>> CollectUdemyLinksWithCoupon(
            IUdemyCouponProviderService provider,
            FunctionArgs args,
            HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    return await provider.GetCouponDataAsync(args.StartPageNo,
                        args.NumberOfPagesToParse, httpClient,
                        browsingContext);
                }
                catch (Exception e)
                {
                    LoggerUtils.Error($"an error occured while getting udemy coupon link from {provider.Name}", e);
                }

                return null;
            });
        }

        public class FunctionArgs
        {
            public int NumberOfPagesToParse { get; set; } = 5;

            public int StartPageNo { get; set; } = 1;
        }

        public class MetaFile
        {
            public string LastSynced { get; set; }
        }
        
        public class CourseDetailsFile
        {
            public Dictionary<string, CourseDetailsWithCouponViewModel> CoursesWithCoupon { get; set; } 
            
            public Dictionary<string, CourseDetailsWithCouponViewModel> FreeCourses { get; set; } 
        }

        public static class ConfigurationKeys
        {
            public const string Branch = "fc.branch";
            public const string Owner = "fc.owner";
            public const string Repository = "fc.repository";
            public const string Token = "fc.token";
        }
    }
}