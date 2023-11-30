using Microsoft.EntityFrameworkCore;
using orgcat.domain;

namespace orgcat.postgresdb
{
    internal class OrgCatPostgresDbStorage : IOrgCatStorage
    {
        private readonly OrgCatDb _context;

        public OrgCatPostgresDbStorage(OrgCatDb context)
        {
            _context = context;
        }

        public async Task<bool> SurveyExists(string id)
        {
            return await _context.SurveyResponses.AnyAsync(s => s.Id == id);
        }

        public async Task CreateNewSurveyResponse(string id, int surveyId)
        {
            _context.SurveyResponses.Add(new Entities.SurveyResponse
            {
                Id = id,
                SurveyId = surveyId
            });

            await _context.SaveChangesAsync();
        }

        public async Task Add(SurveyQuestionResponse response)
        {
            _context.SurveyQuestionResponses.Add(new Entities.SurveyQuestionResponse
            {
                SurveyResponseId = response.SurveyResponseId,
                QuestionId = response.QuestionId,
                ResponseText = response.ResponseText
            });

            await _context.SaveChangesAsync();
        }

        public async Task Add(NewSurvey survey)
        {
            await _context.Surveys.AddAsync(new Entities.Survey
            {
                Name = survey.Name
            });
            await _context.SaveChangesAsync();
        }

        public async Task Add(NewSurveyQuestion question)
        {
            await _context.SurveyQuestions.AddAsync(new Entities.SurveyQuestion
            {
                SurveyId = question.SurveyId,
                QuestionText = question.QuestionText,
            });
            await _context.SaveChangesAsync();
        }

        public async Task<ExistingSurvey> LoadSurvey(int id)
        {
            var survey = await _context
                .Surveys
                .Include(s => s.SurveyQuestions)
                .SingleAsync(s => s.Id == id);

            return survey.ToDomain();
        }

        public async Task<ExistingSurvey> LoadSurveyByName(string surveyName)
        {
            var survey = await _context.Surveys.SingleAsync(s => s.Name == surveyName);
            return survey.ToDomain();
        }

        public Task<List<ExistingSurvey>> ListSurveys()
        {
            return _context.Surveys.Select(s => s.ToDomain()).ToListAsync();
        }

        public async Task Add(NewSurveyResponse response)
        {
            await _context.SurveyResponses.AddAsync(new Entities.SurveyResponse
            {
                Id = response.ResponseId,
                SurveyId = response.SurveyId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<ExistingSurveyResponse?> LoadSurveyResponse(string responseId)
        {
            var response = await _context.SurveyResponses
                .Include(r => r.SurveyQuestionResponses)
                .SingleOrDefaultAsync(r => r.Id == responseId);

            return response?.ToDomain();
        }
    }
}
