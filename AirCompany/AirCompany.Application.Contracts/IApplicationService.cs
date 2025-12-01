namespace AirCompany.Application.Contracts;

/// <summary>
/// Interface for application services providing CRUD operations for entities
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates DTO
    /// </summary>
    /// <param name="dto">The data transfer object containing entity creation data</param>
    /// <returns>The created entity as a data transfer object</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Get a DTO by ID
    /// </summary>
    /// <param name="dtoId">The unique identifier of the entity to retrieve</param>
    /// <returns>The entity as a data transfer object if found; otherwise, null</returns>
    public Task<TDto?> Get(Guid dtoId);

    /// <summary>
    /// Getting the entire list of DTO
    /// </summary>
    /// <returns>A list of all entities as data transfer objects</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Update DTO
    /// </summary>
    /// <param name="dto">The data transfer object containing updated entity data</param>
    /// <param name="dtoId">The unique identifier of the entity to update</param>
    /// <returns>The updated entity as a data transfer object</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, Guid dtoId);

    /// <summary>
    /// Delete DTO
    /// </summary>
    /// <param name="dtoId">The unique identifier of the entity to delete</param>
    /// <returns>True if the entity was found and deleted; otherwise, false</returns>
    public Task<bool> Delete(Guid dtoId);
}