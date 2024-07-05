using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Services;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7112/odata/Material";

        public IndexModel(CartService cartService, HttpClient httpClient)
        {
            _cartService = cartService;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<CartItem> CartItems { get; private set; }
        public List<Material> Materials { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public async Task OnGetAsync()
        {
            var token = HttpContext.Request.Cookies["UserCookie"];
            IsAuthenticated = !string.IsNullOrEmpty(token);

            if (IsAuthenticated)
            {
                CartItems = _cartService.GetCart();
                var response = await _httpClient.GetAsync(_apiUrl);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                Materials = JsonSerializer.Deserialize<List<Material>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                CartItems = new List<CartItem>();
                Materials = new List<Material>();
            }
        }

        public async Task<IActionResult> OnPostAsync(List<List<int>> SelectedMaterials)
        {
            var token = HttpContext.Request.Cookies["UserCookie"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }

            CartItems = _cartService.GetCart();
            var response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Materials = JsonSerializer.Deserialize<List<Material>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            for (int i = 0; i < CartItems.Count; i++)
            {
                CartItems[i].SelectedMaterials.Clear();
                if (SelectedMaterials != null && SelectedMaterials.Count > i)
                {
                    foreach (var materialId in SelectedMaterials[i])
                    {
                        var material = Materials.FirstOrDefault(m => m.MaterialID == materialId);
                        if (material != null)
                        {
                            CartItems[i].SelectedMaterials.Add(new SelectedMaterial
                            {
                                MaterialID = material.MaterialID,
                                MaterialName = material.MaterialName,
                                Price = material.Price
                            });
                        }
                    }
                }
            }

            _cartService.UpdateCart(CartItems);
            return RedirectToPage();
        }
    }
}