using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.Shipper
{
    public class TaskHistoryModel : PageModel
    {
        private readonly HttpClient _client;

        public TaskHistoryModel(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            TaskUserVM = new List<TaskUserVM>(); // Initialize to avoid null reference
        }

        public IList<TaskUserVM> TaskUserVM { get; set; }

        public async Task OnGetAsync()
        {
            await RefreshCompletedTaskList();
        }

        private async Task RefreshCompletedTaskList()
        {
            try
            {
                string apiUrl = "https://localhost:7112/odata/TaskUser";
                HttpResponseMessage response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var taskUserList = JsonSerializer.Deserialize<List<TaskUserVM>>(responseData, options) ?? new List<TaskUserVM>();

                    // Filter tasks where Status is true (Success) or false (Failed)
                    TaskUserVM = taskUserList.FindAll(t => t.Status.HasValue && (t.Status.Value == true || t.Status.Value == false));
                }
                else
                {
                    TaskUserVM = new List<TaskUserVM>(); // Initialize to an empty list if the API call fails
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing completed TaskUser list: {ex.Message}");
            }
        }
    }
}