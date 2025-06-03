using AutoMapper;
using leverX.Application.Helpers;
using leverX.Application.Helpers.Constants;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.Domain.Exceptions;
using leverX.DTOs.Openings;

namespace leverX.Application.Services
{
    public class OpeningService : IOpeningService
    {
        private readonly IOpeningRepository _openingRepository;
        private readonly IMapper _mapper;

        public OpeningService(IOpeningRepository openingRepository, IMapper mapper)
        {
            _openingRepository = openingRepository;
            _mapper = mapper;
        }

        //creates a new game asyncronously - needs async/await to avoid blocking the thread.
        public async Task<OpeningDto> CreateAsync(CreateOpeningDto dto)
        {
            var opening = _mapper.Map<Opening>(dto);
            opening.Id = Guid.NewGuid();

            await _openingRepository.AddAsync(opening);

            return _mapper.Map<OpeningDto>(opening);
        }

        // fetches an opening by id - needs async/await to avoid blocking the thread.
        public async Task<OpeningDto?> GetByIdAsync(Guid id)
        {
            var opening = await _openingRepository.GetByIdAsync(id);
            return opening == null ? null : _mapper.Map<OpeningDto>(opening);
        }

        // fetches all openings - needs async/await to avoid blocking the thread.
        public async Task<IEnumerable<OpeningDto>> GetAllAsync()
        {
            var openings = await _openingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OpeningDto>>(openings);
        }

        // updates an existing opening by id - needs async/await to avoid blocking the thread.
        public async Task UpdateAsync(Guid id, UpdateOpeningDto dto)
        {
            var opening = await _openingRepository.GetByIdAsync(id);
            if (opening == null)
                throw new NotFoundException(ExceptionMessages.OpeningNotFound);

            _mapper.Map(dto, opening); 
            await _openingRepository.UpdateAsync(opening);
        }

        // deletes an opening by id - needs async/await to avoid blocking the thread.
        public async Task DeleteAsync(Guid id)
        {
            await _openingRepository.DeleteAsync(id);
        }
    }
}
