using System.Text.Json.Serialization;

namespace VerifierInsuranceCompany.Services
{
    public class CacheData
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("expiry")]
        public string Expiry { get; set; } = string.Empty;
        [JsonPropertyName("payload")]
        public string Payload { get; set; } = string.Empty;
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("details")]
        public string Details { get; set; } = string.Empty;
    }
}
