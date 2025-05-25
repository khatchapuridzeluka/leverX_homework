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

        public async Task UpdateAsync(Guid id, UpdatePlayerDto dto)
        {
            var existing = await _playerRepository.GetByIdAsync(id);
            if(existing == null)
                throw new Exception("Player not found");
            existing.Name = dto.Name;
            existing.LastName = dto.LastName;
            existing.FideRating = dto.FideRating;
            existing.Title = dto.Title;
            existing.Nationality = dto.Nationality;
            existing.Sex = dto.Sex;
            await _playerRepository.UpdateAsync(existing);
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
