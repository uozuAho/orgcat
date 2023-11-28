using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using orgcat.postgresdb;
using orgcat.postgresdb.Entities;

namespace orgcat.web.Pages.Dummy
{
    public class EditModel : PageModel
    {
        private readonly orgcat.postgresdb.OrgCatDb _context;

        public EditModel(orgcat.postgresdb.OrgCatDb context)
        {
            _context = context;
        }

        [BindProperty]
        public Dummy Dummy { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dummies == null)
            {
                return NotFound();
            }

            var dummy =  await _context.Dummies.FirstOrDefaultAsync(m => m.Id == id);
            if (dummy == null)
            {
                return NotFound();
            }
            Dummy = dummy;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Dummy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DummyExists(Dummy.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DummyExists(int id)
        {
          return (_context.Dummies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
