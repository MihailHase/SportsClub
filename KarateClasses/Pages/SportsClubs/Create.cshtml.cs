using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsCLubClasses.Data;
using SportsCLubClasses.Models;

namespace SportsCLubClasses.Pages.SportsCLubs
{
    public class CreateModel : PageModel
    {
        private readonly SportsCLubClasses.Data.SportsCLubClassesContext _context;

        public CreateModel(SportsCLubClasses.Data.SportsCLubClassesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LocationID"] = new SelectList(_context.Location, "ID", "Name");
        ViewData["TrainerID"] = new SelectList(_context.Trainer, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public SportsCLub SportsCLub { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.SportsCLub == null || SportsCLub == null)
            {
                return Page();
            }

            _context.SportsCLub.Add(SportsCLub);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
