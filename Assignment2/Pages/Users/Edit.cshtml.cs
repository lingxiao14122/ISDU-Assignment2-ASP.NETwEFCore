using Assignment2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Pages.Users
{
    [Authorize(Policy = "BusinessUserOnly")]
    public class EditModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(Assignment2.Data.AssignmentContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
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
                userToUpdate.Password = CreateModel.hashpass(User.Password);
            }

            if (User.FormFile != null)
            {
                userToUpdate.Photo = ProcessUploadedFile(User.FormFile);
            }

            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "user",
                s => s.UserName, s => s.UserEmail, s => s.EmployeeNumber,
                s => s.Age, s => s.DepartmentID, s => s.Active))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewData["Active"] = new SelectList(Active.active);
            return Page();
        }

        private string ProcessUploadedFile(IFormFile formFile)
        {
            string uniqueFileName;

            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
