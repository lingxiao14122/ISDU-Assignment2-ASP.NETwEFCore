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
    public class IndexModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public IndexModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        public IList<UserAccessMap> UserAccessMap { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserAccessMap != null)
            {
                UserAccessMap = await _context.UserAccessMap
                .Include(u => u.User)
                .Include(u => u.UserAccess).ToListAsync();
            }
        }
    }
}
