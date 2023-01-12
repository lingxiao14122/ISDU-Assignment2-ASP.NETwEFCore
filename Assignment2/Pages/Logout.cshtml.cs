using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment2.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync();
            Response.Redirect("/");
        }
    }
}
