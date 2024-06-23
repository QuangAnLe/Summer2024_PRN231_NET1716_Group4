using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Services;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string ApiUrl;
        private readonly string ApiUrlDetail;
        private readonly CartService _cartService;

        public CheckoutModel(CartService cartService)
        {
            _cartService = cartService;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Order";
            ApiUrlDetail = "https://localhost:7112/odata/OrderDetailByOid";


        }

        public List<CartItem> CartItems { get; private set; }

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
            orderDTO.Status = false;
            orderDTO.TypeOrder = "Online";
            //   orderDTO.UserID = int.Parse(userIdClaim);
            orderDTO.UserID = 1;
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
                    return Redirect("/MyOrder/OrderDetail?id=" + reps.OrderID);
                }
            }
            else
            {
                ViewData["Message"] = "Fail";
                return Redirect("/Cart/Index");
            }

            ViewData["Message"] = "Fail";
            return Redirect("/Cart/Index");
        }

        public void OnGet()
        { }
    }
}
