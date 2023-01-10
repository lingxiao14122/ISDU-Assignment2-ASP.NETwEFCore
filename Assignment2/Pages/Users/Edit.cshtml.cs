using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public EditModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewData["Active"] = new SelectList(Active.active);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userToUpdate = await _context.Users.FindAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            // hash if password field edited
            if (! User.Password.Equals(userToUpdate.Password))
            {
                User.Password = CreateModel.hashpass(User.Password);
            }

            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "user",
                s => s.UserName, s => s.UserEmail, s => s.EmployeeNumber,
                s => s.Age, s => s.Password, s => s.DepartmentID, s => s.Active))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewData["Active"] = new SelectList(Active.active);
            return Page();
        }
    }
}
