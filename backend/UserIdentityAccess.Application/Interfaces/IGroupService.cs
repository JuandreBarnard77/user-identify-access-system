using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Interfaces;

public interface IGroupService
{
    Task<ServiceResponse<IEnumerable<GroupDto>>> GetAllGroupsAsync();
    Task<ServiceResponse<GroupDto>> GetGroupByIdAsync(int id);
    Task<ServiceResponse<GroupDto>> CreateGroupAsync(GroupDto groupDto);
    Task<ServiceResponse<GroupDto>> UpdateGroupAsync(int id, GroupDto groupDto);
    Task<ServiceResponse<bool>> DeleteGroupAsync(int id);
    Task<TotalCountDto> GetAllGroupTotalCountAsync();
    Task<ServiceResponse<List<UserDto>>> GetUsersInGroupAsync(int groupId);
}