using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services
{
    public class IssuanceRequestPayload
    {
        [JsonPropertyName("includeQRCode")]
        public bool IncludeQRCode { get; set; }
        [JsonPropertyName("callback")]
        public Callback Callback { get; set; } = new Callback();    
        [JsonPropertyName("authority")]
        public string Authority { get; set; } = string.Empty;
        [JsonPropertyName("registration")]
        public Registration Registration { get; set; }
        [JsonPropertyName("issuance")]
        public Issuance Issuance { get; set; } = new Issuance();
    }

    public class Callback
    {
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        [JsonPropertyName("headers")]
        public Headers Headers { get; set; } = new Headers();

    }

    public class Headers
    {
        [JsonPropertyName("api-key")]
        public string ApiKey { get; set; } = string.Empty;
    }

    public class Registration
    {
        [JsonPropertyName("clientName")]
        public string ClientName { get; set; } = string.Empty;
    }

    public class Issuance
    {
        [JsonPropertyName("type")]
        public string CredentialsType { get; set; } = string.Empty;
        [JsonPropertyName("manifest")]
        public string Manifest { get; set; } = string.Empty;
        [JsonPropertyName("pin")]
        public Pin Pin { get; set; } = new Pin();   
        [JsonPropertyName("claims")]
        public CredentialsClaims Claims { get; set; } = new CredentialsClaims();
        
    }

    public class Pin
    {
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
        [JsonPropertyName("length")]
        public int Length { get; set; } = 4;
    }

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
}
