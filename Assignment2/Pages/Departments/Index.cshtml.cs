using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly Assignment2.Data.ApplicationContext _context;

        public IndexModel(Assignment2.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Department != null)
            {
                Department = await _context.Department.ToListAsync();
            }
        }
    }
}
