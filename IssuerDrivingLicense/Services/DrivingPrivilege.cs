using System.Text.Json.Serialization;

namespace IssuerDrivingLicense.Services;

/// <summary>
///{
///  "codes": [{ "code": "D"}],
///  "vehicle_category_code": "D",
///  "issue_date": "2019-01-01",
///  "expiry_date": "2027-01-01"
///}
/// </summary>
public class DrivingPrivilege
{
    [JsonPropertyName("codes")]
    public List<DrivingPrivilegeCode> Codes { get; set; } = new List<DrivingPrivilegeCode>();
    [JsonPropertyName("vehicle_category_code")]
    public string VehicleCategoryCode { get; set; } = string.Empty;
    [JsonPropertyName("issue_date")]
    public string IssueDate { get; set; } = string.Empty;
    [JsonPropertyName("expiry_date")]
    public string ExpiryDate { get; set; } = string.Empty;
}

public class DrivingPrivilegeCode
{
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
}
