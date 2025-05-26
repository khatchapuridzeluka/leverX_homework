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
            return MapToDto(player);
        }

        public async Task<PlayerDto?> GetByIdAsync(Guid id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            return player == null ? null : MapToDto(player);
        }

        public async Task<List<PlayerDto>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(MapToDto).ToList();
        }

        public async Task<PlayerDto> UpdateAsync(Guid id, UpdatePlayerDto dto)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if(player == null)
                throw new Exception("Player not found");
            player.Name = dto.Name;
            player.LastName = dto.LastName;
            player.FideRating = dto.FideRating;
            player.Title = dto.Title;
            player.Nationality = dto.Nationality;
            player.Sex = dto.Sex;
            await _playerRepository.UpdateAsync(player);

            return MapToDto(player);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _playerRepository.DeleteAsync(id);
        }

        public Task<List<PlayerDto>> GetByRating(int rating)
        {
            throw new NotImplementedException();
        }

        private static PlayerDto MapToDto(Player p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            LastName = p.LastName,
            Sex = p.Sex,
            Nationality = p.Nationality,
            FideRating = p.FideRating,
            Title = p.Title
        };
    }
}
