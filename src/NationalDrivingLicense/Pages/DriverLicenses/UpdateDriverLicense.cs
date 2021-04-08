using System;

namespace NationalDrivingLicense.Pages.DriverLicenses
{
    public class UpdateDriverLicense
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public bool Valid { get; set; }
    }
}
