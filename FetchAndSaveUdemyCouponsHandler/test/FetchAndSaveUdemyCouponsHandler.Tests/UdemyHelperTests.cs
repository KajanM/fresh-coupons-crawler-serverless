using System.IO;
using System.Threading.Tasks;
using FetchAndSaveUdemyCouponsHandler.Helpers;
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
        public async Task UdemyCourseDetailsParsingLogicShouldSucceed()
        {
            var testString = await File.ReadAllTextAsync("./resources/udemy-course-details.html");
            var parsedData = await UdemyHelper.ParseCourseDetailsAsync(testString);
            _outputHelper.WriteLine(parsedData.ToString());
        }
    }
}