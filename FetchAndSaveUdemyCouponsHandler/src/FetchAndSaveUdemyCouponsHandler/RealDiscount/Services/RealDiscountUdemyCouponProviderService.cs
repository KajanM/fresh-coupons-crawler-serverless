using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using FetchAndSaveUdemyCouponsHandler.RealDiscount.Helpers;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;

namespace FetchAndSaveUdemyCouponsHandler.RealDiscount.Services
{
    public class RealDiscountUdemyCouponProviderService : IRealDiscountUdemyCouponProviderService
    {
        public string Name => "RealDiscount";

        public async Task<List<UdemyUrlWithCouponCode>> GetCouponDataAsync(int startPageNo = 1,
            int numberOfPagesToCrawl = 5,
            HttpClient httpClient = null,
            IBrowsingContext browsingContext = null)
        {
            var coupons = new List<UdemyUrlWithCouponCode>();
            var couponPageLinks =
                await CollectRealDiscountCouponPageLinksAsync(startPageNo, numberOfPagesToCrawl, httpClient,
                    browsingContext);
            if (!couponPageLinks.Any()) return coupons;

            var couponCrawlingTasks = couponPageLinks.Distinct().Select(
                realDiscountCouponPageLink => Task.Run(async () =>
                {
                    var getCouponDataFromRealDiscountCouponPageResult = await GetCouponDataFromRealDiscountCouponPageAsync(
                        realDiscountCouponPageLink.Link,
                        httpClient,
                        browsingContext);
                    if (getCouponDataFromRealDiscountCouponPageResult.IsSuccess)
                    {
                        coupons.Add(getCouponDataFromRealDiscountCouponPageResult.Coupon);
                    }
                })).ToArray();

            try
            {
                await Task.WhenAll(couponCrawlingTasks);
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync("an error occured while getting udemy coupons from real-discount", e);
            }

            return coupons;
        }


        public static async Task<List<RealDiscountParsingHelper.RealDiscountCourseThumbnailData>>
            CollectRealDiscountCouponPageLinksAsync(int startPageNo = 1,
                int pageCount = 5,
                HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            var realDiscountCouponPageLinks = new List<RealDiscountParsingHelper.RealDiscountCourseThumbnailData>();
            for (var pageNo = startPageNo; pageNo <= pageCount; pageNo++)
            {
                var getRealDiscountCouponPageLinksResult =
                    await GetRealDiscountCouponPageLinksAsync(pageNo, httpClient, browsingContext);
                if (!getRealDiscountCouponPageLinksResult.IsSuccess) continue;

                realDiscountCouponPageLinks.AddRange(
                    getRealDiscountCouponPageLinksResult.Thumbnails);
            }

            return realDiscountCouponPageLinks;
        }

        private static async Task<RealDiscountParsingHelper.ParseRealDiscountLinksResult>
            GetRealDiscountCouponPageLinksAsync(
                int pageNo = 1,
                HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            var realDiscountCourseListUrl = $"https://www.real.discount/filter/?category=All&subcategory=All&store=Udemy&duration=All&price=All&rating=All&language=All&search=&submit=Filter&page={pageNo}";
            var result = new RealDiscountParsingHelper.ParseRealDiscountLinksResult();
            var htmlResponse = await HttpHelper.GetStringAsync(realDiscountCourseListUrl, httpClient);
            if (!htmlResponse.IsSuccess)
            {
                result.AddError(htmlResponse.GetFormattedError());
                return result;
            }

            var parseRealDiscountLinksResult =
                await RealDiscountParsingHelper.ParseRealDiscountLinksAsync(htmlResponse.Response, browsingContext);
            if (!parseRealDiscountLinksResult.IsSuccess)
            {
                result.AddError(parseRealDiscountLinksResult.GetFormattedError());
                return result;
            }

            result.Thumbnails = parseRealDiscountLinksResult.Thumbnails;
            result.IsSuccess = true;
            return result;
        }

        private static async Task<GetCouponDataFromRealDiscountCouponPageResult> GetCouponDataFromRealDiscountCouponPageAsync(
            string realDiscountCouponPageUrl, HttpClient httpClient = null, IBrowsingContext browsingContext = null)
        {
            var result = new GetCouponDataFromRealDiscountCouponPageResult();
            var htmlResponse = await HttpHelper.GetStringAsync(realDiscountCouponPageUrl, httpClient);
            if (!htmlResponse.IsSuccess)
            {
                result.AddError(htmlResponse.GetFormattedError());
                return result;
            }

            LoggerUtils.Info($"parsing coupon data from {realDiscountCouponPageUrl}");
            var parseCouponDataResult =
                await RealDiscountParsingHelper.ParseCouponDataAsync(htmlResponse.Response, browsingContext,
                    realDiscountCouponPageUrl);
            if (!parseCouponDataResult.IsSuccess)
            {
                await LoggerUtils.ErrorAsync($"an error occured while parsing coupon data from {realDiscountCouponPageUrl}");
                result.AddError(parseCouponDataResult.GetFormattedError());
                return result;
            }

            LoggerUtils.Info($"successfully parsed coupon data from {realDiscountCouponPageUrl}");

            result.Coupon = parseCouponDataResult.Coupon;
            result.IsSuccess = true;

            return result;
        }

        private class GetCouponDataFromRealDiscountCouponPageResult : BaseResult
        {
            public UdemyUrlWithCouponCode Coupon { get; set; }
        }
    }
}