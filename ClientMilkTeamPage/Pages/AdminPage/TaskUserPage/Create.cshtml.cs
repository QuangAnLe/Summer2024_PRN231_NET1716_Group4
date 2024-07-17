using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.DTO.TaskUserDTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ClientMilkTeamPage.Pages.AdminPage.TaskUserPage
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly ILogger<CreateModel> _logger;
        private readonly string _apiUrl = "https://localhost:7112/odata/TaskUser";
        private readonly string _userApiUrl = "https://localhost:7112/odata/User";
        private readonly string _orderApiUrl = "https://localhost:7112/odata/Order";

        public CreateModel(HttpClient client, ILogger<CreateModel> logger)
        {
            _client = client;
            _logger = logger;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [BindProperty]
        public TaskUserCreateDTO TaskUser { get; set; } = default!;

        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Orders { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateUsersAsync();
            await PopulateOrdersAsync();
            return Page();
        }

        private async Task PopulateUsersAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(_userApiUrl);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var users = JsonSerializer.Deserialize<List<UserVM>>(strData, options);

                // Filter users to only include those with the role "Customer"
                var customerUsers = users?.Where(u => u.RoleName == "Customer").ToList();

                Users = customerUsers?.ConvertAll(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.UserName
                }) ?? new List<SelectListItem>();
            }
        }


        private async Task PopulateOrdersAsync()
        {
            _logger.LogInformation("Fetching orders from API...");
            HttpResponseMessage response = await _client.GetAsync(_orderApiUrl);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var orders = JsonSerializer.Deserialize<List<OrderDTO>>(strData, options);

                if (orders != null)
                {
                    _logger.LogInformation("Orders fetched successfully. Number of orders: {Count}", orders.Count);
                    Orders = orders.ConvertAll(o => new SelectListItem
                    {
                        Value = o.OrderID.ToString(),
                        Text = o.OrderID.ToString()
                    });
                }
                else
                {
                    _logger.LogWarning("No orders found in the response.");
                    Orders = new List<SelectListItem>();
                }
            }
            else
            {
                _logger.LogError("Error fetching orders. Status Code: {StatusCode}", response.StatusCode);
                Orders = new List<SelectListItem>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateUsersAsync();
                await PopulateOrdersAsync();
                return Page();
            }

            try
            {
                string strData = JsonSerializer.Serialize(TaskUser);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_apiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add New Task successfully";
                    return RedirectToPage("./Index");
                }
                else
                {
                    _logger.LogError("Error adding new task. Status Code: {StatusCode}", response.StatusCode);
                    ViewData["ErrorMessage"] = "Error while adding new task";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to call API.");
                ViewData["ErrorMessage"] = $"Fail To Call API: {ex.Message}";
            }

            await PopulateUsersAsync();
            await PopulateOrdersAsync();
            return Page();
        }
    }
}
