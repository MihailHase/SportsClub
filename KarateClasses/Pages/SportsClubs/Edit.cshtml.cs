using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsCLubClasses.Data;
using SportsCLubClasses.Models;

namespace SportsCLubClasses.Pages.SportsCLubs
{
    public class EditModel : PageModel
    {
        private readonly SportsCLubClasses.Data.SportsCLubClassesContext _context;

        public EditModel(SportsCLubClasses.Data.SportsCLubClassesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SportsCLub SportsCLub { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SportsCLub = await _context.SportsCLub.FirstOrDefaultAsync(m => m.ID == id);

            if (SportsCLub == null)
            {
                return NotFound();
            }

            ViewData["LocationID"] = new SelectList(_context.Location, "ID", "Name");
            ViewData["TrainerID"] = new SelectList(_context.Trainer, "ID", "FullName");

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

            _context.Attach(SportsCLub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportsCLubExists(SportsCLub.ID))
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

        private bool SportsCLubExists(int id)
        {
            return _context.SportsCLub.Any(e => e.ID == id);
        }
    }
}
