using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2.Pages.GrantAccesses
{
    public class CreateModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public CreateModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserID");
        ViewData["UserAccessID"] = new SelectList(_context.UserAccess, "UserAccessID", "UserAccessID");
            return Page();
        }

        [BindProperty]
        public UserAccessMap UserAccessMap { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UserAccessMap == null || UserAccessMap == null)
            {
                return Page();
            }

            _context.UserAccessMap.Add(UserAccessMap);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
