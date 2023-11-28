using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace orgcat.postgresdb;

public static class StorageConfiguration
{
    public static void AddOrgCatDbStorage(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<OrgCatDb>(options => options.UseNpgsql(connectionString));
    }
}

internal class OrgCatDb : DbContext
{
    public OrgCatDb(DbContextOptions<OrgCatDb> options) : base(options)
    {
    }
    
    public DbSet<Entities.Dummy> Dummies { get; set; }
}
