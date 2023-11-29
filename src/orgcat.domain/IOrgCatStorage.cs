namespace orgcat.domain;

public interface IOrgCatStorage
{
    void Add(NewDummy newDummy);
    Task<ExistingDummy?> FindDummy(int id);
    void DeleteDummy(int id);
    Task<IList<ExistingDummy>> LoadAllDummies();
    Task<bool> SurveyExists(string id);
}