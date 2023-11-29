namespace orgcat.postgresdb.Entities;

public class SurveyQuestion
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    
    public string QuestionText { get; set; } = string.Empty;
    
    public Survey Survey { get; set; } = null!;
    
    public domain.ExistingSurveyQuestion ToDomain()
    {
        return new domain.ExistingSurveyQuestion(Id, SurveyId, QuestionText);
    }
}