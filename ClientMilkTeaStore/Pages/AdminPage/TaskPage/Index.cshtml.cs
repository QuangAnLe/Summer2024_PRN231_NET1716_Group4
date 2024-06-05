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
    public class IndexModel : PageModel
    {
        private readonly MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext _context;

        public IndexModel(MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext context)
        {
            _context = context;
        }

        public IList<TaskUser> TaskUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TaskUsers != null)
            {
                TaskUser = await _context.TaskUsers
                .Include(t => t.Order)
                .Include(t => t.User).ToListAsync();
            }
        }
    }
}
