using orgcat.domain;

namespace orgcat.postgresdb.Entities;

public class Survey
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();
    
    public ExistingSurvey ToDomain()
    {
        var questions = SurveyQuestions.Select(q => q.ToDomain()).ToList();
        return new ExistingSurvey(Id, Name, questions);
    }
}