using ClientMilkTeamPage.DTO.DistrictDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.ManagerPage.DistrictPage
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string ApiUrl = "";

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/District";
        }

        [BindProperty]
        public DistrictUpdateDTO District { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var district = JsonSerializer.Deserialize<DistrictUpdateDTO>(strData, options)!;

            District = district;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                string strData = JsonSerializer.Serialize(District);
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
