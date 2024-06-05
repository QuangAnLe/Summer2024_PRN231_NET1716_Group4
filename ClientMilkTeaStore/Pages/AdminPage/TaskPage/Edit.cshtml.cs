using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace ClientMilkTeaStore.Pages.AdminPage.TaskPage
{
    public class EditModel : PageModel
    {
        private readonly MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext _context;

        public EditModel(MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskUser TaskUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TaskUsers == null)
            {
                return NotFound();
            }

            var taskuser =  await _context.TaskUsers.FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskuser == null)
            {
                return NotFound();
            }
            TaskUser = taskuser;
           ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "ReasonContent");
           ViewData["UserID"] = new SelectList(_context.Users, "UserID", "DistrictID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TaskUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskUserExists(TaskUser.TaskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TaskUserExists(int id)
        {
          return (_context.TaskUsers?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
