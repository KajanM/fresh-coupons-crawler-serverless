using System.IO;
using System.Net;
using System.Net.Http;
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
        public async Task FetchingCourseDetailsFromAffiliateApiShouldSucceed()
        {
            #region Arrage 

            var udemyClient = await GetUdemyClientAsync();

            #endregion

            #region Act

            var response = await UdemyHelper.GetCourseDetailsFromAffiliateApiAsync(
                "https://www.udemy.com/course/mobile-app-development-for-people-with-autism-dyslexia-etc", udemyClient);

            #endregion

            #region Assert

            Assert.True(response.IsSuccess);
            Assert.Equal("course", response.Data.Class);

            #endregion
        }

        [Fact]
        public async Task UdemyCourseDetailsFetchingLogicShouldSucceed()
        {
            var handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("user-agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");
            // httpClient.DefaultRequestHeaders.Add(":authority", "www.udemy.com");
            // httpClient.DefaultRequestHeaders.Add(":method", "GET");
            // httpClient.DefaultRequestHeaders.Add(":scheme", "https");
            httpClient.DefaultRequestHeaders.Add("accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8");
            // httpClient.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("accept-language", "en-GB,en;q=0.5");
            httpClient.DefaultRequestHeaders.Add("host", "www.udemy.com");
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "Windows");
            httpClient.DefaultRequestHeaders.Add("sec-fetch-mode", "navigate");
            httpClient.DefaultRequestHeaders.Add("sec-fetch-dest", "document");
            httpClient.DefaultRequestHeaders.Add("sec-fetch-site", "none");
            httpClient.DefaultRequestHeaders.Add("sec-fetch-user", "?1");
            httpClient.DefaultRequestHeaders.Add("sec-gpc", "1");
            httpClient.DefaultRequestHeaders.Add("upgrade-insecure-requests", "1");
            httpClient.DefaultRequestHeaders.Add("connection", "keep-alive");
            // httpClient.DefaultRequestHeaders.Add("cookie", "seen=1;");

            var response =
                await UdemyHelper.GetCourseDetailsAsync("https://www.udemy.com/course/the-melody-of-english/", false,
                    httpClient);
            Assert.True(response.IsSuccess);
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

            var (instructors, courseId) = await UdemyHelper.ParseInstructorDataAsync(document, true);

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

            var isCouponValidResult = await UdemyHelper.ParseCouponValidStatusAsync(testString);

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

            var httpClient = await GetUdemyClientAsync();

            #endregion

            #region Act

            var resolveUdemyDataFromApiResult = await UdemyHelper.ResolveDurationFromApiAsync(3339492, httpClient);

            #endregion

            #region Assert

            Assert.True(resolveUdemyDataFromApiResult.IsSuccess);

            #endregion
        }

        private async Task<HttpClient> GetUdemyClientAsync()
        {
            var service = new ParameterStoreConfigurationService(RegionEndpoint.APSouth1);
            var getConfigResult = await service.GetAsync(Function.ConfigurationKeys.UdemyCredentials);
            return Function.GetUdemyHttpClient(getConfigResult.Config);
        }
    }
}