using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class TournamentPlayerRepository : ITournamentPlayerRepository
    {
        private readonly List<TournamentPlayer> _tournamentPlayers = new();

        public Task AddAsync(TournamentPlayer entity)
        {
            _tournamentPlayers.Add(entity);
            return Task.CompletedTask;
        }

        public Task<TournamentPlayer?> GetByIdAsync(Guid tournamentId, Guid playerId)
        {
            var tournamentPlayer = _tournamentPlayers
                .FirstOrDefault(tp => tp.TournamentId == tournamentId && tp.PlayerId == playerId);
            return Task.FromResult(tournamentPlayer);
        }

        public Task<List<TournamentPlayer>> GetAllAsync()
        {
            return Task.FromResult(_tournamentPlayers.ToList());
        }

        public Task UpdateAsync(TournamentPlayer entity)
        {
            var existing = _tournamentPlayers
                .FirstOrDefault(tp => tp.TournamentId == entity.TournamentId && tp.PlayerId == entity.PlayerId);
            if(existing != null)
            {
                existing.FinalRank = entity.FinalRank;
                existing.Score = entity.Score;
            }
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Guid tournamentId, Guid playerId)
        {
            _tournamentPlayers.RemoveAll(tp =>
               tp.TournamentId == tournamentId && tp.PlayerId == playerId);
            return Task.CompletedTask;
        }
    }
}
