using Shouldly;

namespace orgcat.domain.test;

internal class FakeOrgCatStorage : IOrgCatStorage
{
    private readonly List<ExistingSurvey> _surveys = new();
    private readonly List<ExistingSurveyQuestion> _questions = new();
    private readonly List<ExistingSurveyResponse> _surveyResponses = new();
    private readonly List<SurveyQuestionResponse> _questionResponses = new();

    public void Add(NewDummy newDummy)
    {
        throw new NotImplementedException();
    }

    public Task<ExistingDummy?> FindDummy(int id)
    {
        throw new NotImplementedException();
    }

    public void DeleteDummy(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<ExistingDummy>> LoadAllDummies()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SurveyExists(string id)
    {
        throw new NotImplementedException();
    }

    public Task CreateNewSurveyResponse(string id)
    {
        throw new NotImplementedException();
    }

    public Task Add(SurveyQuestionResponse response)
    {
        throw new NotImplementedException();
    }

    public Task<ExistingSurveyQuestion> LoadQuestion(int surveyId, int questionId)
    {
        throw new NotImplementedException();
    }

    public Task<string> LoadQuestionText(string surveyId, int questionId)
    {
        throw new NotImplementedException();
    }

    public Task Add(NewSurvey survey)
    {
        _surveys.Add(new ExistingSurvey(_surveys.Count + 1, survey.Name, new List<ExistingSurveyQuestion>()));
        return Task.CompletedTask;
    }

    public Task Add(NewSurveyQuestion question)
    {
        _questions.Add(new ExistingSurveyQuestion(
            _questions.Count + 1, question.SurveyId, question.QuestionText));
        return Task.CompletedTask;
    }

    public Task<ExistingSurvey> LoadSurvey(int id)
    {
        var existingSurvey = _surveys.Single(s => s.Id == id);
        var questions = _questions.Where(q => q.SurveyId == id).ToList();
        
        return Task.FromResult(existingSurvey with { Questions = questions });
    }

    public Task Add(ExistingSurveyQuestion question)
    {
        _questions.Add(question);
        return Task.CompletedTask;
    }

    public Task<ExistingSurvey> LoadSurveyByName(string surveyName)
    {
        return Task.FromResult(_surveys.Single(s => s.Name == surveyName));
    }

    public Task<List<ExistingSurvey>> ListSurveys()
    {
        return Task.FromResult(_surveys);
    }

    public Task Add(NewSurveyResponse response)
    {
        _surveyResponses.Add(new ExistingSurveyResponse(
            response.SurveyId,
            response.ResponseId,
            new List<SurveyQuestionResponse>()));
        
        return Task.CompletedTask;
    }

    public Task<ExistingSurveyResponse> LoadSurveyResponse(string responseId)
    {
        var response = _surveyResponses.Single(r => r.ResponseId == responseId);
        
        var questionResponses = _questionResponses
            .Where(r => r.SurveyResponseId == responseId)
            .ToList();
        
        return Task.FromResult(response with { Responses = questionResponses });
    }
}

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
        await _service.StartNewSurveyResponse(surveyId, responseId);
        var question = await _service.LoadNextQuestion(responseId);
        question.ShouldNotBeNull();
        question.QuestionText.ShouldBe("What is your name?");
    }
}