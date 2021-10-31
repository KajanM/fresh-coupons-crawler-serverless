using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AngleSharp;
using AngleSharp.Html.Dom;

namespace FetchAndSaveUdemyCouponsHandler.Helpers
{
    public static class DiskUdemyHelper
    {
        public static async Task<List<string>> ParseDiskUdemyLinksAsync(string htmlResponseStr,
            IBrowsingContext browsingContext = null)
        {
            browsingContext ??= BrowsingContext.New(Configuration.Default);
            var document = await browsingContext.OpenAsync(req => req.Content(htmlResponseStr));

            var diskUdemyLinks =
                document.QuerySelectorAll(DomSelectors.DiskUdemyCouponLink)
                    .Select(a => ((IHtmlAnchorElement)a).Href)
                    .ToList();

            LambdaLogger.Log($"Found {diskUdemyLinks.Count} disk udemy links.");

            return diskUdemyLinks;
        }

        public static string[] MapToCouponCodePageLink(params string[] diskUdemyCourseLinks)
        {
            return diskUdemyCourseLinks.Select(l => $"https://www.discudemy.com/go/{l.Split("/")[^1]}")
                .ToArray();
        }

        private static class DomSelectors
        {
            public const string DiskUdemyCouponLink = ".card .card-header";
        }
    }
}