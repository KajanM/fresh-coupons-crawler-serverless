using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AngleSharp;
using AngleSharp.Dom;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;
using FetchAndSaveUdemyCouponsHandler.Shared.ViewModels;
using FetchAndSaveUdemyCouponsHandler.Udemy.BindingModels;
using CouponData = FetchAndSaveUdemyCouponsHandler.Shared.ViewModels.CouponData;

namespace FetchAndSaveUdemyCouponsHandler.Udemy.Helpers
{
    public static class UdemyHelper
    {
        public static async Task<GetCourseDetailsResult> GetCourseDetailsAsync(string udemyCourseDetailsPageUrl,
            bool isFreeCourse = false,
            HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            LoggerUtils.Info($"getting coursed details from Udemy for {udemyCourseDetailsPageUrl}");
            var result = new GetCourseDetailsResult();
            var getHttpResponseAsStringResult = await HttpHelper.GetStringAsync(udemyCourseDetailsPageUrl, httpClient);
            if (!getHttpResponseAsStringResult.IsSuccess)
            {
                LoggerUtils.Error(
                    $"an error occured while getting HTTP response from Udemy for {udemyCourseDetailsPageUrl}");
                result.AddError(getHttpResponseAsStringResult.GetFormattedError());
                return result;
            }

            LoggerUtils.Info($"parsing HTTP response from {udemyCourseDetailsPageUrl}");
            var parseCourseDetailsResult =
                await ParseCourseDetailsAsync(getHttpResponseAsStringResult.Response, isFreeCourse, browsingContext);
            if (!parseCourseDetailsResult.IsSuccess)
            {
                LoggerUtils.Error($"an error occured while parsing HTTP response from {udemyCourseDetailsPageUrl}");
                result.AddError(parseCourseDetailsResult.GetFormattedError());
                return result;
            }

            LoggerUtils.Info($"successfully parsed HTTP response from {udemyCourseDetailsPageUrl}");

            result.CourseDetails = parseCourseDetailsResult.CourseDetails;
            result.IsSuccess = true;

            return result;
        }

        public class GetCourseDetailsResult : BaseResult
        {
            public CourseDetailsViewModel CourseDetails { get; set; }
        }

        public static async Task<ParseCourseDetailsResult> ParseCourseDetailsAsync(string courseDetailsStr,
            bool isFreeCourse = false,
            IBrowsingContext browsingContext = null)
        {
            var result = new ParseCourseDetailsResult();

            try
            {
                browsingContext ??= BrowsingContext.New(Configuration.Default);
                var document = await browsingContext.OpenAsync(req => req.Content(courseDetailsStr));

                var courseContext =
                    JsonSerializer.Deserialize<List<CourseDetailsBindingModel>>(document
                        .QuerySelector(DomSelectors.MainCourseData).TextContent.Trim());

                var mainParsedData = courseContext.FirstOrDefault(e => e.Type == "Course");
                if (mainParsedData == null)
                {
                    result.AddError("Unable to parse main course details");
                    return result;
                }

                var courseDetails = new CourseDetailsViewModel
                {
                    Title = mainParsedData.Name,
                    ShortDescription = mainParsedData.Description,
                    CourseUri = mainParsedData.Id,
                    ImageUri = mainParsedData.Image,
                    TargetAudiences = mainParsedData.Audience.AudienceType,
                    Tags = courseContext.FirstOrDefault(e => e.Type == "BreadcrumbList")?.ItemListElement
                        .Select(e => e.Name).ToArray(),
                };
                result.CourseDetails = courseDetails;

                courseDetails.LongDescription = ParseCourseDescription(document, courseDetails.CourseUri)?.Description;
                courseDetails.EnrolledStudentsCount = ParseEnrolledStudentsCount(document, courseDetails.CourseUri);
                courseDetails.Language = ParseCourseLanguage(document, courseDetails.CourseUri);
                courseDetails.LastUpdated = ParseLastUpdated(document, courseDetails.CourseUri);
                courseDetails.Rating = ParseAggregateRating(mainParsedData, courseDetails.CourseUri);
                var (instructors, courseId) = ParseInstructorData(document, isFreeCourse, courseDetails.CourseUri);
                courseDetails.Instructors = instructors;

                if (!courseId.HasValue)
                {
                    LoggerUtils.Error($"unable to get course-id for {courseDetails.CourseUri}");
                    result.AddError("unable to parse course-id");
                    return result;
                }

                courseDetails.CourseId = courseId.Value;

                var apiResult = await ResolveDurationFromApiAsync(courseId.Value, Function.UdemyHttpClient, isFreeCourse);
                if (apiResult.IsSuccess)
                {
                    courseDetails.Duration = apiResult.Data.Duration;
                }
                else
                {
                    courseDetails.Duration = isFreeCourse ? "3600" : "1 hour";
                }

                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                result.AddError("An error occured while parsing udemy course details");
                result.AddError(e.Message);
                return result;
            }
        }

