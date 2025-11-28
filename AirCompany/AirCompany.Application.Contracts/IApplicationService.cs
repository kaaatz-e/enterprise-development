namespace AirCompany.Application.Contracts;

public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    public Task<TDto> Create(TCreateUpdateDto dto);

    public Task<TDto?> Get(Guid dtoId);

    public Task<IList<TDto>> GetAll();

    public Task<TDto> Update(TCreateUpdateDto dto, Guid dtoId);

    public Task<bool> Delete(Guid dtoId);
}
