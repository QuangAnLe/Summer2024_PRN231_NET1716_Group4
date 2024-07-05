using ClientMilkTeamPage.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.AdminPage.OrderPage
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string OrderApiUrl;
        private readonly string UserApiUrl;
        private readonly string OrderDetailApiUrl;

        public DetailsModel(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "https://localhost:7112/odata/Order";
            UserApiUrl = "https://localhost:7112/odata/User";
            OrderDetailApiUrl = "https://localhost:7112/odata/OrderDetailByOid/";
        }

        public Order Order { get; set; }
        public string UserName { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync($"{OrderApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var order = JsonSerializer.Deserialize<Order>(strData, options);
                if (order == null)
                {
                    return NotFound();
                }
                Order = order;

                // Fetch user details
                HttpResponseMessage userResponse = await client.GetAsync($"{UserApiUrl}/{order.UserID}");
                if (userResponse.IsSuccessStatusCode)
                {
                    string userData = await userResponse.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<User>(userData, options);
                    UserName = user?.UserName ?? "Unknown";
                }
                else
                {
                    UserName = "Unknown";
                }

                // Fetch order details
                HttpResponseMessage detailsResponse = await client.GetAsync(OrderDetailApiUrl + id);
                if (detailsResponse.IsSuccessStatusCode)
                {
                    string detailsData = await detailsResponse.Content.ReadAsStringAsync();
                    var orderDetail = JsonSerializer.Deserialize<List<OrderDetail>>(detailsData, options);
                    if (orderDetail != null)
                    {
                        OrderDetails = orderDetail;
                    }
                }
                else
                {
                    OrderDetails = null;
                }

                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public string GetStatusDisplay(bool? status)
        {
            return status switch
            {
                null => "Processing",
                true => "Success",
                false => "Failed"
            };
        }
    }
}