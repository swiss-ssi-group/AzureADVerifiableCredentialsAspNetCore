using IssuerDrivingLicense.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssuerDrivingLicense;

public class DriverLicenseService
{
    private readonly DrivingLicenseDbContext _drivingLicenseDbContext;

    public DriverLicenseService(DrivingLicenseDbContext DrivingLicenseDbContext)
    {
        _drivingLicenseDbContext = DrivingLicenseDbContext;
    }

    public async Task<bool> HasIdentityDriverLicense(string username)
    {
        if (!string.IsNullOrEmpty(username))
        {
            var driverLicense = await _drivingLicenseDbContext.DriverLicenses.FirstOrDefaultAsync(
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
        var driverLicense = await _drivingLicenseDbContext.DriverLicenses.FirstOrDefaultAsync(
                dl => dl.UserName == username && dl.Valid == true
            );

        return driverLicense;
    }

    public async Task UpdateDriverLicense(DriverLicense driverLicense)
    {
        _drivingLicenseDbContext.DriverLicenses.Update(driverLicense);
        await _drivingLicenseDbContext.SaveChangesAsync();
    }
}
