using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Interfaces;

public interface IPermissionService
{
    Task<ServiceResponse<IEnumerable<PermissionDto>>> GetAllPermissionsAsync();
    Task<ServiceResponse<PermissionDto>> GetPermissionByIdAsync(int id);
    Task<ServiceResponse<PermissionDto>> CreatePermissionAsync(PermissionDto permissionDto);
    Task<ServiceResponse<PermissionDto>> UpdatePermissionAsync(int id, PermissionDto permissionDto);
    Task<ServiceResponse<bool>> DeletePermissionAsync(int id);
    Task<TotalCountDto> GetAllPermissionTotalCountAsync();
}