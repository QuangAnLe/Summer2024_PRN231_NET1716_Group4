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

        public CreateModel(HttpClient client, ILogger<CreateModel> logger)
        {
            _client = client;
            _logger = logger;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [BindProperty]
        public TaskUserCreateDTO TaskUser { get; set; } = default!;

        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateUsersAsync();
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

                // Handle case where users might be null
                var customerUsers = users?.Where(u => u.RoleName == "Customer").ToList();

                Users = customerUsers?.ConvertAll(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.UserName
                }) ?? new List<SelectListItem>();
            }
            else
            {
                _logger.LogError("Error fetching users. Status Code: {StatusCode}", response.StatusCode);
                Users = new List<SelectListItem>();
            }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateUsersAsync();
                return Page();
            }

            try
            {
                // Ensure TaskUser is not null
                if (TaskUser == null)
                {
                    ViewData["ErrorMessage"] = "TaskUser is not initialized.";
                    return Page();
                }

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
            return Page();
        }

    }
}
