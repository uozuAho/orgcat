namespace orgcat.postgresdb.Entities;

public class SurveyQuestion
{
    public int Id { get; set; }
    public string SurveyId { get; set; } = string.Empty;
    
    public string QuestionText { get; set; } = string.Empty;
}