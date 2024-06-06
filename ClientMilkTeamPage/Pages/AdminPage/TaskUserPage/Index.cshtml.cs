using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkTeaBusinessObject.BusinessObject;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.AdminPage.TaskUserPage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string ApiUrl = "";

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/TaskUser";
        }

        public IList<TaskUser> TaskUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<TaskUser> taskUsers = JsonSerializer.Deserialize<List<TaskUser>>(strData, options)!;

            TaskUser = taskUsers;

            return Page();
        }
    }
}
