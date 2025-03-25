using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Mappings;
public class GroupPermissionMappingProfile : Profile
{
    public GroupPermissionMappingProfile()
    {
        CreateMap<GroupPermission, GroupPermissionDto>().ReverseMap();
    }
}