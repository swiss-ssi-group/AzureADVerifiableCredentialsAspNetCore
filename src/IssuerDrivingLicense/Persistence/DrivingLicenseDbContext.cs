using Microsoft.EntityFrameworkCore;

namespace IssuerDrivingLicense.Persistence;

public class DrivingLicenseDbContext : DbContext
{
    public DrivingLicenseDbContext(DbContextOptions<DrivingLicenseDbContext> options) : base(options) { }

    public DbSet<DriverLicense> DriverLicenses => Set<DriverLicense>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<DriverLicense>().HasKey(m => m.Id);

        base.OnModelCreating(builder);
    }
}
