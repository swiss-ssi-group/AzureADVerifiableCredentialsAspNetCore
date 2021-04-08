using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NationalDrivingLicense.Data;

namespace NationalDrivingLicense
{
    public class AdminModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AdminModel(IConfiguration configuration,
            ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<IdentityUser> Users { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            var claimTwoFactorEnabled = User.Claims.FirstOrDefault(t => t.Type == "amr");

            if (claimTwoFactorEnabled != null && "mfa".Equals(claimTwoFactorEnabled.Value))
            {
                // You logged in with MFA, or MFA is disabled, do the admin stuff
            }
            else
            {
                var requireMfa = bool.Parse(_configuration["MfaRequiredForAdmin"]);
                if (requireMfa)
                {
                    return Redirect("/Identity/Account/Manage/TwoFactorAuthentication");
                }
            }

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<IdentityUser> UsersIQ = from s in _context.Users select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                UsersIQ = UsersIQ.Where(s => s.UserName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    UsersIQ = UsersIQ.OrderByDescending(s => s.UserName);
                    break;
                default:
                    UsersIQ = UsersIQ.OrderBy(s => s.UserName);
                    break;
            }

            int pageSize = 3;
            Users = await PaginatedList<IdentityUser>.CreateAsync(
                UsersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}