using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using ClientMilkTeamPage.DTO;

namespace ClientMilkTeamPage.Services
{
    public class MaterialService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7112/odata/Material"; 
        public MaterialService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Material>> GetMaterialsAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Material>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Material> GetMaterialByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Material>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<int> GetMaterialsCountAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/$count");
            response.EnsureSuccessStatusCode();
            var countString = await response.Content.ReadAsStringAsync();
            return int.Parse(countString);
        }

        public async Task AddMaterialAsync(Material material)
        {
            var materialJson = JsonSerializer.Serialize(material);
            var content = new StringContent(materialJson, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateMaterialAsync(Material material)
        {
            var materialJson = JsonSerializer.Serialize(material);
            var content = new StringContent(materialJson, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_apiUrl}/{material.MaterialID}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMaterialAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
