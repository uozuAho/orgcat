using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using orgcat.domain;

namespace orgcat.web.Pages.Dummy
{
    public class CreateModel : PageModel
    {
        public string Name { get; set; } = string.Empty;
        
        private readonly IOrgCatStorage _storage;

        public CreateModel(IOrgCatStorage storage)
        {
            _storage = storage;
        }
        
        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(Page());
            }

            _storage.Add(new domain.NewDummy(Name));

            return Task.FromResult<IActionResult>(RedirectToPage("./Index"));
        }
    }
}
