using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;
public class FlightEfCoreRepository(AirCompanyDbContext context) : IRepository<Flight>
{
    public async Task<Flight> Create(Flight entity)
    {
        var result = await context.Flights.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.Flights.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Flights.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Flight?> Get(Guid entityId) =>
        await context.Flights.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<Flight>> GetAll() =>
        await context.Flights.ToListAsync();

    public async Task<Flight> Update(Flight entity)
    {
        context.Flights.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}
