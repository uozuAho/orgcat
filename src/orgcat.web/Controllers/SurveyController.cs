using Microsoft.AspNetCore.Mvc;
using orgcat.domain;
using orgcat.web.Pages.Survey;

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
        var surveyState = await _surveyService.GetSurveyResponseState(id);

        if (surveyState == SurveyResponseState.NotStarted)
        {
            await _surveyService.StartLatestSurvey(id);
            return RedirectToPage("/Survey/Welcome", new {responseId=id});
        }

        if (surveyState == SurveyResponseState.InProgress)
        {
            return RedirectToPage("/Survey/Welcome", new {responseId=id, inProgress=true});
        }

        return RedirectToPage("/Survey/ThankYou");
    }
}
