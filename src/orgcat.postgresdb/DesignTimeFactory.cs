using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace orgcat.postgresdb;

internal class DesignTimeFactory : IDesignTimeDbContextFactory<OrgCatDb>
{
    public OrgCatDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrgCatDb>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=orgcat;Username=postgres;Password=asdfoot");

        return new OrgCatDb(optionsBuilder.Options);
    }
}