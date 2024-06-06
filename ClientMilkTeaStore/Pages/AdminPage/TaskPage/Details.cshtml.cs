using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace ClientMilkTeaStore.Pages.AdminPage.TaskPage
{
    public class DetailsModel : PageModel
    {
        private readonly MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext _context;

        public DetailsModel(MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext context)
        {
            _context = context;
        }

      public TaskUser TaskUser { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TaskUsers == null)
            {
                return NotFound();
            }

            var taskuser = await _context.TaskUsers.FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskuser == null)
            {
                return NotFound();
            }
            else 
            {
                TaskUser = taskuser;
            }
            return Page();
        }
    }
}
