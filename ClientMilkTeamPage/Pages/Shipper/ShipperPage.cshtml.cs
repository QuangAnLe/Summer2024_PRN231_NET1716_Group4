using ClientMilkTeamPage.DTO.TaskUserDTO;
using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using ClientMilkTeamPage.DTO.CommentDTO;

namespace ClientMilkTeamPage.Pages.Shipper
{
    public class ShipperPageModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string TaskApiUrl = "";
        private string CommentApiUrl = "";

        public ShipperPageModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            TaskApiUrl = "https://localhost:7112/odata/TaskUser";
            CommentApiUrl = "https://localhost:7112/odata/Comment";
        }

        public IList<TaskUserVM> TaskUserVM { get; set; } = default!;

        [BindProperty]
        public bool ShowModal { get; set; } = false;

        [BindProperty]
        public int CurrentTaskId { get; set; }

        [BindProperty]
        public int CurrentTeaID { get; set; }

        [BindProperty]
        public int CurrentUserID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(TaskApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<TaskUser> taskUsers = JsonSerializer.Deserialize<List<TaskUser>>(strData, options)!;

            TaskUserVM = taskUsers.Select(t => new TaskUserVM
            {
                TaskId = t.TaskId,
                WorkName = t.WorkName,
                WorkDescription = t.WorkDescription,
                Status = t.Status,
                UserID = t.UserID,
                OrderID = t.OrderID
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatus(int TaskId, string Status, int TeaID, int UserID)
        {
            if (Status == "Failed")
            {
                ShowModal = true;
                CurrentTaskId = TaskId;
                CurrentTeaID = TeaID;
                CurrentUserID = UserID;
            }
            else
            {
                // Handle success update immediately
                var taskUserUpdate = new TaskUserUpdateDTO
                {
                    TaskId = TaskId,
                    Status = true // Assuming true for success
                };
                await UpdateTaskStatus(taskUserUpdate);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSubmitFailureReason(string FailureReason)
        {
            var taskUserUpdate = new TaskUserUpdateDTO
            {
                TaskId = CurrentTaskId,
                Status = false, // Assuming false for failure
            };

            await UpdateTaskStatus(taskUserUpdate);

            // Create a new comment without TaskId
            var commentCreateDTO = new CommentCreateDTO
            {
                Content = FailureReason,
                CommentDate = DateTime.UtcNow,
                Rating = 0, // Assuming rating is not applicable here
                TeaID = CurrentTeaID,
                UserID = CurrentUserID
            };

            await CreateComment(commentCreateDTO);

            ShowModal = false;
            return RedirectToPage();
        }

        private async Task UpdateTaskStatus(TaskUserUpdateDTO taskUserUpdate)
        {
            var json = JsonSerializer.Serialize(taskUserUpdate);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await client.PutAsync($"{TaskApiUrl}/{taskUserUpdate.TaskId}", content);
        }

        private async Task CreateComment(CommentCreateDTO commentCreateDTO)
        {
            var json = JsonSerializer.Serialize(commentCreateDTO);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            await client.PostAsync(CommentApiUrl, content);
        }
    }
}