using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.DTO.TaskUserDTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.AdminPage.TaskUserPage
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly ILogger<EditModel> _logger;
        private readonly string _apiUrl = "https://localhost:7112/odata/TaskUser";
        private readonly string _userApiUrl = "https://localhost:7112/odata/User";

        public EditModel(HttpClient client, ILogger<EditModel> logger)
        {
            _client = client;
            _logger = logger;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [BindProperty]
        public TaskUserUpdateDTO TaskUser { get; set; }

        public List<UserVM> UserList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await _client.GetAsync($"{_apiUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            TaskUser = JsonSerializer.Deserialize<TaskUserUpdateDTO>(strData, options)!;

            HttpResponseMessage userResponse = await _client.GetAsync(_userApiUrl);
            if (userResponse.IsSuccessStatusCode)
            {
                string userData = await userResponse.Content.ReadAsStringAsync();
                UserList = JsonSerializer.Deserialize<List<UserVM>>(userData, options);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                TaskUser.TaskId = id.Value;

                string strData = JsonSerializer.Serialize(TaskUser);
                var contentData = new StringContent(strData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync($"{_apiUrl}/{id}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Update Success";
                    return RedirectToPage("./Index");
                }
                else
                {
                    ViewData["Error"] = "Update Error";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to call API");
                ViewData["Error"] = $"Failed to call API: {ex.Message}";
                return Page();
            }
        }
    }
}
