using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.DTO.TaskUserDTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.AdminPage.TaskUserPage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl = "https://localhost:7112/odata/TaskUser";
        private readonly string _userApiUrl = "https://localhost:7112/odata/User";

        public IndexModel(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IList<TaskUser> TaskUser { get; set; } = new List<TaskUser>();

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(_apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                TaskUser = JsonSerializer.Deserialize<List<TaskUser>>(strData, options) ?? new List<TaskUser>();

                foreach (var taskUser in TaskUser)
                {
                    HttpResponseMessage userResponse = await _client.GetAsync($"{_userApiUrl}({taskUser.UserID})");
                    if (userResponse.IsSuccessStatusCode)
                    {
                        string userData = await userResponse.Content.ReadAsStringAsync();
                        var userVM = JsonSerializer.Deserialize<UserVM>(userData, options);

                        taskUser.User = new User
                        {
                            UserID = userVM.UserID,
                            UserName = userVM.UserName,
                        };
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, bool status)
        {
            var updateUrl = $"{_apiUrl}/{id}/updatestatus";  // Corrected URL for updating TaskUser by ID
            var taskUserUpdateDTO = new TaskUpdateStatus { TaskId = id, Status = status };  // TaskUpdateStatusDTO as per your definition
            var content = new StringContent(JsonSerializer.Serialize(taskUserUpdateDTO), System.Text.Encoding.UTF8, "application/json");

            try
            {
                // Send PATCH request
                HttpResponseMessage response = await _client.PatchAsync(updateUrl, content);

                // Check response status
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage();  // Redirect on successful update
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError(string.Empty, "Task not found.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Error updating status: {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException e)
            {
                ModelState.AddModelError(string.Empty, $"Request error: {e.Message}");
            }

            return Page();
        }
    }
}
