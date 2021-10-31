using System;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using FetchAndSaveUdemyCouponsHandler.Helpers;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]

namespace FetchAndSaveUdemyCouponsHandler
{
    public class Function
    {
        public async Task FunctionHandler(ILambdaContext context)
        {
            var httpClient = new HttpClient();
            var url = "https://www.udemy.com/course/python-for-machine-learning-data-science-masterclass/";
            string courseDetailsResponse;
            try
            {
                courseDetailsResponse = await httpClient.GetStringAsync(url);
            }
            catch (Exception e)
            {
                LambdaLogger.Log($"An error occured while fetching course details from {url} {e}");
                return;
            }

            try
            {
                var parsedData = await UdemyHelper.ParseCourseDetailsAsync(courseDetailsResponse);
                LambdaLogger.Log("Successfully parsed details from Udemy");
                LambdaLogger.Log(parsedData.ToString());
            }
            catch (Exception e)
            {
               LambdaLogger.Log($"An error occured while parsing course details from Udemy {url}");
               LambdaLogger.Log(e.ToString());
            }
        }
    }
}