using System.Text.Json.Serialization;

namespace FetchAndSaveUdemyCouponsHandler.Udemy.BindingModels
{
    public class CourseTabs
    { 
        [JsonPropertyName("instructorInfo")]
        public InstructorsDataBindingModel InstructorInfo { get; set; }
    }
}