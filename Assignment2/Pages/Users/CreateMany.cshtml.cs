using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2.Pages.Users
{
    public class CreateManyModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateManyModel(Assignment2.Data.AssignmentContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewData["Active"] = new SelectList(Active.active);
            if (RowCount <= 0)
            {
                RowCount = 1;
            }
            Users = new List<User>(new User[RowCount]);
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public int RowCount { get; set; }

        [BindProperty]
        public List<User> Users { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var User in Users)
            {
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
                var emptyUser = new User();

                if (ModelState.IsValid)
                {
                    emptyUser.UserName = User.UserName;
                    emptyUser.UserEmail = User.UserEmail;
                    emptyUser.EmployeeNumber = User.EmployeeNumber;
                    emptyUser.Age = User.Age;
                    emptyUser.Password = User.Password;
                    emptyUser.DepartmentID = User.DepartmentID;
                    emptyUser.Active = User.Active;

                    if (User.FormFile != null)
                    {
                        emptyUser.Photo = CreateModel.ProcessUploadedFile(User.FormFile, webHostEnvironment);
                    }
                    // hash password
                    emptyUser.Password = CreateModel.hashpass(emptyUser.Password);
                    _context.Users.Add(emptyUser);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
                    ViewData["Active"] = new SelectList(Active.active);
                    return Page();
                }
            }

            RowCount = 0;

            return RedirectToPage("./Index");
        }
    }
}
