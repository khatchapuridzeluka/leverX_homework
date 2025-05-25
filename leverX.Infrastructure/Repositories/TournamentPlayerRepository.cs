using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class TournamentPlayerRepository
    {
        private readonly List<TournamentPlayer> _tournamentPlayers = new();
        public Task AddAsync(TournamentPlayer tournamentPlayer)
        {
            _tournamentPlayers.Add(tournamentPlayer);
            return Task.CompletedTask;
        }
        public Task<TournamentPlayer?> GetByIdAsync(Guid id)
        {
            var tournamentPlayer = _tournamentPlayers.FirstOrDefault(tp => tp.TournamentId == id);
            return Task.FromResult(tournamentPlayer);
        }
        public Task<List<TournamentPlayer>> GetAllAsync()
        {
            return Task.FromResult(_tournamentPlayers);
        }
        public Task UpdateAsync(TournamentPlayer tournamentPlayer)
        {
            var existing = _tournamentPlayers.FirstOrDefault(tp => tp.TournamentId == tournamentPlayer.TournamentId);
            if (existing != null)
            {
                existing.TournamentId = tournamentPlayer.TournamentId;
                existing.PlayerId = tournamentPlayer.PlayerId;
                existing.FinalRank = tournamentPlayer.FinalRank;
                existing.Score = tournamentPlayer.Score;
            }
            return Task.CompletedTask;
        }
        public Task DeleteAsync(Guid id)
        {
            var tournamentPlayer = _tournamentPlayers.FirstOrDefault(tp => tp.TournamentId == id);
            if (tournamentPlayer != null)
            {
                _tournamentPlayers.Remove(tournamentPlayer);
            }
            return Task.CompletedTask;
        }   
    }
}
