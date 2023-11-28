using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using orgcat.domain;

namespace orgcat.web.Pages.Dummy
{
    public class DetailsModel : PageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private readonly IOrgCatStorage _storage;

        public DetailsModel(IOrgCatStorage storage)
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
            else 
            {
                Name = dummy.Name;
            }
            return Page();
        }
    }
}
