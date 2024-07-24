using ClientMilkTeamPage.ViewModel;
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
            int currentShipperId = GetCurrentShipperId(); // Obtain the current shipper's ID
            await RefreshCompletedTaskList(currentShipperId);
        }

        private async Task RefreshCompletedTaskList(int shipperId)
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
                    // and the task is assigned to the current shipper
                    TaskUserVM = taskUserList.FindAll(t =>
                        t.Status.HasValue &&
                        (t.Status.Value == true || t.Status.Value == false) &&
                        t.UserID == shipperId);
                }
                else
                {
                    TaskUserVM = new List<TaskUserVM>(); // Initialize to an empty list if the API call fails
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing completed TaskUser list: {ex.Message}");
                TaskUserVM = new List<TaskUserVM>(); // Optionally, set an error message or status for the view
            }
        }

        // This method should implement the logic to get the current shipper's ID
        private int GetCurrentShipperId()
        {
            // Ensure the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the user ID from the claims
                // You may need to adjust the claim type depending on your setup
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    return userId;
                }
            }

            // Return a default value or handle the case where the user is not authenticated
            return 0; // Or handle as needed
        }

    }
}
