namespace orgcat.postgresdb.Entities;

public class SurveyQuestionResponse
{
    public int Id { get; set; }
    public string SurveyId { get; set; } = string.Empty;
    public string ResponseText { get; set; } = string.Empty;
    
    public SurveyResponse Survey { get; set; } = null!;
    public SurveyQuestion Question { get; set; } = null!;
}