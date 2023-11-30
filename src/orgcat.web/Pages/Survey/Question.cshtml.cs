using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using orgcat.domain;

namespace orgcat.web.Pages.Survey;

public class Question : PageModel
{
    private readonly IOrgCatStorage _storage;
    private readonly ISurveyService _surveyService;

    [BindProperty(SupportsGet = true)]
    public string SurveyResponseId { get; set; } = string.Empty;

    [BindProperty]
    [Required]
    [MinLength(1)]
    public string Answer { get; set; } = string.Empty;

    [BindProperty]
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = string.Empty;

    public Question(IOrgCatStorage storage, ISurveyService surveyService)
    {
        _storage = storage;
        _surveyService = surveyService;
    }

    public async Task<IActionResult> OnGet()
    {
        var question = await _surveyService.LoadNextQuestion(SurveyResponseId);

        if (question == null)
        {
            // todo: redirect to thank you page
            QuestionText = "No more questions";
        }
        else
        {
            QuestionId = question.Id;
            QuestionText = question.QuestionText;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _storage.Add(new SurveyQuestionResponse(SurveyResponseId, QuestionId, Answer));

        return RedirectToPage("./Question", new {SurveyResponseId});
    }
}
