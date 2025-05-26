using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly List<Tournament> _tournaments = new();
        public Task AddAsync(Tournament tournament)
        {
            _tournaments.Add(tournament);
            return Task.CompletedTask;
        }
        public Task<Tournament?> GetByIdAsync(Guid id)
        {
            var tournament = _tournaments.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(tournament);
        }
        public Task<List<Tournament>> GetAllAsync()
        {
            return Task.FromResult(_tournaments);
        }
        public Task UpdateAsync(Tournament tournament)
        {
            var existingTournament = _tournaments.FirstOrDefault(t => t.Id == tournament.Id);
            if (existingTournament != null)
            {
                existingTournament.Name = tournament.Name;
                existingTournament.StartDate = tournament.StartDate;
                existingTournament.EndDate = tournament.EndDate;
                existingTournament.Location = tournament.Location;
                existingTournament.Players = tournament.Players;
            }
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Guid id)
        {
            var tournament = _tournaments.FirstOrDefault(t => t.Id == id);
            if (tournament != null)
            {
                _tournaments.Remove(tournament);
            }
            return Task.CompletedTask;
        }
    }
}
