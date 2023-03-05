using System.IO;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.DiscUdemy.Helpers;
using Xunit;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class DiskUdemyHelperTests
    {
        [Fact]
        public async Task ParsingDiskUdemyListingPageShouldSucceed()
        {
            #region Arrange

            var testHtml = await File.ReadAllTextAsync("./resources/disk-udemy-listing.html");

            #endregion

            #region Act

            var parseDiscUdemyLinksResult = await DiscUdemyParsingHelper.ParseDiscUdemyLinksAsync(testHtml);

            #endregion

            #region Assert

            Assert.True(parseDiscUdemyLinksResult.IsSuccess);
            Assert.Equal(15, parseDiscUdemyLinksResult.Thumbnails.Count);
            Assert.All(parseDiscUdemyLinksResult.Thumbnails, l => l.DiscUdemyCourseDetailsLink.Contains("https://www.discudemy.com/"));

            #endregion
        }

        [Fact]
        public void MapToCouponCodePageLinkShouldSucceed()
        {
            #region Arrange

            var courseUrl = "https://www.discudemy.com/csharp/introduction-to-aspnet-core-razor-pages-net-6";

            #endregion

            #region Act

            var couponCodePageLink = DiscUdemyParsingHelper.MapToCouponCodePageLink(new[] { courseUrl })[0];

            #endregion

            #region Assert

            couponCodePageLink.Equals("https://www.discudemy.com/go/introduction-to-aspnet-core-razor-pages-net-6");

            #endregion
        }

        [Fact]
        public async Task ParseCouponCodeLogicShallIgnoreFreeCourses()
        {
            #region Arrange

            var testHtml = await File.ReadAllTextAsync("./resources/disk-udemy/free-course-details.html");

            #endregion

            #region Act

            var parseCouponDataResult = await DiscUdemyParsingHelper.ParseCouponDataAsync(testHtml);

            #endregion

            #region Assert

            Assert.False(parseCouponDataResult.IsSuccess);
            Assert.Null(parseCouponDataResult.Coupon);

            #endregion
        }

        [Fact]
        public async Task ParseCouponCodeLogicShallCorrectlyGetValidCoupon()
        {
            #region Arrange

            var testHtml = await File.ReadAllTextAsync("./resources/disk-udemy/valid-coupon-code.html");

            #endregion

            #region Act

            var parseCouponDataResult = await DiscUdemyParsingHelper.ParseCouponDataAsync(testHtml);

            #endregion

            #region Assert

            Assert.True(parseCouponDataResult.IsSuccess);
            Assert.Equal("FULLSTACK75", parseCouponDataResult.Coupon.CouponCode);
            Assert.Equal("https://www.udemy.com/course/php-for-beginners-2021-the-complete-php-mysql-pdo-course",
                parseCouponDataResult.Coupon.Url);

            #endregion
        }
    }
}