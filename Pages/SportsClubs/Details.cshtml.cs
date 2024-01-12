using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingStudio02.Data;
using TrainingStudio02.Models;

namespace TrainingStudio02.Pages.FitnessClasses
{
    public class DetailsModel : PageModel
    {
        private readonly TrainingStudio02.Data.TrainingStudio02Context _context;

        public DetailsModel(TrainingStudio02.Data.TrainingStudio02Context context)
        {
            _context = context;
        }

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
    }
}
