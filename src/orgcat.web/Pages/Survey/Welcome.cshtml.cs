﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace orgcat.web.Pages.Survey;

[BindProperties]
public class Welcome : PageModel
{
    public string ResponseId { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(
            RedirectToPage("./Question", new {surveyResponseId=ResponseId}));
    }
}
