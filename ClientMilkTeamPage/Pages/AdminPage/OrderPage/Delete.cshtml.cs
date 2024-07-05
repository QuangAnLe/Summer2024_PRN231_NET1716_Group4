using ClientMilkTeamPage.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.AdminPage.OrderPage
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string ApiUrl;
        public Order Order { get; set; }

        public DeleteModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Order";
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var order = JsonSerializer.Deserialize<Order>(strData, options)!;
                if (order == null)
                {
                    return NotFound();
                }
                Order = order;
                return Page();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/AdminPage/OrderPage/Index");
            }
            else
            {
                // Log the error or handle it appropriately here
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Delete failed: {errorMessage}");
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
