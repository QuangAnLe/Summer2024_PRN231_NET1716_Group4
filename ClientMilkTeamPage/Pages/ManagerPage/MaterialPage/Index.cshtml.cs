using ClientMilkTeamPage.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.ManagerPage.MaterialPage
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
            ApiUrl = "https://localhost:7112/odata/Material";
        }

        public IList<Material> Material { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(ApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Material> materials = JsonSerializer.Deserialize<List<Material>>(strData, options)!;

            Material = materials;

            return Page();
        }
    }
}
