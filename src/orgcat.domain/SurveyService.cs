namespace orgcat.domain;

public interface ISurveyService
{
    Task<int> CreateSurvey(string justAFewQuestions);
    Task AddQuestion(int surveyId, string questionText);
    Task<ExistingSurveyQuestion?> LoadNextQuestion(string responseId);
    Task StartLatestSurvey(string responseId);
    Task<SurveyResponseState> GetSurveyResponseState(string id);
    Task AnswerQuestion(string responseId, int questionId, string answer);
}

public enum SurveyResponseState
{
    NotStarted,
    InProgress,
    Complete
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

    public async Task<ExistingSurveyQuestion?> LoadNextQuestion(string responseId)
    {
        var response = await _storage.LoadSurveyResponse(responseId);
        if (response == null) return null;

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

    public async Task<SurveyResponseState> GetSurveyResponseState(string id)
    {
        var surveyResponse = await _storage.LoadSurveyResponse(id);

        if (surveyResponse == null) return SurveyResponseState.NotStarted;

        var survey = await _storage.LoadSurvey(surveyResponse.SurveyId);

        if (surveyResponse.Responses.Count == survey.Questions.Count)
            return SurveyResponseState.Complete;

        return SurveyResponseState.InProgress;
    }

    public Task AnswerQuestion(string responseId, int questionId, string answer)
    {
        return _storage.Add(new SurveyQuestionResponse(responseId, questionId, answer));
    }
}
