﻿using System;
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

namespace ClientMilkTeaStore.Pages.AdminPage.TeaPage
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client = null!;
        private string ApiUrl = "";

        public DetailsModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = "https://localhost:7112/odata/Tea";
        }

      public Tea Tea { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var _tea = JsonSerializer.Deserialize<Tea>(strData, options)!;

            Tea = _tea;
            return Page();
        }
    }
}
