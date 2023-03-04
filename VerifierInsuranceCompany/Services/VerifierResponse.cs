using System.Text.Json.Serialization;

namespace VerifierInsuranceCompany.Services;

public class VerifierResponse
{
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = string.Empty;
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    [JsonPropertyName("expiry")]
    public int Expiry { get; set; }
    [JsonPropertyName("qrCode")]
    public string QrCode { get; set; } = string.Empty;
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}
