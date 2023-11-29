namespace orgcat.domain;

public interface ISurveyService
{
    Task<bool> IsSurveyStarted(string id);
    Task StartSurvey(string id);
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
}