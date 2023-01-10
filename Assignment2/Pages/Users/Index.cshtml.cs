using Assignment2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public IndexModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users
                .Include(u => u.Department).ToListAsync();
            }
        }
    }
}
