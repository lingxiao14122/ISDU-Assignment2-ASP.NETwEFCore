using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2.Pages.Accesses
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
            return Page();
        }

        [BindProperty]
        public UserAccess UserAccess { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UserAccess == null || UserAccess == null)
            {
                return Page();
            }

            _context.UserAccess.Add(UserAccess);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
