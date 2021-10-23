using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services
{
    public class IssuanceCallbackResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public CallbackError? Error { get; set; }

    }
}
