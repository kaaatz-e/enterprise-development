namespace AirCompany.Domain;

/// <summary>
/// Repository interface for CRUD operations
/// </summary>
/// <typeparam name="TEntity">The type of entity whose collection is being abstracted</typeparam>
/// <typeparam name="TKey">The type of the entity ID</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Creating a new entity
    /// </summary>
    /// <param name="entity">New entity</param>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Getting an entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Entity</returns>
    public Task<TEntity?> Get(TKey id);

    /// <summary>
    /// Getting the entire list of entities
    /// </summary>
    /// <returns></returns>
    public Task<IList<TEntity>> GetAll();

    /// <summary>
    /// Updating an entity in a collection
    /// </summary>
    /// <param name="entity">Edited entity</param>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Deleting an entity from a collection
    /// </summary>
    /// <param name="id">Entity ID</param>
    public Task<bool> Delete(TKey id);
}
