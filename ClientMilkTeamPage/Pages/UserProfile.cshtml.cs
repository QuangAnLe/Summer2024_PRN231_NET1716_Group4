using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Xml;

namespace ClientMilkTeamPage.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly HttpClient client;
        private string ApiUrl = "";

        public UserProfileModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/User";
        }
        [BindProperty]
        public UserVM User { get; set; } = default!;

        public int UserID;


        public async Task<IActionResult> OnGetAsync(int? id)
        {

            var token = HttpContext.Request.Cookies["UserCookie"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login"); // Không tìm thấy token trong cookie
            }
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token) as JwtSecurityToken;
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;



            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{userIdClaim}");
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
