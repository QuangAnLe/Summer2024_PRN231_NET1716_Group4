using ClientMilkTeamPage.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.ManagerPage.OrderPage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string ApiUrl = "";

        public IndexModel()
        {

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Order";
        }

        public IList<Order> Orders { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Order> orders = JsonSerializer.Deserialize<List<Order>>(strData, options)!;

            Orders = orders;

            return Page();
        }

        public async Task<IActionResult> OnPostSetStatusAsync(int id, bool? status)
        {
            try { 
                HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
                response.EnsureSuccessStatusCode(); 
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
                order.Status = status;

                var content = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
                HttpResponseMessage putResponse = await client.PutAsync($"{ApiUrl}/{id}", content);

                if (putResponse.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    return StatusCode((int)putResponse.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
