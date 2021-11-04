using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using FetchAndSaveUdemyCouponsHandler.DiscUdemy.Helpers;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;

namespace FetchAndSaveUdemyCouponsHandler.Services.DiscUdemy
{
    public class DiscUdemyCouponProviderService : IDiscUdemyCouponProviderService
    {
        public string Name => "DiscUdemy";

        public async Task<List<UdemyUrlWithCouponCode>> GetCouponDataAsync(int startPageNo = 1,
            int numberOfPagesToCrawl = 5,
            HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            var coupons = new List<UdemyUrlWithCouponCode>();
            var couponPageLinks =
                await CollectDiscUdemyCouponPageLinksAsync(startPageNo, numberOfPagesToCrawl, httpClient,
                    browsingContext);
            if (!couponPageLinks.Any()) return coupons;

            var couponCrawlingTasks = couponPageLinks.Where(t => !t.IsAlreadyAFreeCourse).Distinct().Select(
                discUdemyCouponPageLink => Task.Run(async () =>
                {
                    var getCouponDataFromDiscUdemyCouponPageResult = await GetCouponDataFromDiscUdemyCouponPageAsync(
                        discUdemyCouponPageLink.DiscUdemyCouponDetailsLink,
                        httpClient,
                        browsingContext);
                    if (getCouponDataFromDiscUdemyCouponPageResult.IsSuccess)
                    {
                        coupons.Add(getCouponDataFromDiscUdemyCouponPageResult.Coupon);
                    }
                })).ToArray();

            coupons.AddRange(couponPageLinks.Where(l => l.IsAlreadyAFreeCourse).Select(l => new UdemyUrlWithCouponCode
            {
                Url = MapDiscUdemyCourseLinkToUdemyLink(l.DiscUdemyCourseDetailsLink),
                IsAlreadyAFreeCourse = true
            }));

            try
            {
                await Task.WhenAll(couponCrawlingTasks);
            }
            catch (Exception e)
            {
                LoggerUtils.Error("an error occured while getting udemy coupons from discudemy", e);
            }

            return coupons;
        }

        public static string MapDiscUdemyCourseLinkToUdemyLink(string diskUdemyLink)
        {
            var coursePathVariable = diskUdemyLink.Split("/").Last();
            if (string.IsNullOrWhiteSpace(coursePathVariable))
            {
                LoggerUtils.Error($"unable to map udemy link from {diskUdemyLink}");
                throw new ArgumentException(nameof(diskUdemyLink));
            }

            return $"https://www.udemy.com/course/{coursePathVariable}";
        }

        public static async Task<List<DiscUdemyParsingHelper.DiscUdemyCourseThumbnailData>>
            CollectDiscUdemyCouponPageLinksAsync(int startPageNo = 1,
                int pageCount = 5,
                HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            var discUdemyCouponPageLinks = new List<DiscUdemyParsingHelper.DiscUdemyCourseThumbnailData>();
            for (var pageNo = startPageNo; pageNo <= pageCount; pageNo++)
            {
                var getDiscUdemyCouponPageLinksResult =
                    await GetDiscUdemyCouponPageLinksAsync(pageNo, httpClient, browsingContext);
                if (!getDiscUdemyCouponPageLinksResult.IsSuccess) continue;

                discUdemyCouponPageLinks.AddRange(
                    getDiscUdemyCouponPageLinksResult.Thumbnails);
            }

            return discUdemyCouponPageLinks;
        }

        private static async Task<DiscUdemyParsingHelper.ParseDiscUdemyLinksResult>
            GetDiscUdemyCouponPageLinksAsync(
                int pageNo = 1,
                HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            var discUdemyCourseListUrl = $"https://www.discudemy.com/all/{pageNo}";
            var result = new DiscUdemyParsingHelper.ParseDiscUdemyLinksResult();
            var htmlResponse = await HttpHelper.GetStringAsync(discUdemyCourseListUrl, httpClient);
            if (!htmlResponse.IsSuccess)
            {
                result.AddError(htmlResponse.GetFormattedError());
                return result;
            }

            var parseDiscUdemyLinksResult =
                await DiscUdemyParsingHelper.ParseDiscUdemyLinksAsync(htmlResponse.Response, browsingContext);
            if (!parseDiscUdemyLinksResult.IsSuccess)
            {
                result.AddError(parseDiscUdemyLinksResult.GetFormattedError());
                return result;
            }

            result.Thumbnails = parseDiscUdemyLinksResult.Thumbnails;
            result.IsSuccess = true;
            return result;
        }

        private static async Task<GetCouponDataFromDiscUdemyCouponPageResult> GetCouponDataFromDiscUdemyCouponPageAsync(
            string discUdemyCouponPageUrl, HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            var result = new GetCouponDataFromDiscUdemyCouponPageResult();
            var htmlResponse = await HttpHelper.GetStringAsync(discUdemyCouponPageUrl, httpClient);
            if (!htmlResponse.IsSuccess)
            {
                result.AddError(htmlResponse.GetFormattedError());
                return result;
            }

            LoggerUtils.Info($"parsing coupon data from {discUdemyCouponPageUrl}");
            var parseCouponDataResult =
                await DiscUdemyParsingHelper.ParseCouponDataAsync(htmlResponse.Response, browsingContext,
                    discUdemyCouponPageUrl);
            if (!parseCouponDataResult.IsSuccess)
            {
                LoggerUtils.Error($"an error occured while parsing coupon data from {discUdemyCouponPageUrl}");
                result.AddError(parseCouponDataResult.GetFormattedError());
                return result;
            }

            LoggerUtils.Info($"successfully parsed coupon data from {discUdemyCouponPageUrl}");

            result.Coupon = parseCouponDataResult.Coupon;
            result.IsSuccess = true;

            return result;
        }

        private class GetCouponDataFromDiscUdemyCouponPageResult : BaseResult
        {
            public UdemyUrlWithCouponCode Coupon { get; set; }
        }
    }
}