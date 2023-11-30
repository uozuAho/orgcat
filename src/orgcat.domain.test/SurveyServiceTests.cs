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

        const string responseId = "apsdou439k";
        (await _service.GetSurveyResponseState(responseId)).ShouldBe(SurveyResponseState.NotStarted);

        await _service.StartLatestSurvey(responseId);
        (await _service.GetSurveyResponseState(responseId)).ShouldBe(SurveyResponseState.InProgress);

        var question = await _service.LoadNextQuestion(responseId);
        question.ShouldNotBeNull();
        question.QuestionText.ShouldBe("What is your name?");
        await _service.AnswerQuestion(responseId, question.Id, "Sir Lancelot of Camelot");

        question = await _service.LoadNextQuestion(responseId);
        question.ShouldNotBeNull();
        question.QuestionText.ShouldBe("What is your quest?");
        await _service.AnswerQuestion(responseId, question.Id, "To seek the Holy Grail");

        question = await _service.LoadNextQuestion(responseId);
        question.ShouldNotBeNull();
        question.QuestionText.ShouldBe("What is your favorite color?");
        await _service.AnswerQuestion(responseId, question.Id, "Blue");

        question = await _service.LoadNextQuestion(responseId);
        question.ShouldBeNull();
        (await _service.GetSurveyResponseState(responseId)).ShouldBe(SurveyResponseState.Complete);
    }
}
