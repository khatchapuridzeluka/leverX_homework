using leverX.Application.Helpers;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.DTOs.Tournaments;

namespace leverX.Application.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IPlayerRepository _playerRepository;
        public TournamentService(ITournamentRepository tournamentRepository, IPlayerRepository playerRepository)
        {
            _tournamentRepository = tournamentRepository;
            _playerRepository = playerRepository;
        }
        public async Task<TournamentDto> CreateAsync(CreateTournamentDto dto)
        {
            var tournament = new Tournament
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
            };
            await _tournamentRepository.AddAsync(tournament);
            return DtoMapper.MapToDto(tournament);
        }
        public async Task<TournamentDto?> GetByIdAsync(Guid id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            return tournament == null ? null : DtoMapper.MapToDto(tournament);
        }
        public async Task<IEnumerable<TournamentDto>> GetAllAsync()
        {
            var tournaments = await _tournamentRepository.GetAllAsync();
            return tournaments.Select(DtoMapper.MapToDto).ToList();
        }
        public async Task UpdateAsync(Guid id, UpdateTournamentDto dto)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);

            // TODO: Create custom exception to handle 
            if (tournament == null)
                throw new Exception("Tournament not found");

            tournament.Name = dto.Name;
            tournament.StartDate = dto.StartDate;
            tournament.EndDate = dto.EndDate;
            tournament.Location = dto.Location;
            await _tournamentRepository.UpdateAsync(tournament);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _tournamentRepository.DeleteAsync(id);
        }
    }
}
