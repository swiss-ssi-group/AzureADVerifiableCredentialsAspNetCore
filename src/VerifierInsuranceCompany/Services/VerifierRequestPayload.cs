using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VerifierInsuranceCompany.Services
{
    public class VerifierRequestPayload
    {
        [JsonPropertyName("includeQRCode")]
        public bool IncludeQRCode { get; set; }
        [JsonPropertyName("callback")]
        public Callback Callback { get; set; } = new Callback();    
        [JsonPropertyName("authority")]
        public string Authority { get; set; } = string.Empty;
        [JsonPropertyName("registration")]
        public Registration Registration { get; set; } = new Registration();
        [JsonPropertyName("presentation")]
        public Presentation Presentation { get; set; } = new Presentation();
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
        [JsonPropertyName("purpose")]
        public string Purpose { get; set; } = string.Empty;
    }

    public class Presentation
    {
        [JsonPropertyName("includeReceipt")]
        public bool IncludeReceipt { get; set; }
        [JsonPropertyName("requestedCredentials")]
        public RequestedCredentials RequestedCredentials { get; set; } = new RequestedCredentials();
    }

    public class RequestedCredentials
    {
        [JsonPropertyName("type")]
        public string CrendentialsType { get; set; } = string.Empty;
        [JsonPropertyName("purpose")]
        public string Purpose { get; set; } = string.Empty;
        [JsonPropertyName("acceptedIssuers")]
        public List<string> AcceptedIssuers { get; set; } = new List<string>();
        
    }
}
