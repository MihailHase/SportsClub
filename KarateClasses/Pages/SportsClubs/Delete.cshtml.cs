using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsCLubClasses.Data;
using SportsCLubClasses.Models;

namespace SportsCLubClasses.Pages.SportsCLubs
{
    public class DeleteModel : PageModel
    {
        private readonly SportsCLubClasses.Data.SportsCLubClassesContext _context;

        public DeleteModel(SportsCLubClasses.Data.SportsCLubClassesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SportsCLub SportsCLub { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SportsCLub == null)
            {
                return NotFound();
            }

            SportsCLub = await _context.SportsCLub.FirstOrDefaultAsync(m => m.ID == id);

            if (SportsCLub == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SportsCLub == null)
            {
                return NotFound();
            }

            SportsCLub = await _context.SportsCLub.FindAsync(id);

            if (SportsCLub != null)
            {
                _context.SportsCLub.Remove(SportsCLub);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
