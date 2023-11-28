using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using orgcat.domain;

namespace orgcat.web.Pages.Dummy
{
    public class DeleteModel : PageModel
    {
        public string Name { get; set; }
        public int Id { get; set; }

        private readonly IOrgCatStorage _storage;

        public DeleteModel(IOrgCatStorage storage)
        {
            _storage = storage;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dummy = await _storage.FindDummy(id.Value);

            if (dummy == null)
            {
                return NotFound();
            }
            
            Name = dummy.Name;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dummy = await _storage.FindDummy(id.Value);

            if (dummy != null)
            {
                _storage.DeleteDummy(id.Value);
            }

            return RedirectToPage("./Index");
        }
    }
}
