using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FetchAndSaveUdemyCouponsHandler.BindingModels
{
    public class InstructorsDataBindingModel
    {
        [JsonPropertyName("course_id")]
        public int CourseId { get; set; }

        [JsonPropertyName("instructors_info")]
        public List<InstructorsInfo> InstructorsInfo { get; set; }
    }
    
    public class InstructorsInfo
    {
        [JsonPropertyName("avg_rating_recent")]
        public double AvgRatingRecent { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("image_50x50")]
        public string Image50x50 { get; set; }

        [JsonPropertyName("image_75x75")]
        public string Image75x75 { get; set; }

        [JsonPropertyName("image_200_H")]
        public string Image200H { get; set; }

        [JsonPropertyName("job_title")]
        public string JobTitle { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("absolute_url")]
        public string AbsoluteUrl { get; set; }

        [JsonPropertyName("total_num_reviews")]
        public int TotalNumReviews { get; set; }

        [JsonPropertyName("total_num_students")]
        public int TotalNumStudents { get; set; }

        [JsonPropertyName("total_num_taught_courses")]
        public int TotalNumTaughtCourses { get; set; }
    }

}