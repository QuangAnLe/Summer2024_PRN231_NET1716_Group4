using ClientMilkTeamPage.DTO;
using ClientMilkTeamPage.Pages.AdminPage.OrderPage;
using ClientMilkTeamPage.ViewModel;
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
        public SelectList ShipperList { get; set; }

        [BindProperty]
        public OrderEditViewModel OrderVM { get; set; }
        public int taskId { get; set; }

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
            var orderWithTaskUser = JsonSerializer.Deserialize<OrderWithTaskUserDTO>(strData, options)!;

            OrderVM = new OrderEditViewModel
            {
                OrderID = orderWithTaskUser.Order.OrderID,
                TypeOrder = orderWithTaskUser.Order.TypeOrder,
                ReasonContent = orderWithTaskUser.Order.ReasonContent,
                Status = orderWithTaskUser.Order.Status,
                StartDate = orderWithTaskUser.Order.StartDate,
                EndDate = orderWithTaskUser.Order.EndDate,
                ShipAddress = orderWithTaskUser.Order.ShipAddress,
                UserID = orderWithTaskUser.Order.UserID,
                ShipperID = orderWithTaskUser.TaskUser?.UserID ?? 0
            };
            taskId = orderWithTaskUser.TaskUser.TaskID;

            await LoadUserList();
            await LoadShipperList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadUserList();
                await LoadShipperList();
                return Page();
            }

            var orderDTO = new OrderDTO
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

            var taskUserDTO = new TaskUserDTO
            {
                TaskID = taskId,
                OrderID = OrderVM.OrderID,
                UserID = OrderVM.ShipperID,
                WorkName = "Shipping",
                WorkDescription = "Order Delivery",
                Status = true
            };

            var orderWithTaskUser = new OrderWithTaskUserDTO
            {
                Order = orderDTO,
                TaskUser = taskUserDTO
            };

            var content = new StringContent(JsonSerializer.Serialize(orderWithTaskUser), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{OrderApiUrl}/{OrderVM.OrderID}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/AdminPage/OrderPage/Index");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Update failed: {errorMessage}");
                await LoadUserList();
                await LoadShipperList();
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

        private async Task LoadShipperList()
        {
            HttpResponseMessage shipperResponse = await client.GetAsync($"{UserApiUrl}/Shippers");
            string shipperData = await shipperResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var shippers = JsonSerializer.Deserialize<List<User>>(shipperData, options);
            ShipperList = new SelectList(shippers, "UserID", "FullName");
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

        [Required(ErrorMessage = "Shipper is required")]
        public int ShipperID { get; set; }
    }
}
