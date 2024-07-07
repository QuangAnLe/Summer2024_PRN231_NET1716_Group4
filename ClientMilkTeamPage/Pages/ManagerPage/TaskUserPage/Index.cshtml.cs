using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.ManagerPage.TaskUserPage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl = "https://localhost:7112/odata/TaskUser";
        private readonly string _userApiUrl = "https://localhost:7112/odata/User"; // Assuming User API endpoint

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
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                TaskUser = JsonSerializer.Deserialize<List<TaskUser>>(strData, options) ?? new List<TaskUser>();

                foreach (var taskUser in TaskUser)
                {
                    HttpResponseMessage userResponse = await _client.GetAsync($"{_userApiUrl}/{taskUser.UserID}");
                    if (userResponse.IsSuccessStatusCode)
                    {
                        string userData = await userResponse.Content.ReadAsStringAsync();
                        var userVM = JsonSerializer.Deserialize<UserVM>(userData, options);

                        // Mapping UserVM to User
                        taskUser.User = new User
                        {
                            UserID = userVM.UserID,
                            UserName = userVM.UserName,
                            // Add any other properties you need to map from UserVM to User
                        };
                    }
                }
            }
            else
            {
                // Handle error response here
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }

            return Page();
        }
    }
}
