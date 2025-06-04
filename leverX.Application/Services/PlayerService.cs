using AutoMapper;
using leverX.Application.Helpers;
using leverX.Application.Helpers.Constants;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.Domain.Exceptions;
using leverX.DTOs.Players;

namespace leverX.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<PlayerDto> CreateAsync(CreatePlayerDto dto)
        {
            var player = _mapper.Map<Player>(dto);

            await _playerRepository.AddAsync(player);

            return DtoMapper.MapToDto(player);
        }


        public async Task<PlayerDto?> GetByIdAsync(Guid id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            return player == null ? null : _mapper.Map<PlayerDto>(player);
        }

        public async Task<IEnumerable<PlayerDto>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(_mapper.Map<PlayerDto>).ToList();
        }

        public async Task UpdateAsync(Guid id, UpdatePlayerDto dto)
        {
            var player = await _playerRepository.GetByIdAsync(id);

            if(player == null)
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);

            _mapper.Map(dto, player);
            
            await _playerRepository.UpdateAsync(player);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _playerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PlayerDto>> GetByRatingAsync(int rating)
        {
            var players = await _playerRepository.GetByRatingAsync(rating);
            return players.Select(_mapper.Map<PlayerDto>).ToList();
        }
    }
}
