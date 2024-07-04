using Microsoft.AspNetCore.Mvc;

namespace ClientMilkTeamPage.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            // Chuyển hướng đến trang Index
            return RedirectToPage("/AdminPage/OrderPage/Index");
        }

        public IActionResult Edit(int id)
        {
            // Chuyển hướng đến trang Edit
            return RedirectToPage("/AdminPage/OrderPage/Edit", new { id = id });
        }
        public IActionResult Details(int id)
        {
            // Chuyển hướng đến trang Edit
            return RedirectToPage("/AdminPage/OrderPage/Details", new { id = id });
        }

        public IActionResult Delete(int id)
        {
            // Chuyển hướng đến trang Delete
            return RedirectToPage("/AdminPage/OrderPage/Delete", new { id = id });
        }
    }
}
