namespace UserIdentityAccess.Application.DTOs;

public class GroupPermissionDto
{
    public int GroupId { get; set; }
    public int PermissionId { get; set; }
}

public class PermissionGroupCountDto
{
    public string PermissionName { get; set; } = String.Empty;
    public int GroupCount { get; set; }
}

public class GroupPermissionCountDto
{
    public string GroupName { get; set; } = String.Empty;
    public int PermissionCount { get; set; }
}