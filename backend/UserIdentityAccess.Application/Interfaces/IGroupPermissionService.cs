using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Interfaces;

public interface IGroupPermissionService
{
    Task<ServiceResponse<GroupPermissionDto>> CreateGroupPermissionAsync(int groupId, int permissionId);
    Task<ServiceResponse<bool>> DeleteGroupPermissionAsync(int groupId, int permissionId);
    Task<ServiceResponse<IEnumerable<PermissionGroupCountDto>>> GetPermissionGroupCountsAsync();
    Task<ServiceResponse<IEnumerable<GroupPermissionCountDto>>> GetGroupPermissionCountsAsync();
}