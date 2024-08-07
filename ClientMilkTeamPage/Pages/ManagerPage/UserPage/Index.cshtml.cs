﻿using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.ManagerPage.UserPage
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
            ApiUrl = "https://localhost:7112/odata/User";
        }

        public IList<UserVM> User { get; set; } = default!;

        public string Admin { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {

            var token = HttpContext.Request.Cookies["ManagerCookie"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login"); // Không tìm thấy token trong cookie
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token) as JwtSecurityToken;
            var roleClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim?.Value == "4")
            {
                HttpResponseMessage response = await client.GetAsync(ApiUrl);
                string strData = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<UserVM> users = JsonSerializer.Deserialize<List<UserVM>>(strData, options)!;

                User = users.Where(u => u.RoleName == "Shipper" || u.RoleName == "Shipper" || u.RoleName == "Shipper").ToList();

                return Page();
            }

            return Page();

        }
    }
}
