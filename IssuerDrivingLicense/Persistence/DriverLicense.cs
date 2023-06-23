using System.ComponentModel.DataAnnotations;

namespace IssuerDrivingLicense.Persistence;

public class DriverLicense
{
    [Key]
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? Issuedby { get; set; } = string.Empty;
    public bool Valid { get; set; }
    public string DriverLicenseCredentials { get; set; } = string.Empty;

    // properties from spec
    public string FamilyName { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public DateTimeOffset IssueDate { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public string IssuingCountry { get; set; } = string.Empty;
    public string IssuingAuthority { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string AdministrativeNumber { get; set; } = string.Empty;
    public string DrivingPrivileges { get; set; } = string.Empty;
    public string UnDistinguishingSign { get; set; } = string.Empty;

    //public string Gender { get; set; } = string.Empty;
    //public string Height { get; set; } = string.Empty;
    //public string Weight { get; set; } = string.Empty;
    //public string EyeColor { get; set; } = string.Empty;
    //public string HairColor { get; set; } = string.Empty;
    //public string BirthPlace { get; set; } = string.Empty;
    //public string ResidentAddress { get; set; } = string.Empty;
    //public string Portrait { get; set; } = string.Empty;
    //public string PortraitCaptureDate { get; set; } = string.Empty;
    //public string AgeInYears { get; set; } = string.Empty;
    //public string AgeBirthYear { get; set; } = string.Empty;
    //public string IssuingJurisdiction { get; set; } = string.Empty;
    //public string Nationality { get; set; } = string.Empty;
    //public string ResidentCity { get; set; } = string.Empty;
    //public string ResidentState { get; set; } = string.Empty;
    //public string ResidentPostalCode { get; set; } = string.Empty;
    //public string NameNationalCharacter { get; set; } = string.Empty;
    //public string SignatureUsualMark { get; set; } = string.Empty;
}
