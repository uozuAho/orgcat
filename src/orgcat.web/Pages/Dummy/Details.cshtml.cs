using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using orgcat.postgresdb;
using orgcat.postgresdb.Entities;

namespace orgcat.web.Pages.Dummy
{
    public class DetailsModel : PageModel
    {
        private readonly orgcat.postgresdb.OrgCatDb _context;

        public DetailsModel(orgcat.postgresdb.OrgCatDb context)
        {
            _context = context;
        }

      public Dummy Dummy { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dummies == null)
            {
                return NotFound();
            }

            var dummy = await _context.Dummies.FirstOrDefaultAsync(m => m.Id == id);
            if (dummy == null)
            {
                return NotFound();
            }
            else 
            {
                Dummy = dummy;
            }
            return Page();
        }
    }
}
