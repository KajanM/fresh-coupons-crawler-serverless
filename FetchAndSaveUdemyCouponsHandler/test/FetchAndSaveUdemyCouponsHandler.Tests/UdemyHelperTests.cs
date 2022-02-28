using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon;
using Amazon.Internal;
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
        public async Task ParsingUdemyCourseIdShouldSucceed()
        {
            var testString = await File.ReadAllTextAsync("./resources/udemy/free-udemy-course-details.html");
            var parsedData = await UdemyHelper.ParseCourseIdAsync(testString);
            _outputHelper.WriteLine(parsedData.Id);
        }

        [Fact]
        public async Task ResolveCourseDetailsFromApiAsyncShouldSucceed()
        {
            #region Arrange

            var configurationService = new ParameterStoreConfigurationService(RegionEndpoint.APSouth1);
            var getConfigResult = await configurationService.GetAsync(Function.ConfigurationKeys.UdemyCredentials);
            var udemyClient = new HttpClient();
            udemyClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", getConfigResult.Config[Function.ConfigurationKeys.UdemyCredentials]);

            #endregion

            #region Act

            var result = await UdemyHelper.ResolveCourseDetailsFromApiAsync("4439276", udemyClient);

            #endregion

            #region Assert

            Assert.True(result.IsSuccess);
            #endregion
        }
    }
}