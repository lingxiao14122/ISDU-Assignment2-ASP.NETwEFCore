using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2.Pages.Accesses
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public DeleteModel(Assignment2.Data.AssignmentContext context)
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

            var useraccess = await _context.UserAccess.FirstOrDefaultAsync(m => m.UserAccessID == id);

            if (useraccess == null)
            {
                return NotFound();
            }
            else 
            {
                UserAccess = useraccess;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserAccess == null)
            {
                return NotFound();
            }
            var useraccess = await _context.UserAccess.FindAsync(id);

            if (useraccess != null)
            {
                UserAccess = useraccess;
                _context.UserAccess.Remove(UserAccess);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
