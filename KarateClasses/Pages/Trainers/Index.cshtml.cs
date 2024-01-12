using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportsCLubClasses.Data;
using SportsCLubClasses.Models;
using SportsCLubClasses.Models.ViewModels;

namespace SportsCLubClasses.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly SportsCLubClasses.Data.SportsCLubClassesContext _context;

        public IndexModel(SportsCLubClasses.Data.SportsCLubClassesContext context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        public TrainerIndexData TrainerData { get; set; }
        public int TrainerID { get; set; }
        public int SportsCLubClassID { get; set; }
        public async Task OnGetAsync(int? id, int? SportsCLubClassesID)
        {
            TrainerData = new TrainerIndexData();
            TrainerData.Trainers = await _context.Trainer
                .Include(i => i.SportsCLub)
                .ThenInclude(c => c.Location)
                .OrderBy(i => i.LastName) //Nu poti face referinta dupa Full Name pentru ca nu e in baza de date
                .ToListAsync();
            if (id != null)
            {
                TrainerID = id.Value;
                Trainer trainer = TrainerData.Trainers
                    .Where(i => i.ID == id.Value).Single();
                TrainerData.SportsCLub = trainer.SportsCLub;
            }
        }
    }
}
