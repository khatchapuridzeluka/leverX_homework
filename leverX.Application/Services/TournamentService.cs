using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.DTOs.Tournaments;

namespace leverX.Application.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        public TournamentService(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }
        public async Task<TournamentDto> CreateAsync(CreateTournamentDto dto)
        {
            var tournament = new Tournament
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location
            };
            await _tournamentRepository.AddAsync(tournament);
            return MapToDto(tournament);
        }
        public async Task<TournamentDto?> GetByIdAsync(Guid id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            return tournament == null ? null : MapToDto(tournament);
        }
        public async Task<List<TournamentDto>> GetAllAsync()
        {
            var tournaments = await _tournamentRepository.GetAllAsync();
            return tournaments.Select(MapToDto).ToList();
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

        private static TournamentDto MapToDto(Tournament tournament) => new()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            Location = tournament.Location
        };
    }
}
