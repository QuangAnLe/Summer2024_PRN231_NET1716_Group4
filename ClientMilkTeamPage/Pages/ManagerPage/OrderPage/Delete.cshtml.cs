using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Pages.AdminPage.OrderPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.ManagerPage.OrderPage
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string ApiUrl;

        public OrderEditViewModel OrderVM { get; set; }

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
                var orderWithTaskUser = JsonSerializer.Deserialize<OrderWithTaskUserDTO>(strData, options)!;
                if (orderWithTaskUser == null)
                {
                    return NotFound();
                }
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
