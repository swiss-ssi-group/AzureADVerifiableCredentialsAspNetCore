using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services
{
    public class CallbackError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
