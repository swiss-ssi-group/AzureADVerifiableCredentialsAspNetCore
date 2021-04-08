using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NationalDrivingLicense.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        public DbSet<FidoStoredCredential> FidoStoredCredential { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DriverLicense>().HasKey(m => m.Id);
            builder.Entity<FidoStoredCredential>().HasKey(m => m.Id);

            base.OnModelCreating(builder);
        }
    }
}
