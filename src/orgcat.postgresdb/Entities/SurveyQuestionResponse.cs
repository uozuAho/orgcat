namespace orgcat.postgresdb.Entities;

public class SurveyQuestionResponse
{
    public int Id { get; set; }
    public string SurveyResponseId { get; set; } = string.Empty;
    public int QuestionId { get; set; }
    public string ResponseText { get; set; } = string.Empty;
    
    public SurveyResponse SurveyResponse { get; set; } = null!;
    public SurveyQuestion Question { get; set; } = null!;

    public domain.SurveyQuestionResponse ToDomain()
    {
        return new domain.SurveyQuestionResponse(SurveyResponseId, QuestionId, ResponseText);
    }
}