using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MilkTeaBusinessObject.BusinessObject;

namespace ClientMilkTeaStore.Pages.AdminPage.TaskPage
{
    public class CreateModel : PageModel
    {
        private readonly MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext _context;

        public CreateModel(MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "ReasonContent");
        ViewData["UserID"] = new SelectList(_context.Users, "UserID", "DistrictID");
            return Page();
        }

        [BindProperty]
        public TaskUser TaskUser { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TaskUsers == null || TaskUser == null)
            {
                return Page();
            }

            _context.TaskUsers.Add(TaskUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
