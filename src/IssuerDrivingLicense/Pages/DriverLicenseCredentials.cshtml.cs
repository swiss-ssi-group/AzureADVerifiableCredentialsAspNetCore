using IssuerDrivingLicense.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssuerDrivingLicense.Pages
{
    public class DriverLicenseCredentialsModel : PageModel
    {
        private readonly DriverLicenseService _driverLicenseService;

        public string DriverLicenseMessage { get; set; } = "Loading credentials";
        public bool HasDriverLicense { get; set; } = false;
        public DriverLicense? DriverLicense { get; set; }

        public DriverLicenseCredentialsModel(DriverLicenseService driverLicenseService)
        {
            _driverLicenseService = driverLicenseService;
        }
        public async Task OnGetAsync()
        {
            DriverLicense = await _driverLicenseService.GetDriverLicense(HttpContext.User?.Identity?.Name);

            if (DriverLicense != null)
            {
                DriverLicenseMessage = "Add your driver license credentials to your wallet";
                HasDriverLicense = true;
            }
            else
            {
                DriverLicenseMessage = "You have no valid driver license";
            }
        }
    }
}
