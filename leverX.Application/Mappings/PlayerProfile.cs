using AutoMapper;
using leverX.Domain.Entities;
using leverX.DTOs.Players;

namespace leverX.Application.Mappings
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<CreatePlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();

            CreateMap<Player, PlayerDto>()
                .ForMember(dest => dest.GamesAsWhite, opt => opt.MapFrom(src => src.GamesAsWhite.Select(g => g.Id)))
                .ForMember(dest => dest.GamesAsBlack, opt => opt.MapFrom(src => src.GamesAsBlack.Select(g => g.Id)));
        }
    }
}
