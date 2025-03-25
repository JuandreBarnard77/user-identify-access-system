using AutoMapper;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Interfaces;
using UserIdentityAccess.Application.Validators;
using UserIdentityAccess.Domain.Entities;

namespace UserIdentityAccess.Application.Services;

public class PermissionService(IRepository<Permission> permissionRepository, IRepository<User> userRepository, IMapper mapper): IPermissionService
{
    public async Task<ServiceResponse<IEnumerable<PermissionDto>>> GetAllPermissionsAsync()
    {
        var permissions = await permissionRepository.GetAllAsync();
        var data = mapper.Map<IEnumerable<PermissionDto>>(permissions);
        return new ServiceResponse<IEnumerable<PermissionDto>>(true, data, []);
    }

    public async Task<ServiceResponse<PermissionDto>> GetPermissionByIdAsync(int id)
    {
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null)
        {
            return new ServiceResponse<PermissionDto>(false, null,
                ["Permission not found."]);
        }
        var data = mapper.Map<PermissionDto>(permission);
        return new ServiceResponse<PermissionDto>(true, data, []);
    }

    public async Task<ServiceResponse<PermissionDto>> CreatePermissionAsync(PermissionDto permissionDto)
    {
        var permission = new Permission(permissionDto.Name);
        var validator = new PermissionValidator();
        var validationResult = await validator.ValidateAsync(permissionDto);
        if (!validationResult.IsValid)
        {
            return new ServiceResponse<PermissionDto>(false, null,
                validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        await permissionRepository.AddAsync(permission);
        await permissionRepository.SaveChangesAsync();
        var data = mapper.Map<PermissionDto>(permission);
        return new ServiceResponse<PermissionDto>(true, data, []);
    }

    public async Task<ServiceResponse<PermissionDto>> UpdatePermissionAsync(int id, PermissionDto permissionDto)
    {
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null) 
            return new ServiceResponse<PermissionDto>(false, null, ["Permission not found."]);;
        var validator = new PermissionValidator();
        var validationResult = await validator.ValidateAsync(permissionDto);
        if (!validationResult.IsValid)
        {
            return new ServiceResponse<PermissionDto>(false, null,
                validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        permission.SetName(permissionDto.Name);

        permissionRepository.Update(permission);
        await permissionRepository.SaveChangesAsync();
        var data = mapper.Map<PermissionDto>(permission);
        return new ServiceResponse<PermissionDto>(true, data, []);
    }

    public async Task<ServiceResponse<bool>> DeletePermissionAsync(int id)
    {
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null) return new ServiceResponse<bool>(false, false, ["Permission not found."]);;

        permissionRepository.Remove(permission);
        await permissionRepository.SaveChangesAsync();
        return new ServiceResponse<bool>(true, true, []);;
    }
    
    public async Task<TotalCountDto> GetAllPermissionTotalCountAsync()
    {
        var permissionCount = await permissionRepository.CountAsync();
        return mapper.Map<TotalCountDto>(permissionCount);
    }
}