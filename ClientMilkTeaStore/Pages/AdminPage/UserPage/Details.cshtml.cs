using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using MilkTeaStore.ViewModels;

namespace ClientMilkTeaStore.Pages.AdminPage.UserPage
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client;
        private string ApiUrl = "";

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
