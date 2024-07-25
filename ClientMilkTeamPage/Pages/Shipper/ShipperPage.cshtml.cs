using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.DTO.TaskUserDTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.Shipper
{
    public class ShipperPageModel : PageModel
    {
        private readonly HttpClient client;

        public ShipperPageModel(HttpClient client)
        {
            this.client = client;
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
                var allTasks = JsonSerializer.Deserialize<List<TaskUserVM>>(strData, options) ?? new List<TaskUserVM>();

                // Filter tasks by the current shipper's ID
                var shipperId = GetCurrentShipperId(); // Implement this to get the logged-in shipper's ID
                TaskUserVM = allTasks.Where(t => t.UserID == shipperId).ToList();
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

            await UpdateTaskStatusAsync(TaskId, true);
            return RedirectToPage("/UserPage/MyOrder/OrderList");
        }

        public async Task<IActionResult> OnPostSubmitFailureReasonAsync()
        {
            var taskUpdateStatusDTO = new TaskUserUpdateReasonDTO
            {
                TaskId = TaskId,
                Status = false,
                FailureReason = FailureReason
            };

            string apiUrl = $"https://localhost:7112/odata/TaskUser/{TaskId}/updatestatus/failure";
            string strData = JsonSerializer.Serialize(taskUpdateStatusDTO);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PatchAsync(apiUrl, contentData);

            if (response.IsSuccessStatusCode)
            {
                // Fetch the updated data
                await OnGetAsync();
            }

            return RedirectToPage("/UserPage/MyOrder/OrderList");
        }

        private async Task UpdateTaskStatusAsync(int taskId, bool status)
        {
            var taskUpdateStatusDTO = new TaskUserUpdateSuccessDTO
            {
                TaskId = taskId,
                Status = status
            };

            string apiUrl = $"https://localhost:7112/odata/TaskUser/{taskId}/updatestatus/success";
            string strData = JsonSerializer.Serialize(taskUpdateStatusDTO);
            var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PatchAsync(apiUrl, contentData);

            if (response.IsSuccessStatusCode)
            {
                // Fetch the updated data
                await OnGetAsync();
            }
        }

        private int GetCurrentShipperId()
        {
            // Ensure the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Retrieve the user ID from the claims
               
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
                if (int.TryParse(userIdClaim, out int userId))
                {
                    return userId;
                }
            }

            // Return a default value or handle the case where the user is not authenticated
            return 0; 
        }

    }
}
