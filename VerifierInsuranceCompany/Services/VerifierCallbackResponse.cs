using System.Text.Json.Serialization;

namespace VerifierInsuranceCompany.Services;

public class VerifierCallbackResponse
{
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = string.Empty;

    [JsonPropertyName("requestStatus")]
    public string RequestStatus { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("error")]
    public CallbackError? Error { get; set; }

    [JsonPropertyName("verifiedCredentialsData")]
    public List<VerifiedCredentialsData> VerifiedCredentialsData { get; set; } = new List<VerifiedCredentialsData>();

}

public class CallbackError
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

public class VerifiedCredentialsData
{
    [JsonPropertyName("authority")]
    public string Authority { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public List<string> CredentialTypes { get; set; } = new List<string>();

    [JsonPropertyName("claims")]
    public CredentialsClaims Claims { get; set; } = new CredentialsClaims();

}
