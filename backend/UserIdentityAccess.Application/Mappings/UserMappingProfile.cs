using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Mappings;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<int, TotalCountDto>()
            .ForMember(dest => dest.Count, 
                opt => opt.MapFrom(src => src));
    }
}