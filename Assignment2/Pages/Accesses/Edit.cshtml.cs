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

namespace Assignment2.Pages.Accesses
{
    public class EditModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public EditModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserAccess UserAccess { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserAccess == null)
            {
                return NotFound();
            }

            var useraccess =  await _context.UserAccess.FirstOrDefaultAsync(m => m.UserAccessID == id);
            if (useraccess == null)
            {
                return NotFound();
            }
            UserAccess = useraccess;
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

            _context.Attach(UserAccess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccessExists(UserAccess.UserAccessID))
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

        private bool UserAccessExists(int id)
        {
          return (_context.UserAccess?.Any(e => e.UserAccessID == id)).GetValueOrDefault();
        }
    }
}
