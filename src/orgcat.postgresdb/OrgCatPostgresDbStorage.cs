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

        public void Add(NewDummy newDummy)
        {
            _context.Dummies.Add(Entities.Dummy.FromDomain(newDummy));
            _context.SaveChanges();
        }

        public async Task<ExistingDummy?> FindDummy(int id)
        {
            var dummy = await _context.Dummies.FindAsync(id);
            return dummy?.ToDomain();
        }

        public void DeleteDummy(int id)
        {
            var dummy = _context.Dummies.Find(id);
            if (dummy != null)
            {
                _context.Dummies.Remove(dummy);
                _context.SaveChanges();
            }
        }

        public async Task<IList<ExistingDummy>> LoadAllDummies()
        {
            var dummies = await _context.Dummies.Select(d => d.ToDomain()).ToListAsync();
            return dummies;
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

        public async Task<ExistingSurveyQuestion> LoadQuestion(int surveyId, int questionId)
        {
            var question = await _context.SurveyQuestions
                .SingleAsync(r => r.SurveyId == surveyId && r.Id == questionId);

            return question.ToDomain();
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
            await _context.SurveyQuestions.AddAsync(new Entities.SurveyQuestion()
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
