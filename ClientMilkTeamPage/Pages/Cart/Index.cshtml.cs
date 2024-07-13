using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Services;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ClientMilkTeamPage.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7112/odata/Material";
        private readonly string _userApiUrl = "https://localhost:7112/odata/User/";
        public UserInfo CurrentUser { get; private set; }

        public IndexModel(CartService cartService, HttpClient httpClient)
        {
            _cartService = cartService;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<CartItem> CartItems { get; private set; }
        public List<Material> Materials { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public bool IsCartEmpty => CartItems == null || CartItems.Count == 0;
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

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token) as JwtSecurityToken;
                var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userIdClaim))
                {
                    var userResponse = await _httpClient.GetAsync(_userApiUrl + userIdClaim);
                    if (userResponse.IsSuccessStatusCode)
                    {
                        var userJsonString = await userResponse.Content.ReadAsStringAsync();
                        CurrentUser = JsonSerializer.Deserialize<UserInfo>(userJsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }
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
            Materials = await GetMaterialsAsync();

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
                            if (material.Quantity < CartItems[i].Quantity)
                            {
                                ModelState.AddModelError("", $"Not enough {material.MaterialName} for {CartItems[i].TeaName}.");
                                return Page();
                            }

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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _cartService.UpdateCart(CartItems);
            return RedirectToPage();
        }

        private async Task<List<Material>> GetMaterialsAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Material>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }


    public class UserInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string UserAddress { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public string WardName { get; set; }
        public string DistrictName { get; set; }
    }
}