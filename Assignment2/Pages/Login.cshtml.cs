using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Assignment2.Pages
{
    public class LoginModel : PageModel
    {
        private readonly Assignment2.Data.AssignmentContext _context;

        public LoginModel(Assignment2.Data.AssignmentContext context)
        {
            _context = context;
        }

        public String errorMessage = "";

        [BindProperty]
        [Required, MinLength(1)]
        public string Email { get; set; }
        [BindProperty]
        [Required, MinLength(1)]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // hash password
                var hashedPass = Users.CreateModel.hashpass(Password);

                var dbUser = await _context.Users
                        .Include(u => u.Department)
                        .Include(u => u.UserAccessMaps)
                        .Where(u => u.UserEmail == Email)
                        .FirstOrDefaultAsync();
                if (dbUser == null)
                {
                    errorMessage = "Credential Invalid or user doesn't exist";
                    return Page();
                }
                var validate = dbUser.Password.Equals(hashedPass);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "" + dbUser.UserID),
                    new Claim(ClaimTypes.Name, dbUser.UserName),
                };

                foreach (var userAccessMap in dbUser.UserAccessMaps)
                {
                    var access = await _context.UserAccess.FindAsync(userAccessMap.UserAccessID);
                    if (access.UserAccessName == "Business Access")
                    {
                        claims.Add(new Claim("BusinessUser", "value"));
                    }
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
            }
            return Page();
        }
    }
}
