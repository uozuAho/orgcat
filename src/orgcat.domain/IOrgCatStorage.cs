namespace orgcat.domain;

public interface IOrgCatStorage
{
    void Add(NewDummy newDummy);
    Task<ExistingDummy?> FindDummy(int id);
    void DeleteDummy(int id);
    Task<IList<ExistingDummy>> LoadAllDummies();
    Task<bool> SurveyExists(string id);
    Task CreateNewSurveyResponse(string id);
    Task Add(SurveyQuestionResponse response);
    Task<SurveyQuestion> LoadQuestion(int surveyId, int questionId);
    Task Add(NewSurvey survey);
    Task Add(SurveyQuestion question);
    Task<ExistingSurvey> LoadSurveyByName(string surveyName);
    Task<List<ExistingSurvey>> ListSurveys();
    Task Add(NewSurveyResponse response);
    Task<SurveyQuestion> LoadNextQuestion(string surveyResponseId);
}