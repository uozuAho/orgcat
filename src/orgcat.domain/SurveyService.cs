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
}