using Microsoft.EntityFrameworkCore;
using NationalDrivingLicense.Data;
using System.Threading.Tasks;

namespace NationalDrivingLicense
{
    public class DriverLicenseService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DriverLicenseService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> HasIdentityDriverLicense(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var driverLicense = await _applicationDbContext.DriverLicenses.FirstOrDefaultAsync(
                    dl => dl.UserName == username && dl.Valid == true
                );

                if (driverLicense != null)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<DriverLicense> GetDriverLicense(string username)
        {
            var driverLicense = await _applicationDbContext.DriverLicenses.FirstOrDefaultAsync(
                    dl => dl.UserName == username && dl.Valid == true
                );

            return driverLicense;
        }

        public async Task UpdateDriverLicense(DriverLicense driverLicense)
        {
            _applicationDbContext.DriverLicenses.Update(driverLicense);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
