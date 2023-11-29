using Microsoft.AspNetCore.Mvc.RazorPages;
using orgcat.domain;

namespace orgcat.web.Pages.Survey;

public class Question : PageModel
{
    private readonly IOrgCatStorage _storage;
    
    public string SurveyId { get; set; } = string.Empty;
    public string QuestionText { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;

    public Question(IOrgCatStorage storage)
    {
        _storage = storage;
    }
    
    public void OnGet()
    {
        QuestionText = "What is your favorite color?";   
    }
    
    public Task OnPostAsync()
    {
        _storage.Add(new SurveyQuestionResponse(SurveyId, Answer));
        return Task.CompletedTask;
    }
}