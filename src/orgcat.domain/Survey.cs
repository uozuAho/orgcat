namespace orgcat.domain;

public record NewSurvey(string Name);
public record ExistingSurvey(int Id, string Name, List<SurveyQuestion> Questions);
public record SurveyQuestion(int SurveyId, string QuestionText);
public record NewSurveyResponse(int SurveyId, string ResponseId);
public record ExistingSurveyResponse(int SurveyId, List<SurveyQuestionResponse> Responses);
public record SurveyQuestionResponse(int SurveyResponseId, int QuestionId, string ResponseText);
