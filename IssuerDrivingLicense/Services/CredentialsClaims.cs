using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services;

/// <summary>
/// Application specific claims used in the payload of the issue request. 
/// When using the id_token for the subject claims, the IDP needs to add the values to the id_token!
/// The claims can be mapped to anything then.
/// </summary>
public class CredentialsClaims
{
    /// <summary>
    /// attribute names need to match a claim from the id_token
    /// </summary>
    [JsonPropertyName("given_name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("family_name")]
    public string Details { get; set; } = string.Empty;
}
