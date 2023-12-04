using orgcat.domain;
using Shouldly;

namespace orgcat.web.test.Headless;

public class BugExample
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ConcurrentStartMakesResponseUnusable()
    {
        var builder = WebApplication.CreateBuilder();
        WebBuilder.ConfigureBuilder(builder);
        var app = builder.Build();

        const string responseId = "123";
        var surveyService = app.Services.GetRequiredService<ISurveyService>();

        var surveyState = await surveyService.GetSurveyResponseState(responseId);
        surveyState.ShouldBe(SurveyResponseState.NotStarted);

        // simulate two concurrent requests to start the survey
        await surveyService.StartLatestSurvey(responseId);
        await surveyService.StartLatestSurvey(responseId);

        Should.Throw<InvalidOperationException>(async () =>
            await surveyService.LoadNextQuestion(responseId));
    }
}
