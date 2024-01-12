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

namespace SportsCLubClasses.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly SportsCLubClasses.Data.SportsCLubClassesContext _context;

        public DetailsModel(SportsCLubClasses.Data.SportsCLubClassesContext context)
        {
            _context = context;
        }

      public Member Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }
            else 
            {
                Member = member;
            }
            return Page();
        }

    }
}
