namespace orgcat.domain;

public interface ISurveyService
{
    Task<bool> IsSurveyStarted(string id);
    Task StartSurvey(string responseId, int surveyId);
    Task<int> CreateSurvey(string justAFewQuestions);
    Task AddQuestion(int surveyId, string questionText);
    Task<List<ExistingSurvey>> ListAvailableSurveys();
    Task StartNewSurveyResponse(int surveyId, string responseId);
    Task<ExistingSurveyQuestion?> LoadNextQuestion(string responseId);
    Task StartLatestSurvey(string responseId);
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

    public Task StartSurvey(string responseId, int surveyId)
    {
        return _storage.CreateNewSurveyResponse(responseId, surveyId);
    }

    public async Task<int> CreateSurvey(string surveyName)
    {
        await _storage.Add(new NewSurvey(surveyName));
        var survey = await _storage.LoadSurveyByName(surveyName);
        return survey.Id;
    }

    public Task AddQuestion(int surveyId, string questionText)
    {
        return _storage.Add(new NewSurveyQuestion(surveyId, questionText));
    }

    public Task<List<ExistingSurvey>> ListAvailableSurveys()
    {
        return _storage.ListSurveys();
    }

    public Task StartNewSurveyResponse(int surveyId, string responseId)
    {
        return _storage.Add(new NewSurveyResponse(surveyId, responseId));
    }

    public async Task<ExistingSurveyQuestion?> LoadNextQuestion(string responseId)
    {
        var response = await _storage.LoadSurveyResponse(responseId);
        var survey = await _storage.LoadSurvey(response.SurveyId);
        var nextQuestion = survey.Questions
            .FirstOrDefault(q => response.Responses.All(r => r.QuestionId != q.Id));
        return nextQuestion;
    }

    public async Task StartLatestSurvey(string responseId)
    {
        var latestSurvey = (await _storage
            .ListSurveys())
            .OrderByDescending(s => s.Id)
            .First();

        await StartSurvey(responseId, latestSurvey.Id);
    }
}