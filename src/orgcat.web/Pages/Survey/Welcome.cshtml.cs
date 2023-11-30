using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace orgcat.web.Pages.Survey;

[BindProperties(SupportsGet = true)]
public class Welcome : PageModel
{
    public string ResponseId { get; set; } = string.Empty;
    public bool InProgress { get; set; }

    public string WelcomeMessage { get; set; } = string.Empty;

    public string StartButtonText { get; set; } = string.Empty;

    public void OnGet()
    {
        WelcomeMessage = InProgress
            ? "Welcome back! Ready to answer more questions?"
            : "Ready to start answering some questions?";

        StartButtonText = InProgress ? "Yep" : "Let's a-go!";
    }

    public Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(
            RedirectToPage("./Question", new {surveyResponseId=ResponseId}));
    }
}
