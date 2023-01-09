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

namespace Assignment2.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public EditModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department =  await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            Department = department;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var departmentToUpdate = await _context.Departments.FindAsync(id);

            if (departmentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Department>(
                departmentToUpdate,
                "department",
                s => s.Name, s => s.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
