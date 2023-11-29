using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using orgcat.domain;

namespace orgcat.web.Pages.Survey;

[BindProperties]
public class Question : PageModel
{
    private readonly IOrgCatStorage _storage;
    
    public int SurveyId { get; set; }
    public string SurveyResponseId { get; set; } = string.Empty;
    public int QuestionId { get; set; }
    
    public string QuestionText { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1)]
    public string Answer { get; set; } = string.Empty;

    public Question(IOrgCatStorage storage)
    {
        _storage = storage;
    }
    
    public async Task<IActionResult> OnGet()
    {
        var question = await _storage.LoadQuestion(SurveyId, QuestionId);
        QuestionText = question.QuestionText;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        await _storage.Add(new SurveyQuestionResponse(SurveyResponseId, QuestionId, Answer));
        return RedirectToPage("./Question", new {surveyId=SurveyId});
    }
}