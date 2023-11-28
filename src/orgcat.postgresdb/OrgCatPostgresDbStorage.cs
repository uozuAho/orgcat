using Microsoft.EntityFrameworkCore;
using orgcat.domain;

namespace orgcat.postgresdb;

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
}