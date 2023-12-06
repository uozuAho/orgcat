using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using orgcat.domain;

namespace orgcat.postgresdb
{
    internal class OrgCatPostgresDbStorage : IOrgCatStorage
    {
        private readonly OrgCatDb _context;
        private readonly ILogger<OrgCatPostgresDbStorage> _log;

        public OrgCatPostgresDbStorage(
            OrgCatDb context,
            ILogger<OrgCatPostgresDbStorage> logger)
        {
            _context = context;
            _log = logger;
        }

        public async Task<bool> SurveyExists(string id)
        {
            return await _context.SurveyResponses.AnyAsync(s => s.ResponseId == id);
        }

        public async Task CreateNewSurveyResponse(string responseId, int surveyId)
        {
            _context.SurveyResponses.Add(new Entities.SurveyResponse
            {
                ResponseId = responseId,
                SurveyId = surveyId
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is PostgresException { SqlState: "23505" })
                {
                    _log.LogWarning("Ignoring duplicate survey response creation");
                    return;
                }
                throw;
            }
        }

        public async Task Add(SurveyQuestionResponse questionResponse)
        {
            var surveyResponseId = await _context.SurveyResponses
                .Where(r => r.ResponseId == questionResponse.SurveyResponseId)
                .Select(r => r.Id)
                .SingleAsync();

            _context.SurveyQuestionResponses.Add(new Entities.SurveyQuestionResponse
            {
                SurveyResponseId = surveyResponseId,
                QuestionId = questionResponse.QuestionId,
                ResponseText = questionResponse.ResponseText
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

        public async Task<ExistingSurveyResponse?> LoadSurveyResponse(string responseId)
        {
            var response = await _context.SurveyResponses
                .Include(r => r.SurveyQuestionResponses)
                .SingleOrDefaultAsync(r => r.ResponseId == responseId);

            return response?.ToDomain();
        }
    }
}
