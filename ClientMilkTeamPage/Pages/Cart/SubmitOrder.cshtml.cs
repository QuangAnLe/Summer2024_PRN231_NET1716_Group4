using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Services;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClientMilkTeamPage.Pages.Cart
{
    public class SubmitOrderModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string ApiUrl;
        private readonly string ApiUrlTea;
        private readonly string ApiUrlMaterial;
        private readonly string ApiUrlDetail;
        private readonly CartService _cartService;

        public SubmitOrderModel(CartService cartService)
        {
            _cartService = cartService;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Order";
            ApiUrlTea = "https://localhost:7112/odata/Tea";
            ApiUrlMaterial = "https://localhost:7112/odata/Material";
            ApiUrlDetail = "https://localhost:7112/odata/OrderDetail";
        }

        public async Task<IActionResult> OnPostAsync(string content, string address)
        {
            var token = HttpContext.Request.Cookies["UserCookie"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token) as JwtSecurityToken;
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            List<CartItem> CartItems = _cartService.GetCart();
            OrderDTO orderDTO = new OrderDTO();
            orderDTO.ShipAddress = address;
            orderDTO.ReasonContent = content;
            orderDTO.StartDate = new DateTime();
            orderDTO.Status = null;
            orderDTO.TypeOrder = "Online";
            orderDTO.UserID = int.Parse(userIdClaim);
            string strData = JsonSerializer.Serialize(orderDTO);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ApiUrl, contentData);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            if (response.IsSuccessStatusCode)
            {
                string Data = await response.Content.ReadAsStringAsync();
                Order reps = JsonSerializer.Deserialize<Order>(Data, options)!;
                List<OrderDetailDTO> orderDetailDTOs = new List<OrderDetailDTO>();
                foreach (var item in CartItems)
                {
                    OrderDetailDTO orderDetailDTO = new OrderDetailDTO();
                    orderDetailDTO.OrderID = reps.OrderID;
                    orderDetailDTO.Quantity = item.Quantity;
                    orderDetailDTO.TotalPrice = item.TotalPrice;
                    orderDetailDTO.CostsIncurred = "100d";
                    orderDetailDTO.TeaID = item.TeaID;
                    orderDetailDTOs.Add(orderDetailDTO);
                }
                string strData1 = JsonSerializer.Serialize(orderDetailDTOs);
                var contentData1 = new StringContent(strData1, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = await client.PostAsync(ApiUrlDetail, contentData1);
                if (response1.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add Order successfully";
                    return Redirect("/UserPage/MyOrder/OrderDetail?id=" + reps.OrderID);
                }
            }
            ViewData["Message"] = "Fail";
            return Redirect("/Cart/Index");

        }
        public async Task<IActionResult> OnGetAsync(string content, string address)
        {
            var token = HttpContext.Request.Cookies["UserCookie"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token) as JwtSecurityToken;
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            List<CartItem> CartItems = _cartService.GetCart();

            // Validate tea and material quantities
            foreach (var item in CartItems)
            {
                var tea = await GetTeaByIdAsync(item.TeaID);
                if (tea.Estimation < item.Quantity)
                {
                    ViewData["Message"] = $"Not enough {tea.TeaName} in stock.";
                    return RedirectToPage("/Cart/Index");
                }

                foreach (var material in item.SelectedMaterials)
                {
                    var materialData = await GetMaterialByIdAsync(material.MaterialID);
                    if (materialData.Quantity < item.Quantity)
                    {
                        ViewData["Message"] = $"Not enough {material.MaterialName} for {tea.TeaName}.";
                        return RedirectToPage("/Cart/Index");
                    }
                }
            }

            OrderDTO orderDTO = new OrderDTO
            {
                ShipAddress = address,
                ReasonContent = content,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Status = null,
                TypeOrder = "Online",
                UserID = int.Parse(userIdClaim)
            };

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            string strData = JsonSerializer.Serialize(orderDTO, options);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ApiUrl, contentData);
            if (response.IsSuccessStatusCode)
            {
                string Data = await response.Content.ReadAsStringAsync();
                Order reps = JsonSerializer.Deserialize<Order>(Data, options)!;

                foreach (var item in CartItems)
                {
                    OrderDetailDTO orderDetailDTO = new OrderDetailDTO
                    {
                        OrderID = reps.OrderID,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                        CostsIncurred = "100d",
                        TeaID = item.TeaID
                    };

                    // Update tea estimate
                    await UpdateTeaEstimate(item.TeaID, item.Quantity);

                    // Update material quantities
                    foreach (var material in item.SelectedMaterials)
                    {
                        await UpdateMaterialQuantity(material.MaterialID, item.Quantity);
                    }

                    _cartService.RemoveFromCart(item.TeaID);
                    string strData1 = JsonSerializer.Serialize(orderDetailDTO);
                    var contentData1 = new StringContent(strData1, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response1 = await client.PostAsync(ApiUrlDetail, contentData1);
                }

                ViewData["Message"] = "Add Order successfully";
                return Redirect("/UserPage/MyOrder/OrderDetail?id=" + reps.OrderID);
            }

            ViewData["Message"] = "Fail";
            return Redirect("/Cart/Index");
        }

        private async Task UpdateTeaEstimate(int teaId, int quantity)
        {
            var tea = await GetTeaByIdAsync(teaId);
            tea.Estimation -= quantity;
            await UpdateTea(tea);
        }

        private async Task UpdateMaterialQuantity(int materialId, int quantity)
        {
            var material = await GetMaterialByIdAsync(materialId);
            material.Quantity -= quantity;
            await UpdateMaterial(material);
        }

        // Add methods to get and update Tea and Material
        private async Task<Tea> GetTeaByIdAsync(int teaId)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrlTea}/{teaId}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<Tea>(strData, options);
            }
            return null;
        }

        private async Task UpdateTea(Tea tea)
        {
            string strData = JsonSerializer.Serialize(tea);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{ApiUrlTea}/{tea.TeaID}", contentData);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update tea: {response.StatusCode}");
            }
        }

        private async Task<Material> GetMaterialByIdAsync(int materialId)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrlMaterial}/{materialId}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<Material>(strData, options);
            }
            return null;
        }

        private async Task UpdateMaterial(Material material)
        {
            string strData = JsonSerializer.Serialize(material);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{ApiUrlMaterial}/{material.MaterialID}", contentData);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update material: {response.StatusCode}");
            }
        }
    }
}
