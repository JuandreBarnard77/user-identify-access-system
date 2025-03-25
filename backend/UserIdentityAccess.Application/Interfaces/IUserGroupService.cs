using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Interfaces;

public interface IUserGroupService
{
    Task<ServiceResponse<UserGroupDto>> CreateUserGroupAsync(int userId, int groupId);
    Task<ServiceResponse<bool>> DeleteUserGroupAsync(int userId, int groupId);
    Task<ServiceResponse<IEnumerable<GroupUserCountDto>>> GetGroupUserCountsAsync();
    Task<ServiceResponse<IEnumerable<UserGroupCountDto>>> GetUserGroupCountsAsync();
}