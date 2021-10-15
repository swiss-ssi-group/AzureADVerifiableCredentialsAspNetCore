﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using IssuerDrivingLicense.Persistence;

namespace IssuerDrivingLicense
{
    public class AdminModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly DrivingLicenseDbContext _context;

        public AdminModel(IConfiguration configuration,
            DrivingLicenseDbContext context)
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

            //IQueryable<IdentityUser> UsersIQ = from s in _context.Users select s;
            //if (!string.IsNullOrEmpty(searchString))
            //{
            //    UsersIQ = UsersIQ.Where(s => s.UserName.Contains(searchString));
            //}
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        UsersIQ = UsersIQ.OrderByDescending(s => s.UserName);
            //        break;
            //    default:
            //        UsersIQ = UsersIQ.OrderBy(s => s.UserName);
            //        break;
            //}

            //int pageSize = 3;
            //Users = await PaginatedList<IdentityUser>.CreateAsync(
            //    UsersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            return Page();
        }
    }
}