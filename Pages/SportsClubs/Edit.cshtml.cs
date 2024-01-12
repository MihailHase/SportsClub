using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingStudio02.Data;
using TrainingStudio02.Models;

namespace TrainingStudio02.Pages.FitnessClasses
{
    [Authorize(Roles = "Admin")]
    public class EditModel : FitnessClassCategoriesPageModel
    {
        private readonly TrainingStudio02.Data.TrainingStudio02Context _context;

        public EditModel(TrainingStudio02.Data.TrainingStudio02Context context)
        {
            _context = context;
        }

        [BindProperty]
        public FitnessClass FitnessClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FitnessClass == null)
            {
                return NotFound();
            }

            FitnessClass = await _context.FitnessClass
                .Include(b => b.Location)
                .Include(b => b.FitnessClassCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (FitnessClass == null)
            {
                return NotFound();
            }


            PopulateAssignedCategoryData(_context, FitnessClass);

            var trainerList = _context.Trainer.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });


            ViewData["TrainerID"] = new SelectList(trainerList, "ID", "FullName");
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
            selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }


            var fitnessClassToUpdate = await _context.FitnessClass
             .Include(i => i.Trainer)
             .Include(i => i.FitnessClassCategories)
             .ThenInclude(i => i.Category)
             .FirstOrDefaultAsync(s => s.ID == id);


            if (fitnessClassToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<FitnessClass>(
             fitnessClassToUpdate,
             "FitnessClass",
             i => i.Name, i => i.Trainer,
             i => i.Price, i => i.Duration, i => i.TrainerID))
            {
                UpdateFitnessClassCategories(_context, selectedCategories, fitnessClassToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }


            UpdateFitnessClassCategories(_context, selectedCategories, fitnessClassToUpdate);
            PopulateAssignedCategoryData(_context, fitnessClassToUpdate);
            return Page();


        }
    }
}
