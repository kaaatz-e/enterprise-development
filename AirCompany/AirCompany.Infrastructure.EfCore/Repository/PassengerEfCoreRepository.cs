using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;

/// <summary>
/// Repository implementation for managing <see cref="Passenger"/> entities using Entity Framework Core.
/// Provides CRUD operations for <see cref="Passenger"/> in the MongoDB database
/// </summary>
public class PassengerEfCoreRepository(AirCompanyDbContext context) : IRepository<Passenger, Guid>
{
    /// <summary>
    /// Creates a new passenger entity in the database
    /// </summary>
    /// <param name="entity">The passenger entity to create</param>
    /// <returns>The created <see cref="Passenger"/> entity with generated identifier.</returns>
    public async Task<Passenger> Create(Passenger entity)
    {
        var result = await context.Passengers.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes a passenger entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the passenger to delete</param>
    /// <returns>True if the entity was found and deleted; otherwise, false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.Passengers.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.Passengers.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves a passenger entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the passenger to retrieve</param>
    /// <returns>The <see cref="Passenger"/> entity if found; otherwise, null</returns>
    public async Task<Passenger?> Get(Guid entityId) =>
        await context.Passengers.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Retrieves all passenger entities from the database
    /// </summary>
    /// <returns>A list of <see cref="Passenger"/> entities</returns>
    public async Task<IList<Passenger>> GetAll() =>
        await context.Passengers.ToListAsync();

    /// <summary>
    /// Updates an existing passenger entity in the database
    /// </summary>
    /// <param name="entity">The passenger entity with updated values</param>
    /// <returns>The updated <see cref="Passenger"/> entity</returns>
    public async Task<Passenger> Update(Passenger entity)
    {
        context.Passengers.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}