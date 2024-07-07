using ClientMilkTeamPage.DTO.CommentDTO;
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
            TaskUserVM = new List<TaskUserVM>(); // Initialize to avoid null reference
            OrderDetails = new Dictionary<int, OrderDTO>();
        }

        public IList<TaskUserVM> TaskUserVM { get; set; }
        public Dictionary<int, OrderDTO> OrderDetails { get; set; }
        public bool ShowModal { get; set; } = false;

        [BindProperty]
        public int TaskId { get; set; }

        [BindProperty]
        public string Status { get; set; }

        [BindProperty]
        public string FailureReason { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await RefreshTaskList(); // Ensure TaskUserVM reflects the latest from the server
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitFailureReasonAsync()
        {
            var commentCreateDTO = new CommentCreateDTO
            {
                Content = FailureReason,
                CommentDate = DateTime.UtcNow,
                Rating = 0, // Adjust as necessary
                TeaID = 0, // Adjust as necessary
                UserID = 0 // Adjust as necessary
            };

            string apiUrl = "https://localhost:7112/odata/Comment";
            string strData = JsonSerializer.Serialize(commentCreateDTO);
            var contentData = new StringContent(strData, Encoding.UTF8, "application/json");
            HttpResponseMessage commentResponse = await client.PostAsync(apiUrl, contentData);

            if (commentResponse.IsSuccessStatusCode)
            {
                await UpdateTaskStatusAsync(TaskId, false);
                await RefreshTaskList(); // Refresh the list after updating status
            }

            return RedirectToPage("/UserPage/MyOrder/OrderList");
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            if (Status == "Failed")
            {
                ShowModal = true;
                return Page();
            }

            var taskId = TaskId;
            var status = Status == "Success";
            await UpdateTaskStatusAsync(taskId, status);
            RemoveTaskFromList(taskId);
            await RefreshTaskList();
            return RedirectToPage("/UserPage/MyOrder/OrderList");
        }

        private async Task UpdateTaskStatusAsync(int taskId, bool status)
        {
            try
            {
                var taskUpdateStatusDTO = new TaskUserUpdateStatusDTO
                {
                    TaskId = taskId,
                    Status = status
                };

                string apiUrl = $"https://localhost:7112/odata/TaskUser/{taskId}";
                string strData = JsonSerializer.Serialize(taskUpdateStatusDTO);
                var contentData = new StringContent(strData, Encoding.UTF8, "application/json");

                // Send PATCH request
                var response = await client.PatchAsync(apiUrl, contentData);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to update TaskUser status for Task: {taskId}");
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

                    // Fetch orders and filter tasks based on order status
                    var filteredTasks = new List<TaskUserVM>();
                    foreach (var task in taskUserList)
                    {
                        string orderApiUrl = $"https://localhost:7112/odata/Order/{task.OrderID}";
                        HttpResponseMessage orderResponse = await client.GetAsync(orderApiUrl);

                        if (orderResponse.IsSuccessStatusCode)
                        {
                            string orderData = await orderResponse.Content.ReadAsStringAsync();
                            var order = JsonSerializer.Deserialize<OrderDTO>(orderData, options);

                            // Filter out tasks with order status "Success" or "Failed"
                            if (order != null && order.Status != true && order.Status != false)
                            {
                                filteredTasks.Add(task);
                                OrderDetails[task.OrderID] = order; // Store order details
                            }
                        }
                    }

                    TaskUserVM = filteredTasks;
                }
                else
                {
                    TaskUserVM = new List<TaskUserVM>(); // Initialize to an empty list if the API call fails
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing TaskUser list: {ex.Message}");
            }
        }

        private void RemoveTaskFromList(int taskId)
        {
            try
            {
                var taskToRemove = TaskUserVM.FirstOrDefault(t => t.TaskId == taskId);
                if (taskToRemove != null)
                {
                    TaskUserVM.Remove(taskToRemove);
                    Console.WriteLine($"Task removed from TaskUserVM: {taskId}");
                }
                else
                {
                    Console.WriteLine($"Task not found in TaskUserVM: {taskId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing task from TaskUserVM: {ex.Message}");
            }
        }

    }
}
