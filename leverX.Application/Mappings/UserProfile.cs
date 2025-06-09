using AutoMapper;
using leverX.Domain.Entities;
using leverX.Dtos.DTOs.Users;

namespace leverX.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<User, UserDto>();
        }
    }
}
