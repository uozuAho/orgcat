using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using orgcat.postgresdb.Entities;
using SurveyQuestionResponse = orgcat.postgresdb.Entities.SurveyQuestionResponse;

namespace orgcat.postgresdb;

internal class OrgCatDb : DbContext, IDataProtectionKeyContext
{
    public OrgCatDb(DbContextOptions<OrgCatDb> options) : base(options)
    {
    }

    public DbSet<SurveyResponse> SurveyResponses { get; set; } = null!;
    public DbSet<SurveyQuestionResponse> SurveyQuestionResponses { get; set; } = null!;
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; } = null!;
    public DbSet<Survey> Surveys { get; set; } = null!;
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;
}
