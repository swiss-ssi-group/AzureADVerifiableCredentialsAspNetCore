using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services;

public class IssuanceResponse
{
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    [JsonPropertyName("expiry")]
    public int Expiry { get; set; }
    [JsonPropertyName("pin")]
    public string Pin { get; set; } = string.Empty;
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}
