using ClientMilkTeamPage.DTO.TaskUserDTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.Shipper
{
    public class ShipperPageModel : PageModel
    {
        private readonly HttpClient client;

        public ShipperPageModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            TaskUserVM = new List<TaskUserVM>();
            OrderDetails = new Dictionary<int, OrderDTO>();
        }

        public IList<TaskUserVM> TaskUserVM { get; set; }
        public Dictionary<int, OrderDTO> OrderDetails { get; set; }
        public bool ShowModal { get; set; } = false;

        [BindProperty]
        public int TaskId { get; set; }

        [BindProperty]
        public bool? Status { get; set; }

        [BindProperty]
        public string FailureReason { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await RefreshTaskList();
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitFailureReasonAsync()
        {
            try
            {
                Console.WriteLine($"Submitting FailureReason for TaskId: {TaskId}");
                await UpdateTaskStatusAsync(TaskId, false, FailureReason);
                await RefreshTaskList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating failure reason and status: {ex.Message}");
            }

            return RedirectToPage("/UserPage/MyOrder/OrderList");
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            Console.WriteLine($"Updating Status for TaskId: {TaskId} with Status: {Status}");

            // Check if Status is explicitly set to false
            if (Status.HasValue && Status.Value == false)
            {
                ShowModal = true;
                return Page();
            }

            // If Status is true, proceed with update
            if (Status.HasValue && Status.Value == true)
            {
                await UpdateTaskStatusAsync(TaskId, Status.Value, null); // Null for failure reason when status is not failed
                await RefreshTaskList();
                return RedirectToPage("/UserPage/MyOrder/OrderList");
            }

            // If Status is null, handle appropriately (you can adjust this part based on your specific requirements)
            return Page();
        }

        private async Task UpdateTaskStatusAsync(int taskId, bool status, string failureReason)
        {
            try
            {
                Console.WriteLine($"TaskId: {taskId}, Status: {status}, FailureReason: {failureReason}");

                var taskUpdateStatusDTO = new TaskUserUpdateStatusDTO
                {
                    TaskId = taskId,
                    Status = status
                };

                // Include failureReason only if status is false
                if (!status)
                {
                    taskUpdateStatusDTO.FailureReason = failureReason;
                }

                string apiUrl = $"https://localhost:7112/odata/TaskUser/{taskId}";
                Console.WriteLine($"API URL: {apiUrl}"); // Debug API URL

                string strData = JsonSerializer.Serialize(taskUpdateStatusDTO);
                var contentData = new StringContent(strData, Encoding.UTF8, "application/json");

                // Send PATCH request
                var response = await client.PatchAsync(apiUrl, contentData);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to update TaskUser status for Task: {taskId}, StatusCode: {response.StatusCode}");
                }
                else
                {
                    Console.WriteLine($"Successfully updated TaskUser status for Task: {taskId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Task status: {ex.Message}");
            }
        }

       private async Task RefreshTaskList()
        {
            try
            {
                string taskApiUrl = "https://localhost:7112/odata/TaskUser";
                HttpResponseMessage taskResponse = await client.GetAsync(taskApiUrl);

                if (taskResponse.IsSuccessStatusCode)
                {
                    string taskData = await taskResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var taskUserList = JsonSerializer.Deserialize<List<TaskUserVM>>(taskData, options) ?? new List<TaskUserVM>();

                    var allOrders = new Dictionary<int, OrderDTO>();
                    foreach (var task in taskUserList)
                    {
                        string orderApiUrl = $"https://localhost:7112/odata/Order/{task.OrderID}";
                        HttpResponseMessage orderResponse = await client.GetAsync(orderApiUrl);

                        if (orderResponse.IsSuccessStatusCode)
                        {
                            string orderData = await orderResponse.Content.ReadAsStringAsync();
                            var order = JsonSerializer.Deserialize<OrderDTO>(orderData, options);
                            if (order != null)
                            {
                                allOrders[task.OrderID] = order;
                            }
                        }

                        // Fetch UserName based on UserID
                        string userApiUrl = $"https://localhost:7112/odata/User/{task.UserID}";
                        HttpResponseMessage userResponse = await client.GetAsync(userApiUrl);

                        if (userResponse.IsSuccessStatusCode)
                        {
                            string userData = await userResponse.Content.ReadAsStringAsync();
                            var user = JsonSerializer.Deserialize<UserVM>(userData, options);
                            if (user != null)
                            {
                                task.UserName = user.UserName; // Populate UserName
                            }
                        }
                    }

                    var filteredTasks = taskUserList.Where(task =>
                    {
                        if (allOrders.TryGetValue(task.OrderID, out var order))
                        {
                            bool includeTask = order.Status != true && order.Status != false;
                            return includeTask;
                        }
                        return false;
                    }).ToList();

                    TaskUserVM = filteredTasks;
                    OrderDetails = allOrders;
                }
                else
                {
                    TaskUserVM = new List<TaskUserVM>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing TaskUser list: {ex.Message}");
            }
        }
    }
}
