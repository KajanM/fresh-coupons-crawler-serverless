using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using AngleSharp;
using FetchAndSaveUdemyCouponsHandler.DiscUdemy.Helpers;
using FetchAndSaveUdemyCouponsHandler.Services.DiscUdemy;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
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

                var couponProviders = new List<IUdemyCouponProviderService>
                {
                    new DiscUdemyCouponProviderService()
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
                    var courseDetails = await GetCourseDetailsAndCouponValidityAsync(coupon, httpClient, browsingContext);
                    if (courseDetails == null) continue;
                    coursesWithCoupon.Add(courseDetails);
                }

                #endregion

                return coursesWithCoupon;
            }
            catch (Exception e)
            {
                LoggerUtils.Error("an error occured while executing the lambda", e);
            }

            return null;
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
    }
}