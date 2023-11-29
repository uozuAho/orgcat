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
    Task<string> LoadQuestionText(string surveyId, int questionId);
}