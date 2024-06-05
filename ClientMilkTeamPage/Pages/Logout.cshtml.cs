using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientMilkTeamPage.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {

			Response.Cookies.Delete("UserCookie");
			return RedirectToPage("/HomePage");
		}
    }
}
