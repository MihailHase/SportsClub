using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingStudio02.Data;
using TrainingStudio02.Models;

namespace TrainingStudio02.Pages.FitnessClasses
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly TrainingStudio02.Data.TrainingStudio02Context _context;

        public DeleteModel(TrainingStudio02.Data.TrainingStudio02Context context)
        {
            _context = context;
        }

        [BindProperty]
      public FitnessClass FitnessClass { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FitnessClass == null)
            {
                return NotFound();
            }

            var fitnessclass = await _context.FitnessClass.FirstOrDefaultAsync(m => m.ID == id);

            if (fitnessclass == null)
            {
                return NotFound();
            }
            else 
            {
                FitnessClass = fitnessclass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FitnessClass == null)
            {
                return NotFound();
            }
            var fitnessclass = await _context.FitnessClass.FindAsync(id);

            if (fitnessclass != null)
            {
                FitnessClass = fitnessclass;
                _context.FitnessClass.Remove(FitnessClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
