namespace orgcat.domain;

public interface ISurveyService
{
    Task<bool> IsSurveyStarted(string id);
    Task StartSurvey(string id);
    Task<int> CreateSurvey(string justAFewQuestions);
    Task AddQuestion(int surveyId, string questionText);
    Task<List<ExistingSurvey>> ListAvailableSurveys();
    Task StartNewSurveyResponse(int surveyId, string responseId);
    Task<SurveyQuestion?> LoadNextQuestion(string responseId);
}

public class SurveyService : ISurveyService
{
    private readonly IOrgCatStorage _storage;

    public SurveyService(IOrgCatStorage storage)
    {
        _storage = storage;
    }
    
    public async Task<bool> IsSurveyStarted(string id)
    {
        return await _storage.SurveyExists(id);
    }

    public Task StartSurvey(string id)
    {
        return _storage.CreateNewSurveyResponse(id);
    }

    public async Task<int> CreateSurvey(string surveyName)
    {
        await _storage.Add(new NewSurvey(surveyName));
        var survey = await _storage.LoadSurveyByName(surveyName);
        return survey.Id;
    }

    public Task AddQuestion(int surveyId, string questionText)
    {
        return _storage.Add(new SurveyQuestion(surveyId, questionText));
    }

    public Task<List<ExistingSurvey>> ListAvailableSurveys()
    {
        return _storage.ListSurveys();
    }

    public Task StartNewSurveyResponse(int surveyId, string responseId)
    {
        return _storage.Add(new NewSurveyResponse(surveyId, responseId));
    }

    public Task<SurveyQuestion> LoadNextQuestion(string responseId)
    {
        return _storage.LoadNextQuestion(responseId);
    }
}