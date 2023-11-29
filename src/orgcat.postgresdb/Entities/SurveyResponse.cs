using orgcat.domain;

namespace orgcat.postgresdb.Entities;

public class SurveyResponse
{
    public string Id { get; set; } = string.Empty;
    public int SurveyId { get; set; }
    
    public Survey Survey { get; set; } = null!;
    public ICollection<SurveyQuestionResponse> SurveyQuestionResponses { get; set; } = new List<SurveyQuestionResponse>();

    public ExistingSurveyResponse ToDomain()
    {
        return new ExistingSurveyResponse(
            SurveyId,
            Id,
            SurveyQuestionResponses.Select(r => r.ToDomain()).ToList());
    }
}