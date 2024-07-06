using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.DTO.CommentDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.UserPage.MyOrder
{
    public class OrderDetailModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string ApiUrl = "";
        public Order OrderSummary { get; set; } = default!;

        public OrderDetailModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/OrderDetailByOid";
        }
        public IList<OrderDetail> orderdetails { get; set; } = default!;
        public string OrderUrl { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            // Fetch order details
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var orderResp = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options)!;
            orderdetails = orderResp;

            // Fetch order summary
            HttpResponseMessage summaryResponse = await client.GetAsync($"https://localhost:7112/odata/Order/{id}");
            string summaryData = await summaryResponse.Content.ReadAsStringAsync();
            OrderSummary = JsonSerializer.Deserialize<Order>(summaryData, options)!;

            if (OrderSummary.Status == null)
            {
                HttpResponseMessage response1 = await client.PostAsync($"https://localhost:7112/odata/CreateOrder/{id}", null);
                var jsonResponse = await response1.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonResponse);
                if (responseData.ContainsKey("order_url"))
                {
                    OrderUrl = responseData["order_url"].ToString();
                }
            }
            return Page();
        }
        
    }
}
