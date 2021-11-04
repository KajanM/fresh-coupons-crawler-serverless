using System.Text.Json;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object source)
        {
            return JsonSerializer.Serialize(source, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
}