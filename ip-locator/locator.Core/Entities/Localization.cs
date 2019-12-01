using System.Text.Json.Serialization;

namespace locator.Core.Entities
{
    public class Localization
    {
        public int Id { get; set; }
        [JsonPropertyName("ip")]
        public string Ip { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("country_name")]
        public string Country { get; set; }
    }
}