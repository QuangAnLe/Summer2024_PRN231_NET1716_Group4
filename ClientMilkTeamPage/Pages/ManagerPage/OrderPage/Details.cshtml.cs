using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Pages.AdminPage.OrderPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.ManagerPage.OrderPage
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

        [BindProperty]
        public OrderEditViewModel OrderVM { get; set; }
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
                var orderWithTaskUser = JsonSerializer.Deserialize<OrderWithTaskUserDTO>(strData, options)!;

                OrderVM = new OrderEditViewModel
                {
                    OrderID = orderWithTaskUser.Order.OrderID,
                    TypeOrder = orderWithTaskUser.Order.TypeOrder,
                    ReasonContent = orderWithTaskUser.Order.ReasonContent,
                    Status = orderWithTaskUser.Order.Status,
                    StartDate = orderWithTaskUser.Order.StartDate,
                    EndDate = orderWithTaskUser.Order.EndDate,
                    ShipAddress = orderWithTaskUser.Order.ShipAddress,
                    UserID = orderWithTaskUser.Order.UserID,
                    ShipperID = orderWithTaskUser.TaskUser?.UserID ?? 0
                };

                // Fetch user details
                HttpResponseMessage userResponse = await client.GetAsync($"{UserApiUrl}/{OrderVM.UserID}");
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
