using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Mappings;
public class PermissionMappingProfile : Profile
{
    public PermissionMappingProfile()
    {
        CreateMap<Permission, PermissionDto>().ReverseMap();
        CreateMap<int, TotalCountDto>()
            .ForMember(dest => dest.Count, 
                opt => opt.MapFrom(src => src));
    }
}