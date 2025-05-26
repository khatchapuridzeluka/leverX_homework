using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.DTOs.Openings;

namespace leverX.Application.Services
{
    public class OpeningService : IOpeningService
    {
        private readonly IOpeningRepository _openingRepository;

        public OpeningService(IOpeningRepository openingRepository)
        {
            _openingRepository = openingRepository;
        }

        public async Task<OpeningDto> CreateAsync(CreateOpeningDto dto)
        {
            var opening = new Opening
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                EcoCode = dto.EcoCode,
                Moves = dto.Moves
            };
            await _openingRepository.AddAsync(opening);
            return MapToDto(opening);
        }

        public async Task<OpeningDto?> GetByIdAsync(Guid id)
        {
            var opening = await _openingRepository.GetByIdAsync(id);
            return opening == null ? null : MapToDto(opening);
        }

        public async Task<List<OpeningDto>> GetAllAsync()
        {
            var openings = await _openingRepository.GetAllAsync();
            return openings.Select(MapToDto).ToList();
        }

        public async Task UpdateAsync(Guid id, UpdateOpeningDto dto)
        {
            var opening = await _openingRepository.GetByIdAsync(id);
            if (opening == null)
                throw new Exception("Opening not found");
            opening.Name = dto.Name;
            opening.EcoCode = dto.EcoCode;
            opening.Moves = dto.Moves;
            await _openingRepository.UpdateAsync(opening);

        }

        public async Task DeleteAsync(Guid id)
        {
            await _openingRepository.DeleteAsync(id);
        }

        private static OpeningDto MapToDto(Opening opening) => new()
        {
            Id = opening.Id,
            Name = opening.Name,
            EcoCode = opening.EcoCode,
            Moves = opening.Moves
        };
    }
}
