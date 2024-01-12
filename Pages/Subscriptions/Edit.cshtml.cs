using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingStudio02.Data;
using TrainingStudio02.Models;

namespace TrainingStudio02.Pages.Subscriptions
{
    public class EditModel : PageModel
    {
        private readonly TrainingStudio02.Data.TrainingStudio02Context _context;

        public EditModel(TrainingStudio02.Data.TrainingStudio02Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Subscription Subscription { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Subscription == null)
            {
                return NotFound();
            }

            var subscription =  await _context.Subscription.FirstOrDefaultAsync(m => m.ID == id);
            if (subscription == null)
            {
                return NotFound();
            }
            Subscription = subscription;

            var fitnessClassList = _context.FitnessClass
                 .Include(b => b.Trainer)
                 .Select(x => new
                 {
                     x.ID,
                     FitnessClassFullName = x.Name + " - " + x.Trainer.FirstName + " "
                     + x.Trainer.LastName
                 });

            ViewData["FitnessClassID"] = new SelectList(fitnessClassList, "ID", "FitnessClassFullName");
            ViewData["MemberID"] = new SelectList(_context.Member, "ID", "FullName");
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

            _context.Attach(Subscription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(Subscription.ID))
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

        private bool SubscriptionExists(int id)
        {
          return _context.Subscription.Any(e => e.ID == id);
        }
    }
}
