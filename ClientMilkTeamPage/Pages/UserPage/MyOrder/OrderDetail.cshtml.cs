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

        public OrderDetailModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/OrderDetailByOid";
        }
        public IList<OrderDetail> orderdetails { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var orderResp = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options)!;

            orderdetails = orderResp;
            return Page();
        }
    }
}
