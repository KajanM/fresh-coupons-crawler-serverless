using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FetchAndSaveUdemyCouponsHandler.Udemy.BindingModels
{
    public class MainCourseDataBindingModel
    {
        [JsonPropertyName("serverSideProps")]
        public ServerSidePropsBindingModel ServerSideProps { get; set; }
    }

    public class ServerSidePropsBindingModel
    {
        [JsonPropertyName("course")]
        public CourseBindingModel Course { get; set; } 
    }

    public class CourseBindingModel
    {
        [JsonPropertyName("instructors")]
        public InstructorBindingModel Instructors { get; set; }
    }

    public class InstructorBindingModel
    {
        [JsonPropertyName("course_id")]
        public int? CourseId { get; set; }
        
        [JsonPropertyName("instructors_info")]
        public List<InstructorsInfo> InstructorsInfo { get; set; } 
    }
}