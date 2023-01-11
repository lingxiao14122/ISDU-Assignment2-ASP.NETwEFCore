using Assignment2.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace Assignment2.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(Assignment2.Data.AssignmentContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewData["Active"] = new SelectList(Active.active);
            return Page();
        }

        [BindProperty]
        public User User { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyUser = new User();

            // keep for future ref
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                .SelectMany(E => E.Errors)
                .Select(E => E.ErrorMessage)
                .ToList();
                foreach (var error in validationErrors)
                {
                    Console.WriteLine(error);
                }
            }

            if (User.FormFile != null)
            {
                User.Photo = ProcessUploadedFile(User.FormFile, webHostEnvironment);
            }

            if (await TryUpdateModelAsync<User>(
                emptyUser,
                "user",
                s => s.UserName, s => s.UserEmail, s => s.EmployeeNumber,
                s => s.Age, s => s.Password, s => s.DepartmentID, s => s.Active))
            {
                emptyUser.Photo = ProcessUploadedFile(User.FormFile, webHostEnvironment);
                // hash password
                emptyUser.Password = hashpass(emptyUser.Password);
                _context.Users.Add(emptyUser);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewData["Active"] = new SelectList(Active.active);
            return Page();
        }

        public static String hashpass(String password)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] result;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                result = sha256Hash.ComputeHash(data);
            }
            // Return the hexadecimal string
            return System.Text.Encoding.UTF8.GetString(result);
        }
        public static string ProcessUploadedFile(IFormFile formFile,IWebHostEnvironment webHostEnvironment)
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

    public static class Active
    {
        public const string YES = "Yes", NO = "No";
        public static List<string> active = new List<string> { YES, NO };
    }
}
