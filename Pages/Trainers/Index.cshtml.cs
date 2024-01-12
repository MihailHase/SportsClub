using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrainingStudio02.Data;
using TrainingStudio02.Models;
using TrainingStudio02.Models.ViewModels;

namespace TrainingStudio02.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly TrainingStudio02.Data.TrainingStudio02Context _context;

        public IndexModel(TrainingStudio02.Data.TrainingStudio02Context context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        public TrainerIndexData TrainerData { get; set; }
        public int TrainerID { get; set; }
        public int FitnessClassID { get; set; }
        public async Task OnGetAsync(int? id, int? fitnessClassID)
        {
            TrainerData = new TrainerIndexData();
            TrainerData.Trainers = await _context.Trainer
                .Include(i => i.FitnessClasses)
                .ThenInclude(c => c.Location)
                .OrderBy(i => i.LastName) //Nu poti face referinta dupa Full Name pentru ca nu e in baza de date
                .ToListAsync();
            if (id != null)
            {
                TrainerID = id.Value;
                Trainer trainer = TrainerData.Trainers
                    .Where(i => i.ID == id.Value).Single();
                TrainerData.FitnessClasses = trainer.FitnessClasses;
            }
        }
    }
}
