using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AirCompany.Infrastructure.EfCore.Repository;
public class TicketEfCoreRepository(AirCompanyDbContext context) : IRepository<Ticket, Guid>
{
    public async Task<Ticket> Create(Ticket entity)
    {
        var result = await context.Tickets.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.Tickets.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Tickets.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Ticket?> Get(Guid entityId) =>
        await context.Tickets.FirstOrDefaultAsync(e => e.Id == entityId);

    public async Task<IList<Ticket>> GetAll() =>
        await context.Tickets.ToListAsync();
    
    public async Task<Ticket> Update(Ticket entity)
    {
        context.Tickets.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

}
