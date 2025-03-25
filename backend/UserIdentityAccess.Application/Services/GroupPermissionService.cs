using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Interfaces;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Services;

public class GroupPermissionService (IRepository<GroupPermission> permissionGroupRepository, IMapper mapper): IGroupPermissionService
{
    public async Task<ServiceResponse<GroupPermissionDto>> CreateGroupPermissionAsync(int groupId, int permissionId)
    {
        var permissionGroup = new GroupPermission(groupId, permissionId);
        await permissionGroupRepository.AddAsync(permissionGroup);
        await permissionGroupRepository.SaveChangesAsync();
        var data = mapper.Map<GroupPermissionDto>(permissionGroup);
        return new ServiceResponse<GroupPermissionDto>(true, data, []);
    }

    public async Task<ServiceResponse<bool>> DeleteGroupPermissionAsync(int groupId, int permissionId)
    {
        var permissionGroups = await permissionGroupRepository.GetWhereAsync(v => v.PermissionId == permissionId && v.GroupId == groupId);
        var permissionGroup =  permissionGroups.FirstOrDefault();
        if (permissionGroup != null) 
            return new ServiceResponse<bool>(false, false, ["GroupPermission does not exist."]);;

        permissionGroupRepository.Remove(permissionGroup!);
        await permissionGroupRepository.SaveChangesAsync();
        return new ServiceResponse<bool>(true, true, []);;
    }

    public async Task<ServiceResponse<IEnumerable<GroupPermissionCountDto>>> GetGroupPermissionCountsAsync()
    {
        var result = await permissionGroupRepository.GetGroupedAsync(
            ug => ug.Group.Name,
            g => new GroupPermissionCountDto
            {
                GroupName = g.Key,
                PermissionCount = g.Count()
            });
        return new ServiceResponse<IEnumerable<GroupPermissionCountDto>>(true,result,[]);
    }
    
    public async Task<ServiceResponse<IEnumerable<PermissionGroupCountDto>>> GetPermissionGroupCountsAsync()
    {
        var result = await permissionGroupRepository.GetGroupedAsync(
            ug => ug.Permission.Name,
            g => new PermissionGroupCountDto
            {
                PermissionName = g.Key,
                GroupCount = g.Count()
            });
        return new ServiceResponse<IEnumerable<PermissionGroupCountDto>>(true,result,[]);
    }
}