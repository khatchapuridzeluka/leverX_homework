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

        //creates new player asynchronously - needs async/await to avoid blocking the thread.
        public async Task<PlayerDto> CreateAsync(CreatePlayerDto dto)
        {
            var player = _mapper.Map<Player>(dto);
            player.Id = Guid.NewGuid();

            await _playerRepository.AddAsync(player);

            return DtoMapper.MapToDto(player);
        }


        // Fetches a player by id asynchronously - needs async/await to avoid blocking the thread.
        public async Task<PlayerDto?> GetByIdAsync(Guid id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            return player == null ? null : _mapper.Map<PlayerDto>(player);
        }

        // Fetches all players asynchronously - needs async/await to avoid blocking the thread.
        public async Task<IEnumerable<PlayerDto>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(_mapper.Map<PlayerDto>).ToList();
        }

        // Updates an existing player by id asynchronously - needs async/await to avoid blocking the thread.
        public async Task UpdateAsync(Guid id, UpdatePlayerDto dto)
        {
            var player = await _playerRepository.GetByIdAsync(id);

            if(player == null)
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);

            _mapper.Map(dto, player);
            
            await _playerRepository.UpdateAsync(player);
        }

        // Deletes a player by id asynchronously - needs async/await to avoid blocking the thread.
        public async Task DeleteAsync(Guid id)
        {
            await _playerRepository.DeleteAsync(id);
        }

        // Fetches players by rating asynchronously - needs async/await to avoid blocking the thread.
        public async Task<IEnumerable<PlayerDto>> GetByRatingAsync(int rating)
        {
            var players = await _playerRepository.GetByRatingAsync(rating);
            return players.Select(_mapper.Map<PlayerDto>).ToList();
        }
    }
}
