using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Pages.Departments
{
    public class DetailsModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public DetailsModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        public Department Department { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (department == null)
            {
                return NotFound();
            }
            else
            {
                Department = department;
            }
            return Page();
        }
    }
}
