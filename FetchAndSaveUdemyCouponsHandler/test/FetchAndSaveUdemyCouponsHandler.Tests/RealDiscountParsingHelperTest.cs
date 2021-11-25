using System.IO;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.RealDiscount.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class RealDiscountParsingHelperTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public RealDiscountParsingHelperTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task ParsingLinksFromTheRealDiscountListingPageShouldSucceed()
        {
            #region Arrange

            var testHtml = await File.ReadAllTextAsync("./resources/real-discount/course-listing.html");

            #endregion

            #region Act

            var parseDiscUdemyLinksResult = await RealDiscountParsingHelper.ParseRealDiscountLinksAsync(testHtml);

            #endregion

            #region Assert

            Assert.True(parseDiscUdemyLinksResult.IsSuccess);
            Assert.Equal(12, parseDiscUdemyLinksResult.Thumbnails.Count);
            Assert.All(parseDiscUdemyLinksResult.Thumbnails,
                l => l.Link.Contains("https://real.discount/offer/"));

            _outputHelper.WriteLine(parseDiscUdemyLinksResult.ToString());

            #endregion
        }

        [Fact]
        public async Task ParseCouponCodeLogicShallCorrectlyGetCoupon()
        {
            #region Arrange

            var testHtml = await File.ReadAllTextAsync("./resources/real-discount/coupon-details.html");

            #endregion

            #region Act

            var parseCouponDataResult = await RealDiscountParsingHelper.ParseCouponDataAsync(testHtml);

            #endregion

            #region Assert

            Assert.True(parseCouponDataResult.IsSuccess);
            Assert.Equal("48942C36E7F46FFB8898", parseCouponDataResult.Coupon.CouponCode);
            Assert.Equal("https://www.udemy.com/course/burp-suite-in-depth-survival-guide",
                parseCouponDataResult.Coupon.Url);

            _outputHelper.WriteLine(parseCouponDataResult.ToString());
            #endregion
        }
        
        [Fact]
        public async Task ParseCouponCodeLogicShallCorrectlyGetCouponThatIsLinkedWithClickLinkSynergy()
        {
            #region Arrange

            var testHtml = await File.ReadAllTextAsync("./resources/real-discount/coupon-details-with-click-synergy.html");

            #endregion

            #region Act

            var parseCouponDataResult = await RealDiscountParsingHelper.ParseCouponDataAsync(testHtml);

            #endregion

            #region Assert

            Assert.True(parseCouponDataResult.IsSuccess);
            Assert.Equal("DBDA76A4609CFF805DB7", parseCouponDataResult.Coupon.CouponCode);
            Assert.Equal("https://www.udemy.com/course/power-bi-dax-avanzado",
                parseCouponDataResult.Coupon.Url);
            
            _outputHelper.WriteLine(parseCouponDataResult.ToString());

            #endregion
        }
    }
}