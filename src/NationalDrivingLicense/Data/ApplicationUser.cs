using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace NationalDrivingLicense.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<DriverLicense> DriverLicenses { get; set; }
    }
}