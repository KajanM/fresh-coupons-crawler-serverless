using System.IO;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.Helpers;
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

            var links = await DiskUdemyHelper.ParseDiskUdemyLinksAsync(testHtml);

            #endregion

            #region Assert

            Assert.Equal(15, links.Count);
            Assert.All(links, l => l.Contains("https://www.diskudemy.com/"));

            #endregion
        }

        [Fact]
        public void MapToCouponCodePageLinkShouldSucceed()
        {
            #region Arrange

            var courseUrl = "https://www.discudemy.com/csharp/introduction-to-aspnet-core-razor-pages-net-6";

            #endregion

            #region Act

            var couponCodePageLink = DiskUdemyHelper.MapToCouponCodePageLink(courseUrl)[0];

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

            var (isCouponValid, couponDto) = await DiskUdemyHelper.ParseCouponDataAsync(testHtml);

            #endregion

            #region Assert

            Assert.False(isCouponValid);
            Assert.Null(couponDto);

            #endregion
        }

        [Fact]
        public async Task ParseCouponCodeLogicShallCorrectlyGetValidCoupon()
        {
            #region Arrange

            var testHtml = await File.ReadAllTextAsync("./resources/disk-udemy/valid-coupon-code.html");

            #endregion

            #region Act

            var (isCouponValid, couponDto) = await DiskUdemyHelper.ParseCouponDataAsync(testHtml);

            #endregion

            #region Assert

            Assert.True(isCouponValid);
            Assert.Equal("ARABIC-COURSE", couponDto.Coupon);
            Assert.Equal("https://www.udemy.com/course/arabic-language-learn-to-read-arabic-through-short-stories",
                couponDto.Url);

            #endregion
        }
    }
}