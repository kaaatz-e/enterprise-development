using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;

/// <summary>
/// Repository implementation for managing <see cref="AircraftFamily"/> entities using Entity Framework Core.
/// Provides CRUD operations for <see cref="AircraftFamily"/> in the MongoDB database
/// </summary>
public class AircraftFamilyEfCoreRepository(AirCompanyDbContext context) : IRepository<AircraftFamily, Guid>
{
    /// <summary>
    /// Creates a new aircraft family entity in the database
    /// </summary>
    /// <param name="entity">The aircraft family entity to create</param>
    /// <returns>The created <see cref="AircraftFamily"/> entity with generated identifier</returns>
    public async Task<AircraftFamily> Create(AircraftFamily entity)
    {
        var result = await context.AircraftFamilies.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes an aircraft family entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the aircraft family to delete</param>
    /// <returns>True if the entity was found and deleted; otherwise, false</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.AircraftFamilies.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.AircraftFamilies.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Retrieves an aircraft family entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the aircraft family to retrieve</param>
    /// <returns>The <see cref="AircraftFamily"/> entity if found; otherwise, null</returns>
    public async Task<AircraftFamily?> Get(Guid entityId) =>
        await context.AircraftFamilies.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Retrieves all aircraft family entities from the database
    /// </summary>
    /// <returns>A list of all <see cref="AircraftFamily"/> entities</returns>
    public async Task<IList<AircraftFamily>> GetAll() =>
        await context.AircraftFamilies.ToListAsync();

    /// <summary>
    /// Updates an existing aircraft family entity in the database
    /// </summary>
    /// <param name="entity">The aircraft family entity with updated values</param>
    /// <returns>The updated <see cref="AircraftFamily"/> entity</returns>
    public async Task<AircraftFamily> Update(AircraftFamily entity)
    {
        context.AircraftFamilies.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}