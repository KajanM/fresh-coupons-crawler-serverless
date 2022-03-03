using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon;
using AngleSharp;
using FetchAndSaveUdemyCouponsHandler.Config;
using FetchAndSaveUdemyCouponsHandler.Udemy.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class UdemyHelperTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public UdemyHelperTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task ParseInstructorDataShouldSucceed()
        {
            #region Arrange

            var testString = await File.ReadAllTextAsync("./resources/udemy/free-udemy-course-details.html");
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var document = await browsingContext.OpenAsync(req => req.Content(testString));

            #endregion

            #region Act

            var (instructors, courseId) = UdemyHelper.ParseInstructorData(document, true);

            #endregion

            #region Assert

            Assert.True(courseId.HasValue);
            Assert.NotEmpty(instructors);

            #endregion
        }

        [Fact]
        public async Task UdemyCourseDetailsParsingLogicShouldSucceed()
        {
            var testString = await File.ReadAllTextAsync("./resources/udemy/udemy-course-details.html");
            var parsedData = await UdemyHelper.ParseCourseDetailsAsync(testString);
            _outputHelper.WriteLine(parsedData.ToString());
        }

        [Fact]
        public async Task UdemyCourseDetailsParsingLogicShouldSucceedWithFreeCourses()
        {
            var testString = await File.ReadAllTextAsync("./resources/udemy/free-udemy-course-details.html");
            var parsedData = await UdemyHelper.ParseCourseDetailsAsync(testString);
            _outputHelper.WriteLine(parsedData.ToString());
        }

        [Fact]
        public async Task ParseCouponValidStatusShouldCorrectlyIdentifyValidCoupon()
        {
            #region Arrange

            var testString = await File.ReadAllTextAsync("./resources/udemy/valid-coupon.json");

            #endregion

            #region Act

            var isCouponValidResult = UdemyHelper.ParseCouponValidStatus(testString);

            #endregion

            #region Assert

            Assert.True(isCouponValidResult.IsSuccess);
            Assert.True(isCouponValidResult.IsValid);
            Assert.Equal(isCouponValidResult.CouponData.DiscountedPrice, "Free");
            Assert.Equal(isCouponValidResult.CouponData.DiscountPercentage, 100);
            Assert.Equal(isCouponValidResult.CouponData.OriginalPrice, "$19.99");
            Assert.Equal(isCouponValidResult.CouponData.ExpirationText, "2 hours");

            #endregion
        }

        [Fact]
        public async Task ResolveUdemyDataFromApiAsyncShouldSucceedInHappyPath()
        {
            #region Arrange

            var service = new ParameterStoreConfigurationService(RegionEndpoint.APSouth1);
            var getConfigResult = await service.GetAsync(Function.ConfigurationKeys.UdemyCredentials);
            var httpClient = Function.GetUdemyHttpClient(getConfigResult.Config);

            #endregion

            #region Act

            var resolveUdemyDataFromApiResult = await UdemyHelper.ResolveDurationFromApiAsync(3339492, httpClient);

            #endregion

            #region Assert

            Assert.True(resolveUdemyDataFromApiResult.IsSuccess);

            #endregion
        }
    }
}