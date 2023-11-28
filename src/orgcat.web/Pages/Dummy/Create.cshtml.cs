using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using orgcat.postgresdb;
using orgcat.postgresdb.Entities;

namespace orgcat.web.Pages.Dummy
{
    public class CreateModel : PageModel
    {
        private readonly orgcat.postgresdb.OrgCatDb _context;

        public CreateModel(orgcat.postgresdb.OrgCatDb context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Dummy Dummy { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Dummies == null || Dummy == null)
            {
                return Page();
            }

            _context.Dummies.Add(Dummy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
