using IssuerDrivingLicense.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssuerDrivingLicense;

public class DriverLicenseService
{
    private readonly DrivingLicenseDbContext _DrivingLicenseDbContext;

    public DriverLicenseService(DrivingLicenseDbContext DrivingLicenseDbContext)
    {
        _DrivingLicenseDbContext = DrivingLicenseDbContext;
    }

    public async Task<bool> HasIdentityDriverLicense(string username)
    {
        if (!string.IsNullOrEmpty(username))
        {
            var driverLicense = await _DrivingLicenseDbContext.DriverLicenses.FirstOrDefaultAsync(
                dl => dl.UserName == username && dl.Valid == true
            );

            if (driverLicense != null)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<DriverLicense?> GetDriverLicense(string? username)
    {
        var driverLicense = await _DrivingLicenseDbContext.DriverLicenses.FirstOrDefaultAsync(
                dl => dl.UserName == username && dl.Valid == true
            );

        return driverLicense;
    }

    public async Task UpdateDriverLicense(DriverLicense driverLicense)
    {
        _DrivingLicenseDbContext.DriverLicenses.Update(driverLicense);
        await _DrivingLicenseDbContext.SaveChangesAsync();
    }
}
