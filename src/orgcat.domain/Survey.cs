namespace orgcat.domain;

public record NewSurvey(string Name);
public record ExistingSurvey(int Id, string Name, List<ExistingSurveyQuestion> Questions);
public record NewSurveyQuestion(int SurveyId, string QuestionText);
public record ExistingSurveyQuestion(int Id, int SurveyId, string QuestionText);
public record NewSurveyResponse(int SurveyId, string ResponseId);
public record ExistingSurveyResponse(int SurveyId, string ResponseId, List<SurveyQuestionResponse> Responses);
public record SurveyQuestionResponse(string SurveyResponseId, int QuestionId, string ResponseText);
