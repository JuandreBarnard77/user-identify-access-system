namespace UserIdentityAccess.Application.DTOs;

public class UserGroupDto
{
    public int GroupId { get; set; }
    public int UserId { get; set; }
}

public class UserGroupCountDto
{
    public string UserName { get; set; } = String.Empty;
    public int GroupCount { get; set; }
}

public class GroupUserCountDto
{
    public string GroupName { get; set; } = String.Empty;
    public int UserCount { get; set; }
}