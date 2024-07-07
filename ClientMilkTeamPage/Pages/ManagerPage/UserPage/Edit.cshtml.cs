using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.DTO.UserDTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.ManagerPage.UserPage
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client;
        private string ApiUrl = "";

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/User";
        }

        [BindProperty]
        public UserUpdateDTO User { get; set; } = default!;

        public List<SelectListItem> Roles { get; set; }
        public IList<RoleVM> Role { get; set; } = default!;

        public List<SelectListItem> Districts { get; set; }
        public IList<DistrictVM> District { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var _user = JsonSerializer.Deserialize<UserUpdateDTO>(strData, options)!;

            User = _user;

            //Role
            HttpResponseMessage roleResponse = await client.GetAsync("https://localhost:7112/api/Role");
            if (roleResponse.IsSuccessStatusCode)
            {
                string roleData = await roleResponse.Content.ReadAsStringAsync();
                List<RoleVM> roles = JsonSerializer.Deserialize<List<RoleVM>>(roleData, options);

                Roles = new List<SelectListItem>();
                foreach (var role in roles)
                {
                    Roles.Add(new SelectListItem { Value = role.RoleID.ToString(), Text = role.RoleName });
                }
            }

            //District
            HttpResponseMessage districtResponse = await client.GetAsync("https://localhost:7112/odata/District");
            if (roleResponse.IsSuccessStatusCode)
            {
                string districtData = await districtResponse.Content.ReadAsStringAsync();
                List<DistrictVM> districts = JsonSerializer.Deserialize<List<DistrictVM>>(districtData, options);

                Districts = new List<SelectListItem>();
                foreach (var district in districts)
                {
                    Districts.Add(new SelectListItem { Value = district.DistrictID.ToString(), Text = district.DistrictName + " - " + district.WardName });
                }
            }


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                string strData = JsonSerializer.Serialize(User);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{ApiUrl}/{id}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Success"] = "Update Success";
                    return RedirectToPage("./Index");
                }
                ViewData["Error"] = "Update Error";
                return RedirectToPage("./Index");
            }
            catch
            {
                ViewData["Error"] = "Fail To Call API";
                return RedirectToPage("/Error");
            }
        }
    }
}
