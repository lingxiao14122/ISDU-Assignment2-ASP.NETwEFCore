using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2.Pages.GrantAccesses
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public DeleteModel(Assignment2.Data.AssignmentContext context)
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

            var useraccessmap = await _context.UserAccessMap.FirstOrDefaultAsync(m => m.UserAccessMapID == id);

            if (useraccessmap == null)
            {
                return NotFound();
            }
            else 
            {
                UserAccessMap = useraccessmap;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserAccessMap == null)
            {
                return NotFound();
            }
            var useraccessmap = await _context.UserAccessMap.FindAsync(id);

            if (useraccessmap != null)
            {
                UserAccessMap = useraccessmap;
                _context.UserAccessMap.Remove(UserAccessMap);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
