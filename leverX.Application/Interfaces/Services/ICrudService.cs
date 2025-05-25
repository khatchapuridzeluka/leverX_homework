using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leverX.Application.Interfaces.Services
{
    public interface ICrudService<TDto, TCreateDto, TUpdateDto>
    {
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto?> GetByIdAsync(Guid id);
        Task<List<TDto>> GetAllAsync();
        Task UpdateAsync(Guid id, TUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}
