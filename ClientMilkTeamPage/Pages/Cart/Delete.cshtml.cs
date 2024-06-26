using ClientMilkTeamPage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientMilkTeamPage.Pages.Cart
{
    public class DeleteModel : PageModel
    {
        private readonly CartService _cartService;

        public DeleteModel(CartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult OnGet(int teaID)
        {
            _cartService.RemoveFromCart(teaID);
            return Redirect("/Cart/Index");
        }
    }
}