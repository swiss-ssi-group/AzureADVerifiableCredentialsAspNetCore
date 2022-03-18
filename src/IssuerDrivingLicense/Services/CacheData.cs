using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services
{
    public class CacheData
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string? Message { get; set; } = string.Empty;
        [JsonPropertyName("expiry")]
        public string? Expiry { get; set; } = string.Empty;
        [JsonPropertyName("payload")]
        public string? Payload { get; set; } = string.Empty;
    }
}
