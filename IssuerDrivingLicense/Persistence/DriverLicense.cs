using System.ComponentModel.DataAnnotations;

namespace IssuerDrivingLicense.Persistence;

public class DriverLicense
{
    [Key]
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTimeOffset IssuedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string? Issuedby { get; set; } = string.Empty;
    public bool Valid { get; set; }
    public string DriverLicenseCredentials { get; set; } = string.Empty;
    public string LicenseType { get; set; } = string.Empty;
}
