using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2.Pages.GrantAccesses
{
    public class EditModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public EditModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserAccessMap UserAccessMap { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserAccessMap == null)
            {
                return NotFound();
            }

            var useraccessmap =  await _context.UserAccessMap.FirstOrDefaultAsync(m => m.UserAccessMapID == id);
            if (useraccessmap == null)
            {
                return NotFound();
            }
            UserAccessMap = useraccessmap;
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "UserName");
            ViewData["UserAccessID"] = new SelectList(_context.UserAccess, "UserAccessID", "UserAccessName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserAccessMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccessMapExists(UserAccessMap.UserAccessMapID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserAccessMapExists(int id)
        {
          return (_context.UserAccessMap?.Any(e => e.UserAccessMapID == id)).GetValueOrDefault();
        }
    }
}
