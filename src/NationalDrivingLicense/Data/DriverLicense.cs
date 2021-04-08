using System;
using System.ComponentModel.DataAnnotations;

namespace NationalDrivingLicense.Data
{
    public class DriverLicense
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset IssuedAt { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Issuedby { get; set; }
        public bool Valid { get; set; }
        public string DriverLicenseCredentials { get; set; }
        public string LicenseType { get; set; }
    }
}
