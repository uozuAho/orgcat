using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using orgcat.domain;
using orgcat.postgresdb.Entities;
using SurveyQuestionResponse = orgcat.postgresdb.Entities.SurveyQuestionResponse;

namespace orgcat.postgresdb;

public static class StorageConfiguration
{
    public static void AddOrgCatPostgresDb(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<OrgCatDb>(options => options.UseNpgsql(connectionString));
        services.AddTransient<IOrgCatStorage, OrgCatPostgresDbStorage>();
    }
}

internal class OrgCatDb : DbContext
{
    public OrgCatDb(DbContextOptions<OrgCatDb> options) : base(options)
    {
    }

    public DbSet<SurveyResponse> SurveyResponses { get; set; } = null!;
    public DbSet<SurveyQuestionResponse> SurveyQuestionResponses { get; set; } = null!;
    public DbSet<SurveyQuestion> SurveyQuestions { get; set; } = null!;
    public DbSet<Survey> Surveys { get; set; } = null!;
}
