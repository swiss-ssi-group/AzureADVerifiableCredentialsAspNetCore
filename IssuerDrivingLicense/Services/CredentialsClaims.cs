using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services;

/// <summary>
/// https://w3c-ccg.github.io/vdl-vocab/
/// https://github.com/w3c-ccg/vdl-vocab/blob/main/context/v1.jsonld
/// Iso18013DriversLicense
/// </summary>
public class CredentialsClaims
{
    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; } = string.Empty;
    [JsonPropertyName("given_name")]
    public string GivenName { get; set; } = string.Empty;
    [JsonPropertyName("birth_date")]
    public string BirthDate { get; set; } = string.Empty;
    // "@type": "http://www.w3.org/2001/XMLSchema#dateTime"
    [JsonPropertyName("issue_date")]
    public string IssueDate { get; set; } = string.Empty;
    // "@type": "http://www.w3.org/2001/XMLSchema#dateTime"
    [JsonPropertyName("expiry_date")]
    public string ExpiryDate { get; set; } = string.Empty;
    [JsonPropertyName("issuing_country")]
    public string IssuingCountry { get; set; } = string.Empty;
    [JsonPropertyName("issuing_authority")]
    public string IssuingAuthority { get; set; } = string.Empty;
    [JsonPropertyName("document_number")]
    public string DocumentNumber { get; set; } = string.Empty;
    [JsonPropertyName("administrative_number")]
    public string AdministrativeNumber { get; set; } = string.Empty;
    // "@type": "@json"
    [JsonPropertyName("driving_privileges")]
    public string DrivingPrivileges { get; set; } = string.Empty;
    [JsonPropertyName("un_distinguishing_sign")]
    public string UnDistinguishingSign { get; set; } = string.Empty;

    // Not added but part of the spec

    //[JsonPropertyName("gender")]
    //public string Gender { get; set; } = string.Empty;
    //[JsonPropertyName("height")]
    //public string Height { get; set; } = string.Empty;
    //[JsonPropertyName("weight")]
    //public string Weight { get; set; } = string.Empty;
    //[JsonPropertyName("eye_color")]
    //public string EyeColor { get; set; } = string.Empty;
    //[JsonPropertyName("hair_color")]
    //public string HairColor { get; set; } = string.Empty;
    //[JsonPropertyName("birth_place")]
    //public string BirthPlace { get; set; } = string.Empty;
    //[JsonPropertyName("resident_address")]
    //public string ResidentAddress { get; set; } = string.Empty;
    //// "type": "image/jpg;base64url",
    //[JsonPropertyName("portrait")]
    //public string Portrait { get; set; } = string.Empty;
    //[JsonPropertyName("portrait_capture_date")]
    //public string PortraitCaptureDate { get; set; } = string.Empty;
    //[JsonPropertyName("age_in_years")]
    //public string AgeInYears { get; set; } = string.Empty;
    //[JsonPropertyName("age_birth_year")]
    //public string AgeBirthYear { get; set; } = string.Empty;
    //[JsonPropertyName("issuing_jurisdiction")]
    //public string IssuingJurisdiction { get; set; } = string.Empty;
    //[JsonPropertyName("nationality")]
    //public string Nationality { get; set; } = string.Empty;
    //[JsonPropertyName("resident_city")]
    //public string ResidentCity { get; set; } = string.Empty;
    //[JsonPropertyName("resident_state")]
    //public string ResidentState { get; set; } = string.Empty;
    //[JsonPropertyName("resident_postal_code")]
    //public string ResidentPostalCode { get; set; } = string.Empty;
    //[JsonPropertyName("name_national_character")]
    //public string NameNationalCharacter { get; set; } = string.Empty;
    //[JsonPropertyName("signature_usual_mark")]
    //public string SignatureUsualMark { get; set; } = string.Empty;
}
