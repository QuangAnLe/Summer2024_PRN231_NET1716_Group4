using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.DTO.CommentDTO;
using ClientMilkTeamPage.Pages.AdminPage.OrderPage;
using ClientMilkTeamPage.Pages.ManagerPage.OrderPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ClientMilkTeamPage.Pages.UserPage.MyOrder
{
	public class OrderDetailModel : PageModel
	{
		private readonly HttpClient client = null!;
		private string ApiUrl = "";
		private readonly string OrderApiUrl;
		public Order OrderSummary { get; set; } = default!;
		public OrderEditViewModel OrderVM { get; set; }
		public OrderDetailModel()
		{
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			ApiUrl = "https://localhost:7112/odata/OrderDetailByOid";
			OrderApiUrl = "https://localhost:7112/odata/Order";
		}
		public IList<OrderDetail> orderdetails { get; set; } = default!;
		public string OrderUrl { get; set; }
		public async Task<IActionResult> OnGetAsync(int? id)
		{

			// Fetch order details
			HttpResponseMessage response = await client.GetAsync($"{ApiUrl}/{id}");
			string strData = await response.Content.ReadAsStringAsync();
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			var orderResp = JsonSerializer.Deserialize<List<OrderDetail>>(strData, options)!;
			orderdetails = orderResp;


			HttpResponseMessage responseOrder = await client.GetAsync($"{OrderApiUrl}/{id}");
			string strDataorderWithTaskUser = await responseOrder.Content.ReadAsStringAsync();
			var optionsorderWithTaskUser = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			var orderWithTaskUser = JsonSerializer.Deserialize<OrderWithTaskUserDTO>(strDataorderWithTaskUser, optionsorderWithTaskUser)!;

			OrderSummary = new Order
			{
				OrderID = orderWithTaskUser.Order.OrderID,
				EndDate = orderWithTaskUser.Order.EndDate,
				ReasonContent = orderWithTaskUser.Order.ReasonContent,
				ShipAddress = orderWithTaskUser.Order.ShipAddress,
				Status = orderWithTaskUser.Order.Status,
				TypeOrder = orderWithTaskUser.Order.TypeOrder,
				UserID = orderWithTaskUser.Order.UserID,
				StartDate = orderWithTaskUser.Order.StartDate
			};

			if (OrderSummary.Status == null)
			{
				HttpResponseMessage response1 = await client.PostAsync($"https://localhost:7112/odata/CreateOrder/{id}", null);
				var jsonResponse = await response1.Content.ReadAsStringAsync();
				var responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonResponse);
				if (responseData.ContainsKey("order_url"))
				{
					OrderUrl = responseData["order_url"].ToString();
				}
			}
			return Page();
		}

	}
}
