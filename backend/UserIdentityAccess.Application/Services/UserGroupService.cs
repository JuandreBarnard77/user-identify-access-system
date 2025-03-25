using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Interfaces;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Services;
public class UserGroupService(IRepository<UserGroup> userGroupRepository, IMapper mapper): IUserGroupService
{
    public async Task<ServiceResponse<UserGroupDto>> CreateUserGroupAsync(int userId, int groupId)
    {
        var userGroup = new UserGroup(userId,groupId);
        await userGroupRepository.AddAsync(userGroup);
        await userGroupRepository.SaveChangesAsync();
        var data = mapper.Map<UserGroupDto>(userGroup);
        return new ServiceResponse<UserGroupDto>(true, data, []);
    }

    public async Task<ServiceResponse<bool>> DeleteUserGroupAsync(int userId, int groupId)
    {
        var userGroups = await userGroupRepository.GetWhereAsync(v => v.UserId == userId && v.GroupId == groupId);
        var userGroup =  userGroups.FirstOrDefault();
        if (userGroup != null) 
            return new ServiceResponse<bool>(false, false, ["UserGroup does not exist."]);;

        userGroupRepository.Remove(userGroup!);
        await userGroupRepository.SaveChangesAsync();
        return new ServiceResponse<bool>(true, true, []);;
    }

    public async Task<ServiceResponse<IEnumerable<GroupUserCountDto>>> GetGroupUserCountsAsync()
    {
        var result = await userGroupRepository.GetGroupedAsync(
            ug => ug.Group.Name,
            g => new GroupUserCountDto
            {
                GroupName = g.Key,
                UserCount = g.Count()
            });
        return new ServiceResponse<IEnumerable<GroupUserCountDto>>(true,result,[]);
    }
    
    public async Task<ServiceResponse<IEnumerable<UserGroupCountDto>>> GetUserGroupCountsAsync()
    {
        var result = await userGroupRepository.GetGroupedAsync(
            ug => ug.User.FirstName + " " + ug.User.LastName,
            g => new UserGroupCountDto
            {
                UserName = g.Key,
                GroupCount = g.Count()
            });
        return new ServiceResponse<IEnumerable<UserGroupCountDto>>(true,result,[]);
    }
}