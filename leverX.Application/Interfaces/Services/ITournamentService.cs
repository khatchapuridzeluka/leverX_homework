using leverX.DTOs.Tournaments;

namespace leverX.Application.Interfaces.Services
{
    public  interface ITournamentService : ICrudService<TournamentDto, CreateTournamentDto, UpdateTournamentDto>
    {
    }
}
