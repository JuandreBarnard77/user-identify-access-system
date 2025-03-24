using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Interfaces;
using UserIdentityAccess.Application.Validators;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Services;
public class UserService(IRepository<User> userRepository, IMapper mapper) : IUserService
{
    public async Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();
        var data = mapper.Map<IEnumerable<UserDto>>(users);
        return new ServiceResponse<IEnumerable<UserDto>>(true, data, []);
    }

    public async Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return new ServiceResponse<UserDto>(false, null,
                ["User not found."]);
        }
        var data = mapper.Map<UserDto>(user);
        return new ServiceResponse<UserDto>(true, data, []);
    }

    public async Task<ServiceResponse<UserDto>> CreateUserAsync(UserDto userDto)
    {
        var user = new User(userDto.FirstName, userDto.LastName, userDto.Email);
        var validator = new UserValidator();
        var validationResult = await validator.ValidateAsync(userDto);
        if (!validationResult.IsValid)
        {
            return new ServiceResponse<UserDto>(false, null,
                validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();
        var data = mapper.Map<UserDto>(user);
        return new ServiceResponse<UserDto>(true, data, []);
    }

    public async Task<ServiceResponse<UserDto>> UpdateUserAsync(int id, UserDto userDto)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null) 
            return new ServiceResponse<UserDto>(false, null, ["User not found."]);;
        var validator = new UserValidator();
        var validationResult = await validator.ValidateAsync(userDto);
        if (!validationResult.IsValid)
        {
            return new ServiceResponse<UserDto>(false, null,
                validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        user.SetFirstName(userDto.FirstName);
        user.SetLastName(userDto.LastName);
        user.SetEmail(userDto.Email);

        userRepository.Update(user);
        await userRepository.SaveChangesAsync();
        var data = mapper.Map<UserDto>(user);
        return new ServiceResponse<UserDto>(true, data, []);
    }

    public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null) return new ServiceResponse<bool>(false, false, ["User not found."]);;

        userRepository.Remove(user);
        await userRepository.SaveChangesAsync();
        return new ServiceResponse<bool>(true, true, []);;
    }

    public async Task<TotalCountDto> GetAllUserTotalCountAsync()
    {
        var userCount = await userRepository.CountAsync();
        return mapper.Map<TotalCountDto>(userCount);
    }
    
    public async Task<TotalCountDto> GetGroupTotalCountPerUserAsync()
    {
        var userCount = await userRepository.CountAsync();
        return mapper.Map<TotalCountDto>(userCount);
    }
    
    public async Task<List<User>> GetUsersInGroupAsync(int groupId)
    {
        return (await userRepository.GetWhereAsync(u => u.UserGroups.Any(ug => ug.GroupId == groupId))).ToList();
    }
    
    public async Task<int> GetUserCountInGroupAsync(int groupId)
    {
        return await userRepository.CountAsync(u => u.UserGroups.Any(ug => ug.GroupId == groupId));
    }
    
    public async Task<IEnumerable<User>> GetUsersWithGroupsAsync()
    {
        return await userRepository.GetWithIncludesAsync(u => true, u => u.UserGroups, u => u.UserGroups.Select(ug => ug.Group));
    }
}