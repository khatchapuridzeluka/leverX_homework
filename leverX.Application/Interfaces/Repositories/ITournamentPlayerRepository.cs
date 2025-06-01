using leverX.Domain.Entities;
using leverX.DTOs.TournamentPlayers;

namespace leverX.Application.Interfaces.Repositories
{
    public interface ITournamentPlayerRepository
    {
        Task AddAsync(TournamentPlayer entity);
        Task<TournamentPlayer?> GetByIdAsync(Guid tournamentId, Guid playerId);
        Task<List<TournamentPlayer>> GetAllAsync();
        Task UpdateAsync(TournamentPlayer entity);
        Task DeleteAsync(Guid tournamentId, Guid playerId);
    }
}
