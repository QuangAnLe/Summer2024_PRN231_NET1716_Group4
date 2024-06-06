using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MilkTeaBusinessObject.BusinessObject;

namespace ClientMilkTeaStore.Pages.UserPage.CommentPage
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
        ViewData["TeaID"] = new SelectList(_context.Teas, "TeaID", "Image");
        ViewData["UserID"] = new SelectList(_context.Users, "UserID", "DistrictID");
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Comments == null || Comment == null)
            {
                return Page();
            }

            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
