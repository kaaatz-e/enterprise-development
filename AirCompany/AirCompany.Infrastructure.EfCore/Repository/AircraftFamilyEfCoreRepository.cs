using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;
public class AircraftFamilyEfCoreRepository(AirCompanyDbContext context) : IRepository<AircraftFamily>
{
    public async Task<AircraftFamily> Create(AircraftFamily entity)
    {
        var result = await context.AircraftFamilies.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.AircraftFamilies.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.AircraftFamilies.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<AircraftFamily?> Get(Guid entityId) =>
        await context.AircraftFamilies.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<AircraftFamily>> GetAll() =>
        await context.AircraftFamilies.ToListAsync();

    public async Task<AircraftFamily> Update(AircraftFamily entity)
    {
        context.AircraftFamilies.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}
