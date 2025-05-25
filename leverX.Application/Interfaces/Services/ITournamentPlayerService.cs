using leverX.DTOs.TournamentPlayers;

namespace leverX.Application.Interfaces.Services
{
    public interface ITournamentPlayerService : ICrudService<TournamentPlayerDto, CreateTournamentPlayerDto, UpdateTournamentPlayerDto>
    {
    }
}
