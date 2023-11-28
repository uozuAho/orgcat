using Microsoft.AspNetCore.Mvc.RazorPages;
using orgcat.domain;

namespace orgcat.web.Pages.Dummy
{
    public class IndexModel : PageModel
    {
        private readonly IOrgCatStorage _storage;

        public IndexModel(IOrgCatStorage storage)
        {
            _storage = storage;
        }

        public IList<ExistingDummy> Dummies { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Dummies = await _storage.LoadAllDummies();
        }
    }
}
