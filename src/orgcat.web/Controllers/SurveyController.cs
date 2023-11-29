using Microsoft.AspNetCore.Mvc;
using orgcat.domain;

namespace orgcat.web.Controllers;

public class SurveyController : Controller
{
    private readonly ISurveyService _surveyService;

    public SurveyController(ISurveyService svc)
    {
        _surveyService = svc;
    }

    public async Task<IActionResult> Start(string id)
    {
        var isStarted = await _surveyService.IsSurveyStarted(id);

        if (!isStarted)
        {
            await _surveyService.StartLatestSurvey(id);
            return RedirectToPage("/Survey/Welcome", new {responseId=id});
        }
        else
        {
            // todo:
            // var question = _surveyService.GetNextQuestion(id);
            // Show(question);

            return RedirectToPage("/Survey/Welcome", new {responseId=id});
        }
    }
}
