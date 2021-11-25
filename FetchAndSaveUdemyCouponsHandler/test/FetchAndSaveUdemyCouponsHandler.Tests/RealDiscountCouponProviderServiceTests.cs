using System;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.RealDiscount.Services;
using Xunit;
using Xunit.Abstractions;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class RealDiscountCouponProviderServiceTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public RealDiscountCouponProviderServiceTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task CollectRealDiscountCouponPageLinksAsyncShouldSucceed()
        {
            #region Act

            var couponPageLinks =
                await RealDiscountUdemyCouponProviderService.CollectRealDiscountCouponPageLinksAsync();

            #endregion

            #region Assert

            Assert.NotEmpty(couponPageLinks);
            Assert.All(couponPageLinks, l => l.Link.Contains("https://www.real.discount/"));

            _outputHelper.WriteLine(string.Join(Environment.NewLine, couponPageLinks));

            #endregion
        }

        [Fact]
        public async Task GetCouponDataAsyncShouldSucceed()
        {
            #region Arrange

            IRealDiscountUdemyCouponProviderService providerService = new RealDiscountUdemyCouponProviderService();

            #endregion

            #region Act

            var coupons = await providerService.GetCouponDataAsync();

            #endregion

            #region Assert

            Assert.NotEmpty(coupons);
            Assert.All(coupons, c => Assert.NotEmpty(c.CouponCode));
            Assert.All(coupons, c => Assert.NotEmpty(c.Url));

            _outputHelper.WriteLine(string.Join(Environment.NewLine, coupons));

            #endregion
        }
    }
}