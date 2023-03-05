using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using AngleSharp.Html.Dom;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;

namespace FetchAndSaveUdemyCouponsHandler.DiscUdemy.Helpers
{
    public static class DiscUdemyParsingHelper
    {
        public static async Task<ParseDiscUdemyLinksResult> ParseDiscUdemyLinksAsync(
            string htmlResponseStr,
            IBrowsingContext browsingContext = null)
        {
            var result = new ParseDiscUdemyLinksResult();
            try
            {
                browsingContext ??= BrowsingContext.New(Configuration.Default);
                var document = await browsingContext.OpenAsync(req => req.Content(htmlResponseStr));
                // Array.from(document.querySelectorAll('.card .content')).filter(e => !Array.from(e.classList).includes('extra')).map(e => ({link: e.querySelector('.header .card-header').href, isFree: !e.querySelector('.meta span:nth-of-type(2)').textContent.trim().includes('>')}))
                result.Thumbnails = document.QuerySelectorAll(".card .content")
                    .Where(e => !e.ClassList.Contains("extra"))
                    .Select(e =>
                    {
                        var link = ((IHtmlAnchorElement)e.QuerySelector(".header .card-header")).Href;
                        return new DiscUdemyCourseThumbnailData
                        {
                            DiscUdemyCourseDetailsLink = link,
                            DiscUdemyCouponDetailsLink = MapToCouponCodePageLink(link),
                            IsAlreadyAFreeCourse = !e.QuerySelector(".meta span:nth-of-type(2)").TextContent.Trim()
                                .Contains(">")
                        };
                    }).ToList();

                LoggerUtils.Info(
                    $"found {result.Thumbnails.Count} disk udemy links. {result.Thumbnails.Count(t => t.IsAlreadyAFreeCourse)} of them are already free courses");
                result.IsSuccess = true;

                return result;
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync("an error occured while parsing course lists from discudemy");
                result.AddError(e.Message);
            }

            return result;
        }

        public class ParseDiscUdemyLinksResult : BaseResult
        {
            // public List<string> Links { get; set; }
            public List<DiscUdemyCourseThumbnailData> Thumbnails { get; set; }
        }

        public class DiscUdemyCourseThumbnailData
        {
            public string DiscUdemyCourseDetailsLink { get; set; }

            public string DiscUdemyCouponDetailsLink { get; set; }
            public bool IsAlreadyAFreeCourse { get; set; }
        }

        public static List<string> MapToCouponCodePageLink(IEnumerable<string> diskUdemyCourseLinks)
        {
            return diskUdemyCourseLinks.Select(MapToCouponCodePageLink)
                .ToList();
        }

        public static string MapToCouponCodePageLink(string diskUdemyCourseLink)
        {
            return $"https://www.discudemy.com/go/{diskUdemyCourseLink.Split("/")[^1]}";
        }

        public static async Task<ParseCouponDataResult> ParseCouponDataAsync(
            string couponPageHtmlStr,
            IBrowsingContext browsingContext = null, string courseUri = null)
        {
            var result = new ParseCouponDataResult();
            if (string.IsNullOrWhiteSpace(couponPageHtmlStr))
            {
                result.AddError($"{nameof(couponPageHtmlStr)} is empty");
                return result;
            }

            try
            {
                browsingContext ??= BrowsingContext.New(Configuration.Default);
                var document = await browsingContext.OpenAsync(req => req.Content(couponPageHtmlStr));
                var udemyLink = new Uri(
                    ((IHtmlAnchorElement)document.QuerySelector(DomSelectors.UdemyLinkWithCouponCode))
                    .Href);
                var couponCode = HttpUtility.ParseQueryString(new Uri(HttpUtility.ParseQueryString(udemyLink.Query).Get("url")).Query).Get("couponCode");
                if (string.IsNullOrWhiteSpace(couponCode))
                {
                    LoggerUtils.Warn($"coupon code not received for {courseUri}. May be a free course?");
                    result.AddError("Coupon code not received");
                    return result;
                }

                result.Coupon =
                    new UdemyUrlWithCouponCode
                    {
                        Url = udemyLink.GetLeftPart(UriPartial.Path)[..^1],
                        CouponCode = couponCode
                    };
                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync("an error occured while parsing coupon data from discudemy coupon page HTML", e);
                result.AddError(e.Message);
            }

            return result;
        }

        private static class DomSelectors
        {
            public const string DiskUdemyCouponLink = ".card .card-header";
            public const string UdemyLinkWithCouponCode = ".ui.segment>a";
        }
    }
}