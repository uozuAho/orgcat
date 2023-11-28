using orgcat.domain;

namespace orgcat.postgresdb.Entities;

internal class Dummy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public static Dummy FromDomain(domain.NewDummy newDummy)
    {
        return new Dummy
        {
            Name = newDummy.Name
        };
    }

    public ExistingDummy ToDomain()
    {
        return new ExistingDummy(Id, Name);
    }
}