using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AirCompany.Infrastructure.EfCore.Repository;
public class AircraftModelEfCoreRepository(AirCompanyDbContext context) : IRepository<AircraftModel>
{
    public async Task<AircraftModel> Create(AircraftModel entity)
    {
        var result = await context.AircraftModels.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.AircraftModels.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.AircraftModels.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<AircraftModel?> Get(Guid entityId) =>
        await context.AircraftModels.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<AircraftModel>> GetAll() =>
        await context.AircraftModels.ToListAsync();

    public async Task<AircraftModel> Update(AircraftModel entity)
    {
        context.AircraftModels.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}
