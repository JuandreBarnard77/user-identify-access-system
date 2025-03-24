using UserIdentityAccess.Application.DTOs;

namespace UserIdentityAccess.Application.Interfaces;
public interface IUserService
{
    Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
    Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id);
    Task<ServiceResponse<UserDto>> CreateUserAsync(UserDto userDto);
    Task<ServiceResponse<UserDto>> UpdateUserAsync(int id, UserDto userDto);
    Task<ServiceResponse<bool>> DeleteUserAsync(int id);
}