        private static string ParseLastUpdated(IDocument document, string courseUri = null)
        {
            var lastUpdated = document.QuerySelector(DomSelectors.LastUpdatedDate)?.TextContent?.Trim();
            if (string.IsNullOrWhiteSpace(lastUpdated))
            {
                LoggerUtils.Warn($"unable to parse last-updated-date for {courseUri}");
            }

            return lastUpdated;
        }

        private static string ParseCourseLanguage(IDocument document, string courseUri = null)
        {
            var language = document.QuerySelector(DomSelectors.Language)?.TextContent?.Trim();
            if (string.IsNullOrWhiteSpace(language))
            {
                LoggerUtils.Warn($"unable to parse language data for {courseUri}");
            }

            return language;
        }

        private static string ParseEnrolledStudentsCount(IDocument document, string courseUri = null)
        {
            var count = document.QuerySelector(DomSelectors.EnrolledStudentCounts)?.TextContent?.Trim();
            if (string.IsNullOrWhiteSpace(count))
            {
                LoggerUtils.Warn($"unable to get enrolled students count for {courseUri}");
            }

            return count;
        }

        public static async Task<ResolveUdemyDataFromApiResult> ResolveDurationFromApiAsync(int courseId, HttpClient udemyHttpClient, bool isFreeCourse = false)
        {
            var result = new ResolveUdemyDataFromApiResult();

            try
            {
                var responseStream =
                    await udemyHttpClient.GetStreamAsync(
                        $"https://www.udemy.com/api-2.0/courses/{courseId}/?fields[course]=estimated_content_length,content_info");
                var response = await JsonSerializer.DeserializeAsync<UdemyCourseDetailsResponse>(responseStream);

                result.Data = new UdemyApiData
                {
                    Duration = isFreeCourse ? (response.EstimatedContentLength / 60).ToString() : response.ContentInfo
                };
                result.IsSuccess = true;
            }
            catch (Exception e)
            {
                result.AddError($"Unable to resolve course data from Udemy API. course-id: {courseId}");
                result.AddError(e.Message);
                LoggerUtils.Error($"Unable to resolve course data from Udemy API. course-id: {courseId}", e);
            }

            return result;
        }

        public class ResolveUdemyDataFromApiResult : BaseResultWithPayload<UdemyApiData>
        {
            
        }
        
        public class UdemyCourseDetailsResponse
        {
            [JsonPropertyName("estimated_content_length")]
            public int EstimatedContentLength { get; set; }
            
            [JsonPropertyName("content_info")]
            public string ContentInfo { get; set; }
        }
        
        public class UdemyApiData
        {
            public string Duration { get; set; } 
        }

        private static string? ParseDuration(IDocument document, bool isFreeCourse = false, string courseUri = null)
        {
            var selector = isFreeCourse ? DomSelectors.FreeCourseTabsData : DomSelectors.SidebarContainer;
            var element = document.QuerySelector(selector);
            if (element == null)
            {
                LoggerUtils.Warn($"unable to parse duration for {courseUri}");
                return null;
            }

            var jsonElement = JsonSerializer
                .Deserialize<JsonElement>(element
                    .GetAttribute(DomSelectors.DataPropsAttribute).Trim());

            if (isFreeCourse)
            {
                return jsonElement.GetProperty("curriculum").GetProperty("estimated_content_length_in_seconds")
                    .GetInt32().ToString();
            }

            return jsonElement.GetProperty("componentProps").GetProperty("incentives")
                .GetProperty("video_content_length")
                .GetString();
        }

        public static (Instructor[]? instructors, int? courseId) ParseInstructorData(IDocument document,
            bool isFreeCourse = false,
            string courseUri = null)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            var instructorDataEle = document.QuerySelector("body");
            if (instructorDataEle == null)
            {
                LoggerUtils.Error($"could not fetch instructor data, course id for {courseUri}");
                return (null, null);
            }

            var mainCourseData = JsonSerializer.Deserialize<MainCourseDataBindingModel>(instructorDataEle
                .GetAttribute("data-module-args")
                ?.Trim());

            return (mainCourseData.ServerSideProps.Course.Instructors.InstructorsInfo.Select(i => new Instructor
            {
                Name = i.DisplayName,
                Url = i.AbsoluteUrl,
                AverageRating = i.AvgRatingRecent,
                TotalNumberOfReviews = i.TotalNumReviews,
                TotalNumberOfStudents = i.TotalNumStudents
            }).ToArray(), mainCourseData.ServerSideProps.Course.Instructors.CourseId);
        }

