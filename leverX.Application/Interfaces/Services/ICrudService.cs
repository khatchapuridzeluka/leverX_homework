namespace leverX.Application.Interfaces.Services
{
    public interface ICrudService<TDto, TCreateDto, TUpdateDto>
    {
        Task<TDto> CreateAsync(TCreateDto dto);
        Task<TDto?> GetByIdAsync(Guid id);
        Task<List<TDto>> GetAllAsync();
        Task<TDto> UpdateAsync(Guid id, TUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}
