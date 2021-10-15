using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssuerDrivingLicense.Persistence;

namespace IssuerDrivingLicense
{
    public class AdminModel : PageModel
    {
        private readonly DrivingLicenseDbContext _context;

        public AdminModel(DrivingLicenseDbContext context)
        {
            _context = context;
        }

        public List<DriverLicense> DriverLicenses { get; set; }

        public async Task OnGetAsync()
        {
            DriverLicenses = await _context.DriverLicenses.ToListAsync();
        }
    }
}