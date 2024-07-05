using ClientMilkTeamPage.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.AdminPage.OrderPage
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string ApiUrl;

        [BindProperty]
        public Order Order { get; set; }

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Order";
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var _order = JsonSerializer.Deserialize<Order>(strData, options)!;

            Order = _order;
            return Page();
            
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var content = new StringContent(JsonSerializer.Serialize(Order), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{ApiUrl}/{Order.OrderID}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/AdminPage/OrderPage/Index");
            }
            else
            {
                // Log the error or handle it appropriately here
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Update failed: {errorMessage}");
                return Page();
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
