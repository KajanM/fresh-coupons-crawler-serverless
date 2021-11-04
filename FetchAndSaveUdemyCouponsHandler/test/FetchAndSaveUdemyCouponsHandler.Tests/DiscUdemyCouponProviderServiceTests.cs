using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.Services.DiscUdemy;
using Xunit;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class DiscUdemyCouponProviderServiceTests
    {
        [Fact]
        public async Task CollectDiscUdemyCouponPageLinksAsyncShouldSucceed()
        {
            #region Act

            var couponPageLinks = await DiscUdemyCouponProviderService.CollectDiscUdemyCouponPageLinksAsync();

            #endregion

            #region Assert

            Assert.NotEmpty(couponPageLinks);
            Assert.All(couponPageLinks, l => l.DiscUdemyCouponDetailsLink.Contains("https://www.discudemy.com/go/"));

            #endregion
        }

        [Fact]
        public async Task GetCouponDataAsyncShouldSucceed()
        {
            #region Arrange

            IDiscUdemyCouponProviderService providerService = new DiscUdemyCouponProviderService();

            #endregion

            #region Act

            var coupons = await providerService.GetCouponDataAsync();

            #endregion

            #region Assert

            Assert.NotEmpty(coupons);
            Assert.All(coupons, c => Assert.NotEmpty(c.CouponCode));
            Assert.All(coupons, c => Assert.NotEmpty(c.Url));

            #endregion
        }
    }
}