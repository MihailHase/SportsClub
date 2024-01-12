using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsCLubClasses.Data;
using SportsCLubClasses.Models;

namespace SportsCLubClasses.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly SportsCLubClasses.Data.SportsCLubClassesContext _context;

        public CreateModel(SportsCLubClasses.Data.SportsCLubClassesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Trainer Trainer { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Trainer.Add(Trainer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
