namespace orgcat.domain;

public record SurveyQuestion(int SurveyId, int QuestionNumber, string QuestionText);
public record Survey(int Id, string Name, string Description, List<SurveyQuestion> Questions);
public record SurveyQuestionResponse(int SurveyId, string ResponseText);
public record SurveyResponse(int Id, int SurveyId, List<SurveyQuestionResponse> Responses);