using ClientMilkTeamPage.DTO.TaskUserDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.AdminPage.TaskUserPage
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string ApiUrl = "";

        public CreateModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/TaskUser";
        }

        [BindProperty]
        public TaskUserCreateDTO TaskUser { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string strData = JsonSerializer.Serialize(TaskUser);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ApiUrl, contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Add New Task successfully";
                    return RedirectToPage("./Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Error while adding new task";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Fail To Call API: {ex.Message}";
            }

            return Page();
        }
    }
}
