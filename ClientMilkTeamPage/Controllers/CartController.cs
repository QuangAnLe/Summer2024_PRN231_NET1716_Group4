using Microsoft.AspNetCore.Mvc;
using ClientMilkTeamPage.Services;
using System.Text.Json;
using ClientMilkTeamPage.ViewModel;
using ClientMilkTeamPage.DTO;
using System.Net.Http.Headers;

namespace ClientMilkTeamPage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly HttpClient client;
        private readonly string ApiUrl;
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Tea";
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddToCartRequest request)
        {
            var token = Request.Cookies["UserCookie"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is not logged in");
            }

            var tea = await GetTeaByIdAsync(request.TeaID);
            if (tea == null)
            {
                return NotFound("Tea not found");
            }

            if (request.Quantity > tea.Estimation)
            {
                return BadRequest($"Cannot add more than {tea.Estimation} units of {tea.TeaName} to the cart.");
            }

            CartItem item = new CartItem
            {
                TeaID = tea.TeaID,
                TeaName = tea.TeaName,
                Quantity = request.Quantity,
                Price = tea.Price
            };

            _cartService.AddToCart(item);

            return Ok(new { message = $"{tea.TeaName} added to cart successfully" });
        }

        [HttpPost("UpdateTeaQuantity")]
        public IActionResult UpdateTeaQuantity([FromBody] UpdateTeaQuantityRequest request)
        {
            var cartItems = _cartService.GetCart();
            var cartItem = cartItems.FirstOrDefault(item => item.TeaID == request.TeaId);
            if (cartItem != null)
            {
                cartItem.Quantity = request.Quantity;
                cartItem.TotalPrice = CalculateTotalPrice(cartItem);
                _cartService.UpdateCart(cartItems);
                return Ok(new
                {
                    itemTotal = cartItem.TotalPrice,
                    cartTotal = cartItems.Sum(item => item.TotalPrice)
                });
            }
            return BadRequest();
        }

        public class UpdateTeaQuantityRequest
        {
            public int TeaId { get; set; }
            public int Quantity { get; set; }
        }

        private async Task<Tea> GetTeaByIdAsync(int teaID)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{teaID}");
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

        public class AddToCartRequest
        {
            public int TeaID { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost("UpdateMaterial")]
        public IActionResult UpdateMaterial([FromBody] UpdateMaterialRequest request)
        {
            var cartItems = _cartService.GetCart();
            var cartItem = cartItems.FirstOrDefault(item => item.TeaID == request.TeaId);
            if (cartItem != null)
            {
                var selectedMaterial = cartItem.SelectedMaterials.FirstOrDefault(sm => sm.MaterialID == request.MaterialId);
                if (selectedMaterial != null)
                {
                    if (request.Quantity > 0)
                    {
                        selectedMaterial.Quantity = request.Quantity;
                    }
                    else
                    {
                        cartItem.SelectedMaterials.Remove(selectedMaterial);
                    }
                }
                else if (request.Quantity > 0)
                {
                    cartItem.SelectedMaterials.Add(new SelectedMaterial
                    {
                        MaterialID = request.MaterialId,
                        MaterialName = request.MaterialName,
                        Price = request.MaterialPrice,
                        Quantity = request.Quantity
                    });
                }

                cartItem.TotalPrice = CalculateTotalPrice(cartItem);
                _cartService.UpdateCart(cartItems);

                return Ok(new
                {
                    itemTotal = cartItem.TotalPrice,
                    cartTotal = cartItems.Sum(item => item.TotalPrice)
                });
            }

            return BadRequest();
        }

        private double CalculateTotalPrice(CartItem item)
        {
            return (item.Price + item.SelectedMaterials.Sum(m => m.Price * m.Quantity)) * item.Quantity;
        }
    }

    public class UpdateMaterialRequest
    {
        public int TeaId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public double MaterialPrice { get; set; }
        public int Quantity { get; set; }
    }
}