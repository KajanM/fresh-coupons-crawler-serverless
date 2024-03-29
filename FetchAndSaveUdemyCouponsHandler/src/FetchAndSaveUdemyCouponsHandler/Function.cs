using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        // TODO: get from parameter store instead
        private const string GitHubPathSegment = "udemy/v2";
        public static HttpClient UdemyHttpClient;
        public static IErrorReportingService ErrorReportingService;
        
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
                
                var configResult = await InitializeParameterStoreValuesAsync();
                    
                if (!configResult.IsSuccess)
                    throw new ApplicationException(
                        $"Unable to initialize config values from the parameter store.{Environment.NewLine}ConfigResult: {configResult.ToJson()}");

                UdemyHttpClient = GetUdemyHttpClient(configResult.Config);
                ErrorReportingService = new GoogleFormErrorReportingService(configResult.Config, httpClient);
                
                var lastCrawledData = await InitializeLastCrawledDataAsync(configResult.Config[ConfigurationKeys.Owner], configResult.Config[ConfigurationKeys.Repository], configResult.Config[ConfigurationKeys.Branch], httpClient);

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
                        await GetCourseDetailsAndCouponValidityAsync(coupon,lastCrawledData, httpClient, browsingContext);
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
                await LoggerUtils.ErrorAsync("an error occured while executing the lambda", e);
            }

            return null;
        }

        public static HttpClient GetUdemyHttpClient(Dictionary<string, string> parameterStore)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", parameterStore[ConfigurationKeys.UdemyCredentials]);
            return client;
        }

        private static async Task<Dictionary<string, CourseDetailsViewModel>> InitializeLastCrawledDataAsync(string repoOwner, string repoName, string branch, HttpClient httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(repoOwner)) throw new ArgumentNullException(nameof(repoOwner));
            if (string.IsNullOrWhiteSpace(repoName)) throw new ArgumentNullException(nameof(repoName));
            if (string.IsNullOrWhiteSpace(branch)) throw new ArgumentNullException(nameof(branch));
            
            httpClient ??= new HttpClient();
            var result = new Dictionary<string, CourseDetailsViewModel>();

            var metaFileUrl =
                $"https://raw.githubusercontent.com/{repoOwner}/{repoName}/{branch}/{GitHubPathSegment}/meta.json";
            var getLastCrawledMetadataResult = await HttpHelper.GetAsync<MetaFile>(
                metaFileUrl,
                httpClient);

            if (!getLastCrawledMetadataResult.IsSuccess)
            {
                LoggerUtils.Warn(
                    $"Unable to resolve content from {metaFileUrl}.{Environment.NewLine}{nameof(getLastCrawledMetadataResult)}: {getLastCrawledMetadataResult.ToJson()}");
                return result;
            }

            var lastCrawledCourseDataUrl =
                $"https://raw.githubusercontent.com/{repoOwner}/{repoName}/{branch}/{GitHubPathSegment}/{getLastCrawledMetadataResult.Data.LastSynced}.json";

            var getLastCrawledCourseDataResult = await HttpHelper.GetAsync<CourseDetailsFile>(
                lastCrawledCourseDataUrl,
                httpClient);

            if (!getLastCrawledCourseDataResult.IsSuccess)
            {
                LoggerUtils.Warn(
                    $"Unable to resolve content from {lastCrawledCourseDataUrl}.{Environment.NewLine}{nameof(getLastCrawledCourseDataResult)}: {getLastCrawledCourseDataResult.ToJson()}");
                return result;
            }

            foreach (
                var (key, value) in getLastCrawledCourseDataResult.Data.CoursesWithCoupon)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, value.CourseDetails);
                }
                else
                {
                    LoggerUtils.Warn($"Found duplicate key from the last crawled course data file for key {key}");
                }
            }

            foreach (
                var (key, value) in getLastCrawledCourseDataResult.Data.FreeCourses)
            {
                if (!result.ContainsKey(key))
                {
                    result.Add(key, value.CourseDetails);
                }
                else
                {
                    LoggerUtils.Warn($"Found duplicate key from the last crawled course data file for key {key}");
                }
            }

            return result;
        }

        private static async Task<ParameterStoreConfigurationService.GetConfigResult> InitializeParameterStoreValuesAsync()
        {
            var configurationService = new ParameterStoreConfigurationService(RegionEndpoint.APSouth1);
            return await configurationService.GetAsync(
                ConfigurationKeys.Branch,
                ConfigurationKeys.Owner,
                ConfigurationKeys.Repository, 
                ConfigurationKeys.Token, 
                ConfigurationKeys.UdemyCredentials, 
                
                ConfigurationKeys.GoogleFormErrorReportingMessageKey,
                ConfigurationKeys.GoogleFormErrorReportingTimestampKey,
                ConfigurationKeys.GoogleFormErrorReportingUrlKey,
                ConfigurationKeys.GoogleFormErrorReportingSubmitUrl
                );
        }

        private static async Task SaveToRepositoryAsync(List<CourseDetailsWithCouponViewModel> coursesWithCoupon,
            List<CourseDetailsWithCouponViewModel> freeCourses)
        {
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
                        .DistinctBy(c => c.CourseDetails.CourseUri)
                        .OrderByDescending(c => c.CouponData.DiscountPercentage)
                        .ToDictionary(c => c.CourseDetails.CourseUri, c => c),
                    FreeCourses = freeCourses.ToDictionary(c => c.CourseDetails.CourseUri, c => c)
                }.ToJson();

                var now = DateTime.Now.ToString("yyyy-MM-dd-HH-ss");
                var createFileResult =
                    await githubService.CreateFileAsync($"{GitHubPathSegment}/{now}.json", jsonContent, $"add {now}.json");
                if (!createFileResult.IsSuccess)
                {
                    await LoggerUtils.ErrorAsync($"unable to create {now}.json in GitHub. {createFileResult.GetFormattedError()}");
                    return;
                }

                var meta = new MetaFile
                {
                    LastSynced = now
                };
                var updateOrCreateFileResult =
                    await githubService.UpdateOrCreateFileAsync($"{GitHubPathSegment}/meta.json", meta.ToJson(), $"added new contents {now}.json");
                if (!updateOrCreateFileResult.IsSuccess)
                {
                    await LoggerUtils.ErrorAsync(
                        $"an error occured while updating the meta file {updateOrCreateFileResult.GetFormattedError()}");
                }
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync("an error occured while saving parsed result in the GitHub repo", e);
            }
        }

        private static async Task<CourseDetailsWithCouponViewModel> GetCourseDetailsAndCouponValidityAsync(
            UdemyUrlWithCouponCode coupon, Dictionary<string, CourseDetailsViewModel> lastCrawledData, HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            try
            {
                CourseDetailsViewModel courseDetails;
                var urlWithEndingBackSlash = coupon.Url.EndsWith("/") ? coupon.Url : $"{coupon.Url}/";
                if (lastCrawledData.ContainsKey(urlWithEndingBackSlash))
                {
                    courseDetails = lastCrawledData[urlWithEndingBackSlash];
                    LoggerUtils.Info($"Course details for {urlWithEndingBackSlash} initialized from last crawled data");
                }
                else
                {
                    var getCourseDetailsResult =
                        await UdemyHelper.GetCourseDetailsAsync(coupon.Url, coupon.IsAlreadyAFreeCourse, httpClient,
                            browsingContext);
                    if (!getCourseDetailsResult.IsSuccess) return null;
                    courseDetails = getCourseDetailsResult.CourseDetails;
                }

                if (coupon.IsAlreadyAFreeCourse)
                {
                    return new CourseDetailsWithCouponViewModel
                    {
                        CourseDetails = courseDetails,
                        IsAlreadyAFreeCourse = true,
                    };
                }

                if (string.IsNullOrWhiteSpace(coupon.CouponCode))
                {
                    await LoggerUtils.ErrorAsync($"coupon code is empty for a premium course {coupon.Url}.");
                    return null;
                }

                LoggerUtils.Info($"checking coupon validity from Udemy for {coupon.Url}");
                var isCouponValidResult = await UdemyHelper.IsCouponValid(
                    courseDetails.CourseId,
                    coupon.CouponCode, httpClient);
                if (!isCouponValidResult.IsSuccess)
                {
                    await LoggerUtils.ErrorAsync($"an error occured while checking coupon validity for {coupon.Url}");
                    return null;
                }

                LoggerUtils.Info($"successfully parsed coupon validity from Udemy for {coupon.Url}");

                return new CourseDetailsWithCouponViewModel
                {
                    CourseDetails = courseDetails,
                    CouponData = isCouponValidResult.CouponData
                };
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync(
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
                    await LoggerUtils.ErrorAsync($"an error occured while getting udemy coupon link from {provider.Name}", e);
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
            public const string UdemyCredentials = "fc.udemyCredentials";

            public const string GoogleFormErrorReportingSubmitUrl = "gf.error.submiturl";
            public const string GoogleFormErrorReportingTimestampKey = "gf.error.timestamp";
            public const string GoogleFormErrorReportingMessageKey = "gf.error.message";
            public const string GoogleFormErrorReportingUrlKey = "gf.error.url";
        }
    }
}