        private static Rating? ParseAggregateRating(CourseDetailsBindingModel mainParsedData, string courseUri = null)
        {
            if (mainParsedData.AggregateRating == null)
            {
                LoggerUtils.Warn($"unable to parse rating data {courseUri}");
                return null;
            }

            return new Rating
            {
                Count = mainParsedData.AggregateRating?.RatingCount,
                AverageValue = mainParsedData.AggregateRating?.RatingValue,
            };
        }

        private static CourseDescription? ParseCourseDescription(IDocument document, string courseUri = null)
        {
            var courseDescriptionEle = document
                .QuerySelector(DomSelectors.DescriptionData);
            if (courseDescriptionEle == null)
            {
                LoggerUtils.Warn($"course description selector {DomSelectors.DescriptionData} not found {courseUri}");
                return null;
            }

            return JsonSerializer.Deserialize<CourseDescription>(courseDescriptionEle
                .GetAttribute(DomSelectors.DataPropsAttribute).Trim());
        }

        public class ParseCourseDetailsResult : BaseResult
        {
            public CourseDetailsViewModel CourseDetails { get; set; }
        }

        public static async Task<IsCouponValidResult> IsCouponValid(int courseId, string couponCode,
            HttpClient httpClient = null)
        {
            var url =
                $"https://www.udemy.com/api-2.0/course-landing-components/{courseId}/me/?components=discount_expiration,purchase,available_coupons&discountCode={couponCode}";

            LoggerUtils.Info($"checking coupon validity from {url}");
            var getHttpResponseAsStringResult = await HttpHelper.GetStringAsync(url, httpClient);
            if (!getHttpResponseAsStringResult.IsSuccess)
            {
                return new IsCouponValidResult(getHttpResponseAsStringResult.GetFormattedError());
            }

            var result = ParseCouponValidStatus(getHttpResponseAsStringResult.Response);
            result.CouponData.CouponCode = couponCode;
            return result;
        }

        public static IsCouponValidResult ParseCouponValidStatus(string response)
        {
            var result = new IsCouponValidResult();
            try
            {
                var responseJson = JsonSerializer.Deserialize<JsonElement>(response);
                result.IsValid = responseJson.GetProperty("discount_expiration").GetProperty("data")
                    .GetProperty("is_enabled").GetBoolean();
                if (!result.IsValid)
                {
                    result.IsSuccess = true;
                    return result;
                }

                result.CouponData = new CouponData
                {
                    ExpirationText = responseJson.GetProperty("discount_expiration").GetProperty("data")
                        .GetProperty("discount_deadline_text").GetString(),
                    DiscountedPrice = responseJson.GetProperty("purchase").GetProperty("data")
                        .GetProperty("pricing_result").GetProperty("price").GetProperty("price_string").GetString(),
                    OriginalPrice = responseJson.GetProperty("purchase").GetProperty("data")
                        .GetProperty("pricing_result").GetProperty("list_price").GetProperty("price_string")
                        .GetString(),
                    DiscountPercentage = responseJson.GetProperty("purchase").GetProperty("data")
                        .GetProperty("pricing_result").GetProperty("discount_percent").GetInt32()
                };
            }
            catch (Exception e)
            {
                result.AddError("An error occured while parsing coupon status from HTTP response");
                result.AddError(e.Message);
                LoggerUtils.Error("An error occured while parsing coupon status from HTTP response", e);
                return result;
            }

            result.IsSuccess = true;
            return result;
        }

        public class IsCouponValidResult : BaseResult
        {
            public IsCouponValidResult()
            {
            }

            public IsCouponValidResult(string error) : base(error)
            {
            }

            public bool IsValid { get; set; }

            public CouponData CouponData { get; set; }
        }

        private static class DomSelectors
        {
            public const string MainCourseData = "script[type='application/ld+json']";
            public const string InstructorData = ".ud-component--course-landing-page-udlite--instructors";
            public const string FreeCourseTabsData = ".ud-component--course-landing-page-udlite--course-content-tabs";
            public const string DescriptionData = ".ud-component--course-landing-page-udlite--description";
            public const string Language = ".clp-lead__element-item.clp-lead__locale";
            public const string EnrolledStudentCounts = "[data-purpose='enrollment']";
            public const string LastUpdatedDate = ".last-update-date span:nth-of-type(2)";
            public const string SidebarContainer = ".ud-component--course-landing-page-udlite--sidebar-container";

            public const string DataPropsAttribute = "data-component-props";
        }
    }
}