using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
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
                string taskApiUrl = "https://localhost:7112/odata/TaskUser";
                HttpResponseMessage taskResponse = await _client.GetAsync(taskApiUrl);

                if (taskResponse.IsSuccessStatusCode)
                {
                    string taskData = await taskResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var taskUserList = JsonSerializer.Deserialize<List<TaskUserVM>>(taskData, options) ?? new List<TaskUserVM>();

                    foreach (var taskUser in taskUserList)
                    {
                        // Retrieve user details
                        string userApiUrl = $"https://localhost:7112/odata/User/{taskUser.UserID}";
                        HttpResponseMessage userResponse = await _client.GetAsync(userApiUrl);
                        if (userResponse.IsSuccessStatusCode)
                        {
                            string userData = await userResponse.Content.ReadAsStringAsync();
                            var user = JsonSerializer.Deserialize<UserVM>(userData, options);
                            taskUser.UserName = user?.UserName;
                        }

                        // Retrieve order details
                        string orderApiUrl = $"https://localhost:7112/odata/Order/{taskUser.OrderID}";
                        HttpResponseMessage orderResponse = await _client.GetAsync(orderApiUrl);
                        if (orderResponse.IsSuccessStatusCode)
                        {
                            string orderData = await orderResponse.Content.ReadAsStringAsync();
                            var order = JsonSerializer.Deserialize<OrderDTO>(orderData, options);
                            taskUser.ReasonContent = order?.ReasonContent;
                            taskUser.EndDate = order?.EndDate;
                        }
                    }

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
