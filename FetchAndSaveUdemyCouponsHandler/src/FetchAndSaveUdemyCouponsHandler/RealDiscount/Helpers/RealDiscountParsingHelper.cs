using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using AngleSharp.Html.Dom;
using FetchAndSaveUdemyCouponsHandler.Shared.Dtos;
using FetchAndSaveUdemyCouponsHandler.Shared.Helpers;

namespace FetchAndSaveUdemyCouponsHandler.RealDiscount.Helpers
{
    public static class RealDiscountParsingHelper
    {
        public static async Task<ParseRealDiscountLinksResult> ParseRealDiscountLinksAsync(
            string htmlResponseStr,
            IBrowsingContext browsingContext = null)
        {
            var result = new ParseRealDiscountLinksResult();
            try
            {
                browsingContext ??= BrowsingContext.New(Configuration.Default);
                var document = await browsingContext.OpenAsync(req => req.Content(htmlResponseStr));

                result.Thumbnails = document.QuerySelectorAll(DomSelectors.RealDiscountCourseLink)
                    .Where(e => e.QuerySelectorAll(DomSelectors.CouponExpiryText)
                        .FirstOrDefault(e => ((IHtmlDivElement)e).Attributes["style"]?.Value == "float")?.TextContent
                        ?.Trim()?.ToLower() != "expired")
                    .Select(element =>
                        new RealDiscountCourseThumbnailData
                        {
                            Link =
                                ((IHtmlAnchorElement)element).Href.Replace("http://localhost", "https://real.discount")
                        }).ToList();

                LoggerUtils.Info($"found {result.Thumbnails.Count} real-discount links.");

                result.IsSuccess = true;
                return result;
            }
            catch (Exception e)
            {
                await LoggerUtils.ErrorAsync("an error occured while parsing course lists from real-discount");
                result.AddError(e.Message);
            }

            return result;
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
                var couponAnchorElement
                    = document.QuerySelectorAll(DomSelectors.UdemyLinkWithCouponCode)
                        .FirstOrDefault(e => ((IHtmlAnchorElement)e).Href.Contains("https://www.udemy.com"));
                
                if (couponAnchorElement == null)
                {
                   result.AddError("Unable to get any relevant anchor element");
                   return result;
                }
                
                var link = ((IHtmlAnchorElement)couponAnchorElement).Href;
                var udemyLink = new Uri(link[(link.LastIndexOf(":", StringComparison.Ordinal) - 5)..]);
                var couponCode = HttpUtility.ParseQueryString(udemyLink.Query).Get("couponCode");
                if (string.IsNullOrWhiteSpace(couponCode))
                {
                    LoggerUtils.Warn($"coupon code not received for {courseUri}");
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
                await LoggerUtils.ErrorAsync(
                    $"an error occured while parsing coupon data from discudemy coupon page HTML at {courseUri}", e);
                result.AddError(e.Message);
            }

            return result;
        }


        public class ParseRealDiscountLinksResult : BaseResult
        {
            public List<RealDiscountCourseThumbnailData> Thumbnails { get; set; }

            public override string ToString()
            {
                return $"{nameof(Thumbnails)}: {string.Join(Environment.NewLine, Thumbnails)}";
            }
        }

        public class RealDiscountCourseThumbnailData
        {
            public string Link { get; set; }

            public override string ToString()
            {
                return $"{nameof(Link)}: {Link}";
            }
        }

        private static class DomSelectors
        {
            public const string RealDiscountCourseLink = ".row.main-bg a";
            public const string CouponExpiryText = ".card-duration div";
            public const string UdemyLinkWithCouponCode = "a";
        }
    }
}