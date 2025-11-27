namespace AirCompany.Application.Contracts;

public interface IApplicationService<TDto, TCreateUpdateDto>
    where TDto : class
    where TCreateUpdateDto : class
{
    public Task<TDto> Create(TCreateUpdateDto dto);

    public Task<TDto?> Get(Guid dtoId);

    public Task<IList<TDto>> GetAll();

    public Task<TDto> Update(TCreateUpdateDto dto, Guid dtoId);

    public Task<bool> Delete(Guid dtoId);
}
