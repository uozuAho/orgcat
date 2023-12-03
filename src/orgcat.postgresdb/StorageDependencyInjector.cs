using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using orgcat.domain;

namespace orgcat.postgresdb;

public static class StorageDependencyInjector
{
    public static void AddOrgCatPostgresDb(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<OrgCatDb>(options => options.UseNpgsql(connectionString));
        services.AddTransient<IOrgCatStorage, OrgCatPostgresDbStorage>();
    }

    // This is only here since I don't want to expose the OrgCatDb
    // to the web project. It's not nice to have an asp dependency
    // in this project, but I think it's the lesser of the evils.
    public static void StoreKeysInPostgresDb(
        this IDataProtectionBuilder builder)
    {
        builder.PersistKeysToDbContext<OrgCatDb>();
    }
}
