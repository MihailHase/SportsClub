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
    public class IndexModel : PageModel
    {
        private readonly SportsCLubClasses.Data.SportsCLubClassesContext _context;

        public IndexModel(SportsCLubClasses.Data.SportsCLubClassesContext context)
        {
            _context = context;
        }

        public IList<SportsCLub> SportsCLub { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SportsCLub != null)
            {
                SportsCLub = await _context.SportsCLub
                .Include(k => k.Location)
                .Include(k => k.Trainer).ToListAsync();
            }
        }
    }
}
