using AutoMapper;
using leverX.Domain.Entities;
using leverX.DTOs.TournamentPlayers;

namespace leverX.Application.Mappings { 
public class TournamentPlayerProfile : Profile
{
    public TournamentPlayerProfile()
    {
        CreateMap<CreateTournamentPlayerDto, TournamentPlayer>();
        CreateMap<UpdateTournamentPlayerDto, TournamentPlayer>();
        CreateMap<TournamentPlayer, TournamentPlayerDto>();
    }
}
}
