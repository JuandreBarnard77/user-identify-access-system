using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Interfaces;

namespace UserIdentityAccess.API.Controllers;

[ApiController]
[Route("api/permissions")]
public class PermissionController: ControllerBase
{
    private readonly IPermissionService _permissionService;
    private readonly IGroupPermissionService _groupPermissionService;
    public PermissionController(IPermissionService permissionService, IGroupPermissionService groupPermissionService)
    {
        _permissionService = permissionService;
        _groupPermissionService = groupPermissionService;
    }
    
    /// <summary>
    /// Gets list of permissions
    /// </summary>
    /// <returns>List of permissions</returns>
    [HttpGet]
    [SwaggerResponse(200, "Permissions retrieved successfully", typeof(PermissionDto))]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await _permissionService.GetAllPermissionsAsync();
        return Ok(permissions.Data);
    }

    /// <summary>
    /// Gets an permission by ID.
    /// </summary>
    /// <param name="id">The ID of the permission.</param>
    /// <returns>The requested permission details.</returns>
    [HttpGet("{id}")]
    [SwaggerResponse(200, "Permission retrieved successfully", typeof(PermissionDto))]
    [SwaggerResponse(404, "Permission not found")]
    public async Task<IActionResult> GetPermissionById(string id)
    {
        var permission = await _permissionService.GetPermissionByIdAsync(int.Parse(id));
        return permission.Success ? Ok(permission.Data) : NotFound(permission.Errors);
    }

    /// <summary>
    /// Inserts a new permission.
    /// </summary>
    /// <param name="permissionBody">The permission details.</param>
    /// <returns>The newly created permission.</returns>
    [HttpPost]
    [SwaggerResponse(200, "Permission created successfully", typeof(PermissionDto))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> AddPermission([FromBody] PermissionDto permissionBody)
    {
        var createdPermission = await _permissionService.CreatePermissionAsync(permissionBody);
        if (createdPermission.Success)
        {
            return CreatedAtAction(nameof(GetPermissionById), new { id = createdPermission.Data!.Id }, createdPermission.Data);
        } 
        return BadRequest(createdPermission.Errors);
    }

    /// <summary>
    /// Updates an existing permission.
    /// </summary>
    /// <param name="id">The ID of the permission.</param>
    /// <param name="permissionBody">The updated permission details.</param>
    /// <returns>The updated permission.</returns>
    [HttpPut("{id}")]
    [SwaggerResponse(200, "Permission updated successfully", typeof(PermissionDto))]
    [SwaggerResponse(404, "Permission not found")]
    public async Task<IActionResult> UpdatePermission(string id, [FromBody] PermissionDto permissionBody)
    {
        var response = await _permissionService.UpdatePermissionAsync(int.Parse(id), permissionBody);
        return response.Success ? Ok(response.Data) : BadRequest(response.Errors);
    }

    /// <summary>
    /// Deletes a permission by ID.
    /// </summary>
    /// <param name="id">The ID of the permission.</param>
    /// <returns>Success or failure message.</returns>
    [HttpDelete("{id}")]
    [SwaggerResponse(204, "Permission deleted successfully")]
    [SwaggerResponse(404, "Permission not found")]
    public async Task<IActionResult> DeletePermission(string id)
    {
        var deleted = await _permissionService.DeletePermissionAsync(int.Parse(id));
        return deleted.Success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Gets count of permissions
    /// </summary>
    /// <returns>Count of permissions</returns>
    [HttpGet("count")]
    public async Task<IActionResult> CountPermissions()
    {
        var count = await _permissionService.GetAllPermissionTotalCountAsync();
        return Ok(count);
    }
    
    /// <summary>
    /// Gets list of permissions with the amount of groups assigned to them
    /// </summary>
    /// <returns>List of permissions with group count</returns>
    [HttpGet("group-count")]
    [SwaggerResponse(200, "PermissionGroup retrieved successfully", typeof(IEnumerable<PermissionGroupCountDto>))]
    public async Task<IActionResult> CountGroupsInPermission()
    {
        var permissionGroup = await _groupPermissionService.GetPermissionGroupCountsAsync();
        return Ok(permissionGroup.Data);
    }
}