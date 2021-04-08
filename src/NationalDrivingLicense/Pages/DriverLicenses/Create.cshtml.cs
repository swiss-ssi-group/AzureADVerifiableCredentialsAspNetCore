using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NationalDrivingLicense.Data;

namespace NationalDrivingLicense.Pages.DriverLicenses
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [FromQuery(Name = "id")]
        public string UserName { get; set; }

        [BindProperty]
        public DriverLicense DriverLicense { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserName = id;

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
