namespace AirCompany.Domain;
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    public Task<TEntity> Create(TEntity entity);

    public Task<TEntity?> Get(TKey id);

    public Task<IList<TEntity>> GetAll();

    public Task<TEntity> Update(TEntity entity);

    public Task<bool> Delete(TKey id);
}
