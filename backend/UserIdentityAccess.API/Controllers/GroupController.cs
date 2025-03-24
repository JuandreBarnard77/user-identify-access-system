using Microsoft.AspNetCore.Mvc;
using UserIdentityAccess.Application.Services;

namespace UserIdentityAccess.API.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupController(GroupService groupService)
{
    
}