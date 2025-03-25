using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Interfaces;

namespace UserIdentityAccess.API.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupController:  ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly IGroupPermissionService _groupPermissionService;
    private readonly IUserGroupService _userGroupService;
    public GroupController(IGroupService groupService, IGroupPermissionService groupPermissionService, IUserGroupService userGroupService)
    {
        _groupService = groupService;
        _groupPermissionService = groupPermissionService;
        _userGroupService = userGroupService;
    }
    
    /// <summary>
    /// Gets list of groups
    /// </summary>
    /// <returns>List of groups</returns>
    [HttpGet]
    [SwaggerResponse(200, "Groups retrieved successfully", typeof(GroupDto))]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _groupService.GetAllGroupsAsync();
        return Ok(groups.Data);
    }

    /// <summary>
    /// Gets an group by ID.
    /// </summary>
    /// <param name="id">The ID of the group.</param>
    /// <returns>The requested group details.</returns>
    [HttpGet("{id}")]
    [SwaggerResponse(200, "Group retrieved successfully", typeof(GroupDto))]
    [SwaggerResponse(404, "Group not found")]
    public async Task<IActionResult> GetGroupById(string id)
    {
        var group = await _groupService.GetGroupByIdAsync(int.Parse(id));
        return group.Success ? Ok(group.Data) : NotFound(group.Errors);
    }

    /// <summary>
    /// Inserts a new group.
    /// </summary>
    /// <param name="groupBody">The group details.</param>
    /// <returns>The newly created group.</returns>
    [HttpPost]
    [SwaggerResponse(200, "Group created successfully", typeof(GroupDto))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> AddGroup([FromBody] GroupDto groupBody)
    {
        var createdGroup = await _groupService.CreateGroupAsync(groupBody);
        if (createdGroup.Success)
        {
            return CreatedAtAction(nameof(GetGroupById), new { id = createdGroup.Data!.Id }, createdGroup.Data);
        } 
        return BadRequest(createdGroup.Errors);
    }

    /// <summary>
    /// Updates an existing group.
    /// </summary>
    /// <param name="id">The ID of the group.</param>
    /// <param name="groupBody">The updated group details.</param>
    /// <returns>The updated group.</returns>
    [HttpPut("{id}")]
    [SwaggerResponse(200, "Group updated successfully", typeof(GroupDto))]
    [SwaggerResponse(404, "Group not found")]
    public async Task<IActionResult> UpdateGroup(string id, [FromBody] GroupDto groupBody)
    {
        var response = await _groupService.UpdateGroupAsync(int.Parse(id), groupBody);
        return response.Success ? Ok(response.Data) : BadRequest(response.Errors);
    }

    /// <summary>
    /// Deletes an group by ID.
    /// </summary>
    /// <param name="id">The ID of the group.</param>
    /// <returns>Success or failure message.</returns>
    [HttpDelete("{id}")]
    [SwaggerResponse(204, "Group deleted successfully")]
    [SwaggerResponse(404, "Group not found")]
    public async Task<IActionResult> DeleteGroup(string id)
    {
        var deleted = await _groupService.DeleteGroupAsync(int.Parse(id));
        return deleted.Success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Gets count of groups
    /// </summary>
    /// <returns>Count of groups</returns>
    [HttpGet("count")]
    public async Task<IActionResult> CountGroups()
    {
        var count = await _groupService.GetAllGroupTotalCountAsync();
        return Ok(count);
    }
    
    /// <summary>
    /// Gets list of groups with the amount of users assigned to them
    /// </summary>
    /// <returns>List of groups with user count</returns>
    [HttpGet("user-count")]
    [SwaggerResponse(200, "Groups retrieved successfully", typeof(IEnumerable<GroupUserCountDto>))]
    public async Task<IActionResult> CountUsersInGroup()
    {
        var groupUser = await _userGroupService.GetGroupUserCountsAsync();
        return Ok(groupUser.Data);
    }
    
    /// <summary>
    /// Gets list of groups with the amount of permissions assigned to them
    /// </summary>
    /// <returns>List of groups with permission count</returns>
    [HttpGet("permission-count")]
    [SwaggerResponse(200, "GroupPermission retrieved successfully", typeof(IEnumerable<GroupPermissionCountDto>))]
    public async Task<IActionResult> CountPermissionsInGroup()
    {
        var groupPermission = await _groupPermissionService.GetGroupPermissionCountsAsync();
        return Ok(groupPermission.Data);
    }
    
    /// <summary>
    /// Inserts a new GroupPermission.
    /// </summary>
    /// <param name="permissionId">The ID of the permission.</param>
    /// <param name="groupId">The ID of the group.</param>
    /// <returns>The newly created groupPermission connection.</returns>
    [HttpPost( "{groupId}/permissions/{permissionId}")]

    [SwaggerResponse(201, "GroupPermission created successfully")]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> AddGroupPermission(string groupId, string permissionId)
    {
        var createdGroupPermission = await _groupPermissionService.CreateGroupPermissionAsync(int.Parse(groupId),int.Parse(permissionId));
        if (createdGroupPermission.Success)
        {
            return Created();
        } 
        return BadRequest(createdGroupPermission.Errors);
    } 
    
    /// <summary>
    /// Deletes an GroupPermission by IDs.
    /// </summary>
    /// <param name="permissionId">The ID of the permission.</param>
    /// <param name="groupId">The ID of the group.</param>
    /// <returns>Success or failure message.</returns>
    [HttpDelete("{groupId}/permissions/{permissionId}")]
    [SwaggerResponse(204, "GroupPermission deleted successfully")]
    [SwaggerResponse(404, "GroupPermission not found")]
    public async Task<IActionResult> DeleteGroupPermission(string groupId, string permissionId)
    {
        var deleted = await _groupPermissionService.DeleteGroupPermissionAsync(int.Parse(groupId),int.Parse(permissionId));
        return deleted.Success ? NoContent() : NotFound();
    }
}