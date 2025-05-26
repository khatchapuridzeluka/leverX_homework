using leverX.DTOs.TournamentPlayers;

namespace leverX.Application.Interfaces.Services
{
    public interface ITournamentPlayerService
    {
        Task<TournamentPlayerDto?> GetByIdAsync(Guid tournamentId, Guid playerId);
        Task<List<TournamentPlayerDto>> GetAllAsync();
        Task<TournamentPlayerDto> CreateAsync(CreateTournamentPlayerDto dto);
        Task UpdateAsync(Guid tournamentId, Guid playerId, UpdateTournamentPlayerDto dto);
        Task DeleteAsync(Guid tournamentId, Guid playerId);
    }
}
