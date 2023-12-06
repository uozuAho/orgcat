using orgcat.domain;
using orgcat.postgresdb;

namespace orgcat.web;

public static class WebBuilder
{
    public static void ConfigureBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        var connString = builder.Configuration.GetConnectionString("OrgCatDb");
        if (string.IsNullOrEmpty(connString))
        {
            throw new Exception("Connection string is empty");
        }
        builder.Services.AddOrgCatPostgresDb(connString);
        builder.Services.AddDataProtection().StoreKeysInPostgresDb();
        builder.Services.AddTransient<ISurveyService, SurveyService>();
    }
}
