using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace orgcat.postgresdb;

// ReSharper disable once UnusedType.Global  - used by ef core migrations
internal class DesignTimeFactory : IDesignTimeDbContextFactory<OrgCatDb>
{
    public OrgCatDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrgCatDb>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=orgcat;Username=postgres;Password=asdfoot");

        return new OrgCatDb(optionsBuilder.Options);
    }
}
