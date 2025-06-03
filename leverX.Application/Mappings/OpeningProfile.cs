using AutoMapper;
using leverX.Domain.Entities;
using leverX.DTOs.Openings;

namespace LeverX.Application.Mappings
{
    public class OpeningProfile : AutoMapper.Profile
    {
        public OpeningProfile()
        {
            CreateMap<Opening, OpeningDto>();
            CreateMap<CreateOpeningDto, Opening>();
            CreateMap<UpdateOpeningDto, Opening>();
        }
    }
}
