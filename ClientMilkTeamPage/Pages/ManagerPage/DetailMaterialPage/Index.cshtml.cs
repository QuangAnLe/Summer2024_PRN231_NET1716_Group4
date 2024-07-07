using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.ManagerPage.DetailMaterialPage
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
            ApiUrl = "https://localhost:7112/odata/DetailsMaterial";
        }

        public IList<DetailsMaterialVM> DetailMaterial { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<DetailsMaterialVM> materials = JsonSerializer.Deserialize<List<DetailsMaterialVM>>(strData, options)!;

            DetailMaterial = materials;

            return Page();
        }
    }
}
