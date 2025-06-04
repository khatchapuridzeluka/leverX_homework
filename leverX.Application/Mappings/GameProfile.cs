using AutoMapper;
using leverX.Domain.Entities;
using leverX.Dtos.DTOs.Games;
using leverX.DTOs.Games;

namespace leverX.Application.Mappings
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<CreateGameDto, Game>()
                .ForMember(dest => dest.WhitePlayer, opt => opt.Ignore())
                .ForMember(dest => dest.BlackPlayer, opt => opt.Ignore())
                .ForMember(dest => dest.Opening, opt => opt.Ignore())
                .ForMember(dest => dest.Tournament, opt => opt.Ignore());

            CreateMap<UpdateGameDto, Game>()
                .ForMember(dest => dest.WhitePlayer, opt => opt.Ignore())
                .ForMember(dest => dest.BlackPlayer, opt => opt.Ignore())
                .ForMember(dest => dest.Opening, opt => opt.Ignore())
                .ForMember(dest => dest.Tournament, opt => opt.Ignore());

            // Entity -> DTO
            CreateMap<Game, GameDto>()
                .ForMember(dest => dest.WhitePlayerId, opt => opt.MapFrom(src => src.WhitePlayer.Id))
                .ForMember(dest => dest.BlackPlayerId, opt => opt.MapFrom(src => src.BlackPlayer.Id))
                .ForMember(dest => dest.OpeningId, opt => opt.MapFrom(src => src.Opening.Id))
                .ForMember(dest => dest.TournamentId, opt => opt.MapFrom(src => src.Tournament != null ? src.Tournament.Id : (Guid?)null));
        }
    }
}
