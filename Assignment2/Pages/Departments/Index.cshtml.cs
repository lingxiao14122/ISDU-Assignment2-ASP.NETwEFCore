using Assignment2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public IndexModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Departments != null)
            {
                Department = await _context.Departments.ToListAsync();
            }
        }
    }
}
