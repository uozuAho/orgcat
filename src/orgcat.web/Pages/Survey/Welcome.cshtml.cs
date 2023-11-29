using Microsoft.AspNetCore.Mvc.RazorPages;

namespace orgcat.web.Pages.Survey;

public class Welcome : PageModel
{
    public string SurveyId { get; set; } = string.Empty;
    
    public void OnGet()
    {
    }
}