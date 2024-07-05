using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ClientMilkTeamPage.ViewModel;

namespace ClientMilkTeamPage.Pages.Cart
{
    public class AddModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string ApiUrl;
        private readonly CartService _cartService;

        public AddModel(CartService cartService)
        {
            _cartService = cartService;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Tea";
        }

        public List<CartItem> CartItems { get; private set; }

        public async Task<IActionResult> OnGetAsync(int teaID, int quantity)
        {
            var token = HttpContext.Request.Cookies["UserCookie"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            Tea tea = await GetTeaByIdAsync(teaID);
            if (tea != null)
            {
                if (quantity > tea.Estimation)
                {
                    TempData["ErrorMessage"] = $"Cannot add more than {tea.Estimation} units of {tea.TeaName} to the cart.";
                    return RedirectToPage("/HomePage");
                }

                CartItem item = new CartItem
                {
                    TeaID = tea.TeaID,
                    TeaName = tea.TeaName,
                    Quantity = quantity,
                    Price = tea.Price
                };
                _cartService.AddToCart(item);
            }
            return RedirectToPage("/HomePage");
        }

        private async Task<Tea> GetTeaByIdAsync(int teaID)
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Tea> teas = JsonSerializer.Deserialize<List<Tea>>(strData, options)!;
            return teas.FirstOrDefault(x => x.TeaID == teaID);
        }
    }
}
