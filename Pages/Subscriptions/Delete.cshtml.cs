using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingStudio02.Data;
using TrainingStudio02.Models;

namespace TrainingStudio02.Pages.Subscriptions
{
    public class DeleteModel : PageModel
    {
        private readonly TrainingStudio02.Data.TrainingStudio02Context _context;

        public DeleteModel(TrainingStudio02.Data.TrainingStudio02Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Subscription Subscription { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Subscription == null)
            {
                return NotFound();
            }

            var subscription = await _context.Subscription.FirstOrDefaultAsync(m => m.ID == id);

            if (subscription == null)
            {
                return NotFound();
            }
            else 
            {
                Subscription = subscription;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Subscription == null)
            {
                return NotFound();
            }
            var subscription = await _context.Subscription.FindAsync(id);

            if (subscription != null)
            {
                Subscription = subscription;
                _context.Subscription.Remove(Subscription);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
