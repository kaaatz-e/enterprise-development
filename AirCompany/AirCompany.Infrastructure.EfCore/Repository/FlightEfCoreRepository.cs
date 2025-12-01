using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;

/// <summary>
/// Repository implementation for managing <see cref="Flight"/> entities using Entity Framework Core.
/// Provides CRUD operations for <see cref="Flight"/> in the MongoDB database
/// </summary>
public class FlightEfCoreRepository(AirCompanyDbContext context) : IRepository<Flight, Guid>
{
    /// <summary>
    /// Creates a new flight entity in the database
    /// </summary>
    /// <param name="entity">The flight entity to create</param>
    /// <returns>The created <see cref="Flight"/> entity with generated identifier</returns>
    public async Task<Flight> Create(Flight entity)
    {
        var result = await context.Flights.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a flight entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the flight to delete</param>
    /// <returns>True if the entity was found and deleted; otherwise, false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.Flights.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Flights.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a flight entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the flight to retrieve</param>
    /// <returns>The <see cref="Flight"/> entity if found; otherwise, null</returns>
    public async Task<Flight?> Get(Guid entityId) =>
        await context.Flights.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Retrieves all flight entities from the database
    /// </summary>
    /// <returns>A list of <see cref="Flight"/> entities</returns>
    public async Task<IList<Flight>> GetAll() =>
        await context.Flights.ToListAsync();

    /// <summary>
    /// Updates an existing flight entity in the database
    /// </summary>
    /// <param name="entity">The flight entity with updated values</param>
    /// <returns>The updated <see cref="Flight"/> entity</returns>
    public async Task<Flight> Update(Flight entity)
    {
        context.Flights.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}