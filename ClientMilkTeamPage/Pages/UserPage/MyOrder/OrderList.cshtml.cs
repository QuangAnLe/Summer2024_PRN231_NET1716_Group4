using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Pages.AdminPage.OrderPage;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.UserPage.MyOrder
{
    public class OrderListModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string ApiUrl = "https://localhost:7112/odata/Order";

        public OrderListModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
        public IList<OrderWithTaskUserDTO> OrdersWithTaskUsers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await RefreshOrderList();
            return Page();
        }

        private async Task RefreshOrderList()
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                OrdersWithTaskUsers = JsonSerializer.Deserialize<List<OrderWithTaskUserDTO>>(strData, options);
                // Filter to only include orders with processing status (Status == null)
                Orders = OrdersWithTaskUsers.Select(o => o.Order).Where(order => order.Status == null).ToList();
            }
            else
            {
                Orders = new List<OrderDTO>(); // Initialize to an empty list if the API call fails
            }
        }
    }
}