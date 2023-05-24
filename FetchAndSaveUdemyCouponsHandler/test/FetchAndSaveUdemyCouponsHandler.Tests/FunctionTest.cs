using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using Xunit;
using Xunit.Abstractions;

namespace FetchAndSaveUdemyCouponsHandler.Tests
{
    public class FunctionTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public FunctionTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task MainLogicShallExecuteAsExpected()
        {
            #region Arrange

            var function = new Function();
            var context = new TestLambdaContext();
            var args = new Function.FunctionArgs
            {
                NumberOfPagesToParse = 1
            };

            #endregion

            #region Act

            var courses = await function.FunctionHandler(args, context);

            #endregion

            #region Assert

            courses.ForEach(c => _outputHelper.WriteLine(c.ToString()));

            Assert.NotEmpty(courses);
            Assert.All(courses.Where(c => !c.IsAlreadyAFreeCourse).ToList(),
                c => Assert.NotEmpty(c.CouponData?.CouponCode));
            Assert.All(courses, c => Assert.NotEmpty(c.CourseDetails?.CourseUri));

            #endregion
        }
    }
}