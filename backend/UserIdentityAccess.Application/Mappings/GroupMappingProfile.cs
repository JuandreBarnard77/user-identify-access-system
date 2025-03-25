using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Mappings;
public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<int, TotalCountDto>()
            .ForMember(dest => dest.Count, 
                opt => opt.MapFrom(src => src));
    }
}