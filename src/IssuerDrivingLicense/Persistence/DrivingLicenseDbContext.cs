using Microsoft.EntityFrameworkCore;

namespace IssuerDrivingLicense.Persistence
{
    public class DrivingLicenseDbContext : DbContext
    {
        public DbSet<DriverLicense> DriverLicenses { get; set; }

        public DrivingLicenseDbContext(DbContextOptions<DrivingLicenseDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DriverLicense>().HasKey(m => m.Id);

            base.OnModelCreating(builder);
        }
    }
}
