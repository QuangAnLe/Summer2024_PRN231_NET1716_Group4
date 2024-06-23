using ClientMilkTeamPage.Services;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ClientMilkTeamPage.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;
  
        public IndexModel(CartService cartService)
        {
            _cartService = cartService;
           
        }

        public List<CartItem> CartItems { get; private set; }

        public void OnGet()
        {
            CartItems = _cartService.GetCart();
        }
    }
}
