using ClientMilkTeamPage.DTO;
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
            var commentCreateDTO = new CommentCreateDTO
            {
                Content = FailureReason,
                CommentDate = DateTime.UtcNow,
                Rating = 0,
                TeaID = 0,
                UserID = 0
            };

            string apiUrl = "https://localhost:7112/odata/Comment";
            string strData = JsonSerializer.Serialize(commentCreateDTO);
            var contentData = new StringContent(strData, Encoding.UTF8, "application/json");
            HttpResponseMessage commentResponse = await client.PostAsync(apiUrl, contentData);

            if (commentResponse.IsSuccessStatusCode)
            {
                if (Status.HasValue && Status.Value == false)
                {
                    await UpdateTaskStatusAsync(TaskId, false);
                }
                await RefreshTaskList();
            }

            return RedirectToPage("/UserPage/MyOrder/OrderList");
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            if (Status.HasValue && Status.Value == false)
            {
                ShowModal = true;
                return Page();
            }

            var taskId = TaskId;
            if (Status.HasValue && Status.Value == true)
            {
                await UpdateTaskStatusAsync(taskId, true);
              
                await RefreshTaskList();
            }

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
                // Fetch tasks
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

                    // Log fetched tasks
                    Console.WriteLine("Fetched Tasks:");
                    foreach (var task in taskUserList)
                    {
                        Console.WriteLine($"TaskId: {task.TaskId}, OrderID: {task.OrderID}");
                    }

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

                                // Log fetched orders
                                Console.WriteLine($"OrderID: {order.OrderID}, Status: {order.Status}, ReasonContent: {order.ReasonContent}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to fetch order for TaskId: {task.TaskId}");
                        }
                    }

                    var filteredTasks = taskUserList.Where(task =>
                    {
                        if (allOrders.TryGetValue(task.OrderID, out var order))
                        {
                            bool includeTask = order.Status != true && order.Status != false;
                            Console.WriteLine($"TaskId: {task.TaskId}, OrderID: {task.OrderID}, Include: {includeTask}");
                            return includeTask;
                        }
                        return false;
                    }).ToList();

                    TaskUserVM = filteredTasks;
                    OrderDetails = allOrders;
                    Console.WriteLine($"Filtered task list count: {TaskUserVM.Count}");
                }
                else
                {
                    TaskUserVM = new List<TaskUserVM>();
                    Console.WriteLine("Failed to retrieve task user list.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing TaskUser list: {ex.Message}");
            }
        }


        
    }
}
