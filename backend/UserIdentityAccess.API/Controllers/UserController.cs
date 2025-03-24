using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserIdentityAccess.Application.DTOs;
using UserIdentityAccess.Application.Services;

namespace UserIdentityAccess.API.Controllers;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Gets list of users
    /// </summary>
    /// <returns>List of users</returns>
    [HttpGet]
    [SwaggerResponse(200, "Users retrieved successfully", typeof(UserDto))]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Gets an user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The requested user details.</returns>
    [HttpGet("{id}")]
    [SwaggerResponse(200, "User retrieved successfully", typeof(UserDto))]
    [SwaggerResponse(404, "User not found")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userService.GetUserByIdAsync(int.Parse(id));
        return user.Success ? Ok(user.Data) : NotFound(user.Errors);
    }

    /// <summary>
    /// Inserts a new user.
    /// </summary>
    /// <param name="userBody">The user details.</param>
    /// <returns>The newly created user.</returns>
    [HttpPost]
    [SwaggerResponse(200, "User created successfully", typeof(UserDto))]
    [SwaggerResponse(400, "Invalid request")]
    public async Task<IActionResult> AddUser([FromBody] UserDto userBody)
    {
        var createdUser = await _userService.CreateUserAsync(userBody);
        if (createdUser.Success)
        {
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Data!.Id }, createdUser.Data);
        } 
        return BadRequest(createdUser.Errors);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="userBody">The updated user details.</param>
    /// <returns>The updated user.</returns>
    [HttpPut("{id}")]
    [SwaggerResponse(200, "User updated successfully", typeof(UserDto))]
    [SwaggerResponse(404, "User not found")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userBody)
    {
        var response = await _userService.UpdateUserAsync(int.Parse(id), userBody);
        return response.Success ? Ok(response.Data) : BadRequest(response.Errors);
    }

    /// <summary>
    /// Deletes an user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>Success or failure message.</returns>
    [HttpDelete("{id}")]
    [SwaggerResponse(204, "User deleted successfully")]
    [SwaggerResponse(404, "User not found")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var deleted = await _userService.DeleteUserAsync(int.Parse(id));
        return deleted.Success ? NoContent() : NotFound();
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountUsers()
    {
        var count = await _userService.GetAllUserTotalCountAsync();
        return Ok(count);
    }
    
    [HttpGet("group-count")]
    public async Task<IActionResult> CountGroups()
    {
        throw new NotImplementedException();
    }
    
}
