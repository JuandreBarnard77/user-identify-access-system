using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Interfaces;
using UserIdentityAccess.Application.Validators;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Services;
public class GroupService(IRepository<Group> groupRepository, IRepository<User> userRepository, IMapper mapper): IGroupService
{
    public async Task<ServiceResponse<IEnumerable<GroupDto>>> GetAllGroupsAsync()
    {
        var groups = await groupRepository.GetAllAsync();
        var data = mapper.Map<IEnumerable<GroupDto>>(groups);
        return new ServiceResponse<IEnumerable<GroupDto>>(true, data, []);
    }

    public async Task<ServiceResponse<GroupDto>> GetGroupByIdAsync(int id)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null)
        {
            return new ServiceResponse<GroupDto>(false, null,
                ["Group not found."]);
        }
        var data = mapper.Map<GroupDto>(group);
        return new ServiceResponse<GroupDto>(true, data, []);
    }

    public async Task<ServiceResponse<GroupDto>> CreateGroupAsync(GroupDto groupDto)
    {
        var group = new Group(groupDto.Name);
        var validator = new GroupValidator();
        var validationResult = await validator.ValidateAsync(groupDto);
        if (!validationResult.IsValid)
        {
            return new ServiceResponse<GroupDto>(false, null,
                validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        await groupRepository.AddAsync(group);
        await groupRepository.SaveChangesAsync();
        var data = mapper.Map<GroupDto>(group);
        return new ServiceResponse<GroupDto>(true, data, []);
    }

    public async Task<ServiceResponse<GroupDto>> UpdateGroupAsync(int id, GroupDto groupDto)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null) 
            return new ServiceResponse<GroupDto>(false, null, ["Group not found."]);;
        var validator = new GroupValidator();
        var validationResult = await validator.ValidateAsync(groupDto);
        if (!validationResult.IsValid)
        {
            return new ServiceResponse<GroupDto>(false, null,
                validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        group.SetName(groupDto.Name);

        groupRepository.Update(group);
        await groupRepository.SaveChangesAsync();
        var data = mapper.Map<GroupDto>(group);
        return new ServiceResponse<GroupDto>(true, data, []);
    }

    public async Task<ServiceResponse<bool>> DeleteGroupAsync(int id)
    {
        var group = await groupRepository.GetByIdAsync(id);
        if (group == null) return new ServiceResponse<bool>(false, false, ["Group not found."]);;

        groupRepository.Remove(group);
        await groupRepository.SaveChangesAsync();
        return new ServiceResponse<bool>(true, true, []);;
    }
    
    public async Task<TotalCountDto> GetAllGroupTotalCountAsync()
    {
        var groupCount = await groupRepository.CountAsync();
        return mapper.Map<TotalCountDto>(groupCount);
    }
    
    public async Task<ServiceResponse<List<UserDto>>> GetUsersInGroupAsync(int groupId)
    {
        var data = mapper.Map<List<UserDto>>((await userRepository.GetWhereAsync(u => u.UserGroups.Any(ug => ug.GroupId == groupId))).ToList());
        return new ServiceResponse<List<UserDto>>(true, data, []);
    }
}