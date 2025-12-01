using AirCompany.Domain;
using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirCompany.Infrastructure.EfCore.Repository;

/// <summary>
/// Repository implementation for managing <see cref="AircraftModel"/> entities using Entity Framework Core.
/// Provides CRUD operations for <see cref="AircraftModel"/> in the MongoDB database
/// </summary>
public class AircraftModelEfCoreRepository(AirCompanyDbContext context) : IRepository<AircraftModel, Guid>
{
    /// <summary>
    /// Creates a new aircraft model entity in the database
    /// </summary>
    /// <param name="entity">The aircraft model entity to create</param>
    /// <returns>The created <see cref="AircraftModel"/> entity with generated identifier</returns>
    public async Task<AircraftModel> Create(AircraftModel entity)
    {
        var result = await context.AircraftModels.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Deletes an aircraft model entity by its unique identifier.
    /// </summary>
    /// <param name="entityId">The unique identifier of the aircraft model to delete.</param>
    /// <returns>True if the entity was found and deleted; otherwise, false.</returns>
    public async Task<bool> Delete(Guid entityId)
    {
        var entity = await context.AircraftModels.FirstOrDefaultAsync(e => e.Id == entityId);
        if (entity == null)
            return false;

        context.AircraftModels.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Retrieves an aircraft model entity by its unique identifier
    /// </summary>
    /// <param name="entityId">The unique identifier of the aircraft model to retrieve</param>
    /// <returns>The <see cref="AircraftModel"/> entity if found; otherwise, null</returns>
    public async Task<AircraftModel?> Get(Guid entityId) =>
        await context.AircraftModels.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Retrieves all aircraft model entities from the database.
    /// </summary>
    /// <returns>A list of <see cref="AircraftModel"/> entities</returns>
    public async Task<IList<AircraftModel>> GetAll() =>
        await context.AircraftModels.ToListAsync();

    /// <summary>
    /// Updates an existing aircraft model entity in the database
    /// </summary>
    /// <param name="entity">The aircraft model entity with updated values</param>
    /// <returns>The updated <see cref="AircraftModel"/> entity</returns>
    public async Task<AircraftModel> Update(AircraftModel entity)
    {
        context.AircraftModels.Update(entity);

        await context.SaveChangesAsync();
        return entity;
    }
}