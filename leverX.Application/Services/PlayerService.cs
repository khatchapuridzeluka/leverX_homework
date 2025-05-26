using leverX.Application.Helpers;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.DTOs.Players;

namespace leverX.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayerDto> CreateAsync(CreatePlayerDto dto)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LastName = dto.LastName,
                Sex = dto.Sex,
                Nationality = dto.Nationality,
                FideRating = dto.FideRating,
                Title = dto.Title
            };
            await _playerRepository.AddAsync(player);
            return DtoMapper.MapToDto(player);
        }

        public async Task<PlayerDto?> GetByIdAsync(Guid id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            return player == null ? null : DtoMapper.MapToDto(player);
        }

        public async Task<List<PlayerDto>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(DtoMapper.MapToDto).ToList();
        }

        public async Task UpdateAsync(Guid id, UpdatePlayerDto dto)
        {
            var player = await _playerRepository.GetByIdAsync(id);

            if(player == null)
                //TODO: CREATE A CUSTOM EXCEPTION
                throw new Exception("Player not found");

            player.Name = dto.Name;
            player.LastName = dto.LastName;
            player.FideRating = dto.FideRating;
            player.Title = dto.Title;
            player.Nationality = dto.Nationality;
            player.Sex = dto.Sex;
            await _playerRepository.UpdateAsync(player);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _playerRepository.DeleteAsync(id);
        }

        public async Task<List<PlayerDto>> GetByRatingAsync(int rating)
        {
            var players = await _playerRepository.GetByRatingAsync(rating);
            return players.Select(DtoMapper.MapToDto).ToList();
        }
    }
}
