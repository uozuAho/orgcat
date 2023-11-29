namespace orgcat.postgresdb.Entities;

public class SurveyResponse
{
    public string Id { get; set; } = string.Empty;
    
    public ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; } = new List<SurveyQuestionResponse>();
}