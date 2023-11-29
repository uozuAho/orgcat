namespace orgcat.domain.test;

internal class FakeOrgCatStorage : IOrgCatStorage
{
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

    public Task<SurveyQuestion> LoadQuestion(int surveyId, int questionId)
    {
        throw new NotImplementedException();
    }

    public Task<string> LoadQuestionText(string surveyId, int questionId)
    {
        throw new NotImplementedException();
    }

    public Task Add(NewSurvey survey)
    {
        throw new NotImplementedException();
    }

    public Task Add(SurveyQuestion question)
    {
        throw new NotImplementedException();
    }

    public Task<ExistingSurvey> LoadSurveyByName(string surveyName)
    {
        throw new NotImplementedException();
    }
}

public class SurveyServiceTests
{
    private SurveyService _service = null!;

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
        _service.AddQuestion(surveyId, "What is your name?");
        _service.AddQuestion(surveyId, "What is your quest?");
        _service.AddQuestion(surveyId, "What is your favorite color?");

        var surveys = _service.ListAvailableSurveys();
        var survey = surveys.Single(s => s.Id == surveyId);

        var randomResponseId = "apsdou439k";
        _service.StartSurveyResponse(surveyId, randomResponseId);
        var question = _service.LoadNextQuestion(randomResponseId);
        question.Text.ShouldBe("What is your name?");
    }
}