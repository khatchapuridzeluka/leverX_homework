using AutoMapper;
using leverX.Domain.Entities;
using leverX.DTOs.Tournaments;

namespace leverX.Application.Mappings
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile() 
        {
            CreateMap<CreateTournamentDto, Tournament>();
            CreateMap<UpdateTournamentDto, Tournament>();
            CreateMap<Tournament, TournamentDto>();
        }
    }
}
