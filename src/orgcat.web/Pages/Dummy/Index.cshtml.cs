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
    public class IndexModel : PageModel
    {
        private readonly orgcat.postgresdb.OrgCatDb _context;

        public IndexModel(orgcat.postgresdb.OrgCatDb context)
        {
            _context = context;
        }

        public IList<Dummy> Dummy { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Dummies != null)
            {
                Dummy = await _context.Dummies.ToListAsync();
            }
        }
    }
}
