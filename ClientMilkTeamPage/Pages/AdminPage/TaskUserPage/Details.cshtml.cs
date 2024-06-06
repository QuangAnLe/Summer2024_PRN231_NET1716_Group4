using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkTeaBusinessObject.BusinessObject;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.AdminPage.TaskUserPage
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string ApiUrl = "";

        public DetailsModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/TaskUser";
        }

        public TaskUser TaskUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var _taskUser = JsonSerializer.Deserialize<TaskUser>(strData, options)!;

            TaskUser = _taskUser;
            return Page();
        }
    }
}
