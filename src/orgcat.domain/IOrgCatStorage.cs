namespace orgcat.domain;

public interface IOrgCatStorage
{
    void Add(NewDummy newDummy);
    Task<ExistingDummy?> FindDummy(int id);
    void DeleteDummy(int id);
    Task<IList<ExistingDummy>> LoadAllDummies();
    Task<bool> SurveyExists(string id);
    Task CreateNewSurveyResponse(string responseId, int surveyId);
    Task Add(SurveyQuestionResponse response);
    Task<ExistingSurveyQuestion> LoadQuestion(int surveyId, int questionId);
    Task Add(NewSurvey survey);
    Task Add(NewSurveyQuestion question);
    Task<ExistingSurvey> LoadSurvey(int id);
    Task<ExistingSurvey> LoadSurveyByName(string surveyName);
    Task<List<ExistingSurvey>> ListSurveys();
    Task Add(NewSurveyResponse response);
    Task<ExistingSurveyResponse?> LoadSurveyResponse(string responseId);
}
