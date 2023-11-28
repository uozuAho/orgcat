using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using orgcat.domain;

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
    
    public DbSet<Entities.Dummy> Dummies { get; set; }
}
