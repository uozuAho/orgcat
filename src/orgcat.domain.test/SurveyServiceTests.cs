using Shouldly;

namespace orgcat.domain.test;

public class SurveyServiceTests
{
    private ISurveyService _service = null!;

    [SetUp]
    public void Setup()
    {
        var storage = new FakeOrgCatStorage();
        _service = new SurveyService(storage);
    }

    [Test]
    public async Task SurveyWorkflow()
    {
        var surveyId = await _service.CreateSurvey("Just a few questions");
        await _service.AddQuestion(surveyId, "What is your name?");
        await _service.AddQuestion(surveyId, "What is your quest?");
        await _service.AddQuestion(surveyId, "What is your favorite color?");

        var surveys = await _service.ListAvailableSurveys();
        var survey = surveys.Single(s => s.Id == surveyId);

        const string responseId = "apsdou439k";
        // todo: should this be startsurvey?
        await _service.StartNewSurveyResponse(surveyId, responseId);
        var question = await _service.LoadNextQuestion(responseId);
        question.ShouldNotBeNull();
        question.QuestionText.ShouldBe("What is your name?");
    }
}