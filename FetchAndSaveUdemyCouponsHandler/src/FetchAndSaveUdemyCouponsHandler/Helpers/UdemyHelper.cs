using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AngleSharp;
using FetchAndSaveUdemyCouponsHandler.BindingModels;
using FetchAndSaveUdemyCouponsHandler.ViewModels;

namespace FetchAndSaveUdemyCouponsHandler.Helpers
{
    public static class UdemyHelper
    {
        public static async Task<CourseDetailsViewModel> ParseCourseDetailsAsync(string courseDetailsStr)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(courseDetailsStr));

            var courseContext =
                JsonSerializer.Deserialize<List<CourseDetailsBindingModel>>(document
                    .QuerySelector(DomSelectors.MainCourseData).TextContent.Trim());

            var mainParsedData = courseContext.FirstOrDefault(e => e.Type == "Course");
            if (mainParsedData == null)
            {
                LambdaLogger.Log("Unable to parse main course details");
                throw new ApplicationException("Unable to parse main course details.");
            }

            var instructorData = JsonSerializer.Deserialize<InstructorsDataBindingModel>(document
                .QuerySelector(DomSelectors.InstructorData)
                .GetAttribute(DomSelectors.DataPropsAttribute).Trim());

            var description = JsonSerializer.Deserialize<CourseDescription>(document
                .QuerySelector(DomSelectors.DescriptionData)
                .GetAttribute(DomSelectors.DataPropsAttribute).Trim());

            var duration = JsonSerializer
                .Deserialize<JsonElement>(document.QuerySelector(DomSelectors.SidebarContainer)
                    .GetAttribute(DomSelectors.DataPropsAttribute).Trim())
                .GetProperty("componentProps").GetProperty("incentives").GetProperty("video_content_length")
                .GetString();

            var courseDetails = new CourseDetailsViewModel
            {
                CourseId = instructorData.CourseId,
                Title = mainParsedData.Name,
                ShortDescription = mainParsedData.Description,
                LongDescription = description.Description,
                Language = document.QuerySelector(DomSelectors.Language).TextContent.Trim(),
                CourseUri = mainParsedData.Id,
                ImageUri = mainParsedData.Image,
                Duration = duration,
                EnrolledStudentsCount = document.QuerySelector(DomSelectors.EnrolledStudentCounts).TextContent.Trim(),
                LastUpdated = document.QuerySelector(DomSelectors.LastUpdatedDate).TextContent.Trim(),
                TargetAudiences = mainParsedData.Audience.AudienceType,
                Tags = courseContext.FirstOrDefault(e => e.Type == "BreadcrumbList")?.ItemListElement
                    .Select(e => e.Name).ToArray(),
                Rating = new Rating
                {
                    Count = mainParsedData.AggregateRating.RatingCount,
                    AverageValue = mainParsedData.AggregateRating.RatingValue,
                },
                Instructors = instructorData.InstructorsInfo.Select(i => new Instructor
                {
                    Name = i.DisplayName,
                    Url = i.AbsoluteUrl,
                    AverageRating = i.AvgRatingRecent,
                    TotalNumberOfReviews = i.TotalNumReviews,
                    TotalNumberOfStudents = i.TotalNumStudents
                }).ToArray()
            };

            return courseDetails;
        }

        private static class DomSelectors
        {
            public const string MainCourseData = "script[type='application/ld+json']";
            public const string InstructorData = ".ud-component--course-landing-page-udlite--instructors";
            public const string DescriptionData = ".ud-component--course-landing-page-udlite--description";
            public const string Language = ".clp-lead__element-item.clp-lead__locale";
            public const string EnrolledStudentCounts = "[data-purpose='enrollment']";
            public const string LastUpdatedDate = ".last-update-date span:nth-of-type(2)";
            public const string SidebarContainer = ".ud-component--course-landing-page-udlite--sidebar-container";

            public const string DataPropsAttribute = "data-component-props";
        }
    }
}