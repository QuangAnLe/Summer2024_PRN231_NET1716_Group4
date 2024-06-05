using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace ClientMilkTeaStore.Pages.UserPage.CommentPage
{
    public class EditModel : PageModel
    {
        private readonly MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext _context;

        public EditModel(MilkTeaBusinessObject.BusinessObject.MilkTeaDeliveryDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comment Comment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment =  await _context.Comments.FirstOrDefaultAsync(m => m.CommentID == id);
            if (comment == null)
            {
                return NotFound();
            }
            Comment = comment;
           ViewData["TeaID"] = new SelectList(_context.Teas, "TeaID", "Image");
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

            _context.Attach(Comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(Comment.CommentID))
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

        private bool CommentExists(int id)
        {
          return (_context.Comments?.Any(e => e.CommentID == id)).GetValueOrDefault();
        }
    }
}
