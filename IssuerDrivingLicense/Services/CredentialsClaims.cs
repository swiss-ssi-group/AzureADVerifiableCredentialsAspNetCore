using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services;

/// <summary>
/// Application specific claims used in the payload of the issue request. 
/// When using the id_token for the subject claims, the IDP needs to add the values to the id_token!
/// The claims can be mapped to anything then.
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
    [JsonPropertyName("gender")]
    public string Gender { get; set; } = string.Empty;
    [JsonPropertyName("height")]
    public string Height { get; set; } = string.Empty;





        //"weight": {
        //  "@id": "https://w3id.org/vdl#weight"
        //},
        //"eye_color": {
        //  "@id": "https://w3id.org/vdl#eye_color"
        //},
        //"hair_color": {
        //  "@id": "https://w3id.org/vdl#hair_color"
        //},
        //"birth_place": {
        //  "@id": "https://w3id.org/vdl#birth_place"
        //},
        //"resident_address": {
        //  "@id": "https://w3id.org/vdl#resident_address"
        //},
        //"portrait": {
        //  "@id": "https://w3id.org/vdl#portrait"
        //},
        //"portrait_capture_date": {
        //  "@id": "https://w3id.org/vdl#portrait_capture_date"
        //},
        //"age_in_years": {
        //  "@id": "https://w3id.org/vdl#age_in_years"
        //},
        //"age_birth_year": {
        //  "@id": "https://w3id.org/vdl#age_birth_year"
        //},
        //"issuing_jurisdiction": {
        //  "@id": "https://w3id.org/vdl#issuing_jurisdiction"
        //},
        //"nationality": {
        //  "@id": "https://w3id.org/vdl#nationality"
        //},
        //"resident_city": {
        //  "@id": "https://w3id.org/vdl#resident_city"
        //},
        //"resident_state": {
        //  "@id": "https://w3id.org/vdl#resident_state"
        //},
        //"resident_postal_code": {
        //  "@id": "https://w3id.org/vdl#resident_postal_code"
        //},
        //"name_national_character": {
        //  "@id": "https://w3id.org/vdl#name_national_character"
        //},
        //"signature_usual_mark": {
        //  "@id": "https://w3id.org/vdl#signature_usual_mark"
        //}
}
