using ClientMilkTeamPage.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientMilkTeamPage.Pages.ManagerPage.OrderPage
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string OrderApiUrl;
        private readonly string UserApiUrl;

        [BindProperty]
        public OrderEditViewModel OrderVM { get; set; }
        public SelectList UserList { get; set; }

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            OrderApiUrl = "https://localhost:7112/odata/Order";
            UserApiUrl = "https://localhost:7112/odata/User";
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{OrderApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var order = JsonSerializer.Deserialize<Order>(strData, options)!;

            OrderVM = new OrderEditViewModel
            {
                OrderID = order.OrderID,
                TypeOrder = order.TypeOrder,
                ReasonContent = order.ReasonContent,
                Status = order.Status,
                StartDate = order.StartDate,
                EndDate = order.EndDate,
                ShipAddress = order.ShipAddress,
                UserID = order.UserID
            };

            await LoadUserList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadUserList();
                return Page();
            }

            var order = new Order
            {
                OrderID = OrderVM.OrderID,
                TypeOrder = OrderVM.TypeOrder,
                ReasonContent = OrderVM.ReasonContent,
                Status = OrderVM.Status,
                StartDate = OrderVM.StartDate,
                EndDate = OrderVM.EndDate,
                ShipAddress = OrderVM.ShipAddress,
                UserID = OrderVM.UserID
            };

            var content = new StringContent(JsonSerializer.Serialize(order), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{OrderApiUrl}/{order.OrderID}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/AdminPage/OrderPage/Index");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Update failed: {errorMessage}");
                await LoadUserList();
                return Page();
            }
        }

        private async Task LoadUserList()
        {
            HttpResponseMessage userResponse = await client.GetAsync(UserApiUrl);
            string userData = await userResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var users = JsonSerializer.Deserialize<List<User>>(userData, options);
            UserList = new SelectList(users, "UserID", "UserName");
        }

        public string GetStatusDisplay(bool? status)
        {
            return status switch
            {
                null => "Processing",
                true => "Success",
                false => "Failed"
            };
        }
    }


    public class OrderEditViewModel
    {
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Type of order is required")]
        [Display(Name = "Type of Order")]
        public string TypeOrder { get; set; }

        [Display(Name = "Reason")]
        [StringLength(500, ErrorMessage = "Reason content cannot exceed 500 characters")]
        public string ReasonContent { get; set; }

        public bool? Status { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Ship address is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Ship address must be between 5 and 200 characters")]
        [Display(Name = "Ship Address")]
        public string ShipAddress { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserID { get; set; }

       
    }
}
