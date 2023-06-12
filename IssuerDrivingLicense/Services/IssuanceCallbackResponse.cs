using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services;

public class IssuanceCallbackResponse
{
    [JsonPropertyName("requestStatus")]
    public string RequestStatus { get; set; } = string.Empty;
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = string.Empty;
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;
    [JsonPropertyName("error")]
    public CallbackError? Error { get; set; }
}
