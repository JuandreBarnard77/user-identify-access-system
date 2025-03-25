using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Mappings;
public class UserGroupMappingProfile : Profile
{
    public UserGroupMappingProfile()
    {
        CreateMap<UserGroup, UserGroupDto>().ReverseMap();
    }
}