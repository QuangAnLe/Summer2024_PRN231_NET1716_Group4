using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MilkTeaStore.ViewModels;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeaStore.Pages.AdminPage.UserPage
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client;
        private string ApiUrl = string.Empty;

        public DetailsModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/User";
        }

        public UserVM User { get; set; } = default!;

        public string Admin { get; private set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                Admin = HttpContext.Session.GetString("Admin")!;
                if (Admin != "Admin")
                {
                    return NotFound();
                }
                if (Admin == null)
                {
                    return NotFound();
                }
            }
            catch
            {
                NotFound();
            }
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var _user = JsonSerializer.Deserialize<UserVM>(strData, options)!;

            User = _user;
            return Page();
        }
    }
}
