using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FetchAndSaveUdemyCouponsHandler.BindingModels
{
    public class CourseDetailsBindingModel
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("@context")]
        public string Context { get; set; }

        [JsonPropertyName("publisher")]
        public Publisher Publisher { get; set; }

        [JsonPropertyName("provider")]
        public Provider Provider { get; set; }

        [JsonPropertyName("@id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("isAccessibleForFree")]
        public bool IsAccessibleForFree { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("inLanguage")]
        public string InLanguage { get; set; }

        [JsonPropertyName("audience")]
        public Audience Audience { get; set; }

        [JsonPropertyName("about")]
        public About About { get; set; }

        [JsonPropertyName("creator")]
        public List<Creator> Creator { get; set; }

        [JsonPropertyName("aggregateRating")]
        public AggregateRating AggregateRating { get; set; }
        
        [JsonPropertyName("itemListElement")]
        public List<ItemListElement> ItemListElement { get; set; }
    }

    public class Publisher
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sameAs")]
        public string SameAs { get; set; }
    }

    public class Provider
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sameAs")]
        public string SameAs { get; set; }
    }

    public class Audience
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("audienceType")]
        public string[] AudienceType { get; set; }
    }

    public class About
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Creator
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class AggregateRating
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("ratingValue")]
        public string RatingValue { get; set; }

        [JsonPropertyName("ratingCount")]
        public int RatingCount { get; set; }

        [JsonPropertyName("bestRating")]
        public int BestRating { get; set; }

        [JsonPropertyName("worstRating")]
        public double WorstRating { get; set; }
    }

    public class ItemListElement
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("item")]
        public string Item { get; set; }
    }
}