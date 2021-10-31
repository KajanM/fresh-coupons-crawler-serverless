using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FetchAndSaveUdemyCouponsHandler.BindingModels
{
    public class CourseDescription
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("target_audiences")]
        public List<string> TargetAudiences { get; set; }
    }
}