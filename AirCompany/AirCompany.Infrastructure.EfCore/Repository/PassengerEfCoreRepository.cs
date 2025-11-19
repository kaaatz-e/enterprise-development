using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;
public class PassengerEfCoreRepository(AirCompanyDbContext context) : IRepository<Passenger>
{
    public async Task<Passenger> Create(Passenger entity)
    {
        var result = await context.Passengers.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.Passengers.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Passengers.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Passenger?> Get(Guid entityId) =>
        await context.Passengers.FirstOrDefaultAsync(e => e.Id == entityId);
 
    public async Task<IList<Passenger>> GetAll() =>
        await context.Passengers.ToListAsync();
    
    public async Task<Passenger> Update(Passenger entity)
    {
        context.Passengers.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}
