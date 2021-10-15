using IssuerDrivingLicense.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IssuerDrivingLicense.Pages.DriverLicenses
{
    public class CreateModel : PageModel
    {
        private readonly DrivingLicenseDbContext _context;

        [FromQuery(Name = "id")]
        public string Id { get; set; }

        public string UserName { get; set; }

        [BindProperty]
        public DriverLicense DriverLicense { get; set; } = new DriverLicense();

        public CreateModel(DrivingLicenseDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id)
        {
            DriverLicense.UserName = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DriverLicense.Issuedby = HttpContext.User.Identity.Name;
            DriverLicense.IssuedAt = DateTimeOffset.UtcNow;

            _context.DriverLicenses.Add(DriverLicense);
            await _context.SaveChangesAsync();

            return RedirectToPage("./User", new { id = DriverLicense.UserName });
        }
    }
}
