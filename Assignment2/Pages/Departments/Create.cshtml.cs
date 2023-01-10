using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages.Departments
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
        public Department Department { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyDepartment = new Department();

            if (await TryUpdateModelAsync<Department>(
                emptyDepartment,
                "department",   // Prefix for form value.
                s => s.Name, s => s.Description))
            {
                _context.Departments.Add(emptyDepartment);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
