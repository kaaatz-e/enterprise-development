using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;

/// <summary>
/// Repository implementation for managing <see cref="Ticket"/> entities using Entity Framework Core.
/// Provides CRUD operations for <see cref="Ticket"/> in the MongoDB database
/// </summary>
public class TicketEfCoreRepository(AirCompanyDbContext context) : IRepository<Ticket, Guid>
{
    /// <summary>
    /// Creates a new ticket entity in the database
    /// </summary>
    /// <param name="entity">The ticket entity to create</param>
    /// <returns>The created <see cref="Ticket"/> entity with generated identifier</returns>
    public async Task<Ticket> Create(Ticket entity)
    {
        var result = await context.Tickets.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a ticket entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the ticket to delete</param>
    /// <returns>True if the entity was found and deleted; otherwise, false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.Tickets.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Tickets.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a ticket entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the ticket to retrieve</param>
    /// <returns>The <see cref="Ticket"/> entity if found; otherwise, null</returns>
    public async Task<Ticket?> Get(Guid entityId) =>
        await context.Tickets.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Retrieves all ticket entities from the database
    /// </summary>
    /// <returns>A list of <see cref="Ticket"/> entities</returns>
    public async Task<IList<Ticket>> GetAll() =>
        await context.Tickets.ToListAsync();

    /// <summary>
    /// Updates an existing ticket entity in the database
    /// </summary>
    /// <param name="entity">The ticket entity with updated values</param>
    /// <returns>The updated <see cref="Ticket"/> entity</returns>
    public async Task<Ticket> Update(Ticket entity)
    {
        context.Tickets.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}