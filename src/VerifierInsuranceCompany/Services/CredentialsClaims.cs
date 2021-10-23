using System.Text.Json.Serialization;

namespace VerifierInsuranceCompany.Services
{
    /// <summary>
    /// Application specific claims used in the payload of the verify repsonse. 
    /// The values need to match the rules mapping json configuration file which was uploaded to the Azure VC storage in the credential definition.
    /// The mapping values are used in this class. The claims mapping is mapped in the json file for the verifier.
    /// </summary>
    public class CredentialsClaims
    {
        /// <summary>
        /// attribute names need to match a claim from the id_token
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("details")]
        public string Details { get; set; } = string.Empty;
    }
}
