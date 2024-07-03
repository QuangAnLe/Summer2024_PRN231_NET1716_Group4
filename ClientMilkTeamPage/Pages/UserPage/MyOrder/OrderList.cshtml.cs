using ClientMilkTeamPage.DTO;
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

        public IList<Order> orders { get; set; } = new List<Order>();

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                orders = JsonSerializer.Deserialize<List<Order>>(strData, options) ?? new List<Order>();
            }
            else
            {
                orders = new List<Order>(); // Initialize to an empty list if the API call fails
            }

            return Page();
        }
    }
}
