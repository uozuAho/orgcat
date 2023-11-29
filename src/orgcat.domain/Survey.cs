namespace orgcat.domain;

public record SurveyQuestion(string SurveyId, int QuestionNumber, string QuestionText);
public record Survey(string Id, string Name, string Description, List<SurveyQuestion> Questions);
public record SurveyQuestionResponse(string SurveyId, string ResponseText);
public record SurveyResponse(string SurveyId, List<SurveyQuestionResponse> Responses);