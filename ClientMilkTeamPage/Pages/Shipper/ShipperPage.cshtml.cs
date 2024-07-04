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
using System.Text.Json;

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
        }

        public IList<TaskUserVM> TaskUserVM { get; set; }
        public bool ShowModal { get; set; } = false;

        [BindProperty]
        public int TaskId { get; set; }

        [BindProperty]
        public string Status { get; set; }

        [BindProperty]
        public string FailureReason { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string apiUrl = "https://localhost:7112/odata/TaskUser";
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                TaskUserVM = JsonSerializer.Deserialize<List<TaskUserVM>>(strData, options) ?? new List<TaskUserVM>();
            }
            else
            {
                TaskUserVM = new List<TaskUserVM>(); // Initialize to an empty list if the API call fails
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync()
        {
            if (Status == "Failed")
            {
                ShowModal = true;
                return Page();
            }

            await UpdateTaskStatusAsync(TaskId, Status == "Success");
            return RedirectToPage("/UserPage/MyOrder/OrderList");
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
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage commentResponse = await client.PostAsync(apiUrl, contentData);

            if (commentResponse.IsSuccessStatusCode)
            {
                await UpdateTaskStatusAsync(TaskId, false);
            }

            return RedirectToPage("/UserPage/MyOrder/OrderList");
        }

        private async Task UpdateTaskStatusAsync(int taskId, bool status)
        {
            var taskUpdateStatusDTO = new TaskUserUpdateStatusDTO
            {
                TaskId = taskId,
                Status = status
            };

            string apiUrl = $"https://localhost:7112/odata/TaskUser/{taskId}";
            string strData = JsonSerializer.Serialize(taskUpdateStatusDTO);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PatchAsync(apiUrl, contentData);

            if (response.IsSuccessStatusCode)
            {
                // Fetch the updated data
                await OnGetAsync();
            }
        }
    }
}
