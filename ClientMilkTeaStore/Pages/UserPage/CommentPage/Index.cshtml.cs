using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace ClientMilkTeaStore.Pages.UserPage.CommentPage
{
    public class IndexModel : PageModel
    {
        private readonly MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext _context;

        public IndexModel(MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext context)
        {
            _context = context;
        }

        public IList<Comment> Comment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Comments != null)
            {
                Comment = await _context.Comments
                .Include(c => c.Tea)
                .Include(c => c.User).ToListAsync();
            }
        }
    }
}
