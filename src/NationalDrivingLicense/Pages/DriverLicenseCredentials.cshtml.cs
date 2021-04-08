using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NationalDrivingLicense.Data;

namespace NationalDrivingLicense.Pages
{
    public class DriverLicenseCredentialsModel : PageModel
    {
        private readonly AzureADCredentialsService _trinsicCredentialsService;
        private readonly DriverLicenseService _driverLicenseService;

        public string DriverLicenseMessage { get; set; } = "Loading credentials";
        public bool HasDriverLicense { get; set; } = false;
        public DriverLicense DriverLicense { get; set; }

        public string CredentialOfferUrl { get; set; }
        public DriverLicenseCredentialsModel(AzureADCredentialsService trinsicCredentialsService,
           DriverLicenseService driverLicenseService)
        {
            _trinsicCredentialsService = trinsicCredentialsService;
            _driverLicenseService = driverLicenseService;
        }
        public async Task OnGetAsync()
        {
            DriverLicense = await _driverLicenseService.GetDriverLicense(HttpContext.User.Identity.Name);

            if (DriverLicense != null)
            {
                var offerUrl = await _trinsicCredentialsService
                    .GetDriverLicenseCredential(HttpContext.User.Identity.Name);
                DriverLicenseMessage = "Add your driver license credentials to your wallet";
                CredentialOfferUrl = offerUrl;
                HasDriverLicense = true;
            }
            else
            {
                DriverLicenseMessage = "You have no valid driver license";
            }
        }
    }
}
