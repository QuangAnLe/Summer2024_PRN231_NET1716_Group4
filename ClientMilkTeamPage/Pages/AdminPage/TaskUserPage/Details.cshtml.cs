using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.AdminPage.TaskUserPage
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly string _apiUrl = "https://localhost:7112/odata/TaskUser";
        private readonly string _userApiUrl = "https://localhost:7112/odata/User"; // Assuming User API endpoint

        public DetailsModel(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public TaskUser TaskUser { get; set; } = new TaskUser();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"{_apiUrl}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                TaskUser = JsonSerializer.Deserialize<TaskUser>(strData, options);

                HttpResponseMessage userResponse = await _client.GetAsync($"{_userApiUrl}/{TaskUser.UserID}");
                if (userResponse.IsSuccessStatusCode)
                {
                    string userData = await userResponse.Content.ReadAsStringAsync();
                    var userVM = JsonSerializer.Deserialize<UserVM>(userData, options);

                    // Map UserVM to TaskUser.User
                    TaskUser.User = new User
                    {
                        UserID = userVM.UserID,
                        UserName = userVM.UserName,
                        // Add any other properties you need to map from UserVM to User
                    };
                }

                return Page();
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }
    }
}
