using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using orgcat.domain;

namespace orgcat.web.Pages.Dummy
{
    public class EditModel : PageModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private readonly IOrgCatStorage _storage;

        public EditModel(IOrgCatStorage storage)
        {
            _storage = storage;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dummy =  await _storage.FindDummy(id.Value);
            if (dummy == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(Page());
            }
            
            // todo: update dummy

            return Task.FromResult<IActionResult>(RedirectToPage("./Index"));
        }

        private bool DummyExists(int id)
        {
            return (_storage.FindDummy(id) != null);
        }
    }
}
