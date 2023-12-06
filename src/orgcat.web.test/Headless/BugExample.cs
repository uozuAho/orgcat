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
    public async Task ConcurrentStartMadeResponseUnusable()
    {
        var builder = WebApplication.CreateBuilder();
        WebBuilder.ConfigureBuilder(builder);
        var app = builder.Build();

        const string responseId = "123";
        var surveyService = app.Services.GetRequiredService<ISurveyService>();

        var surveyState = await surveyService.GetSurveyResponseState(responseId);
        surveyState.ShouldBe(SurveyResponseState.NotStarted);

        // Simulate two concurrent requests to start the survey.
        // This used to cause the survey to be unusable.
        await surveyService.StartLatestSurvey(responseId);
        await surveyService.StartLatestSurvey(responseId);

        var question = await surveyService.LoadNextQuestion(responseId);
        question.ShouldNotBeNull();
    }
}
