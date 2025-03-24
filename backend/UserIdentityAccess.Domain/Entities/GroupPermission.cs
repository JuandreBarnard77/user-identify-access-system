namespace UserIdentityAccess.Domain.Entities;
public class GroupPermission
{
    public int PermissionId { get; private set; }
    public int GroupId { get; private set; }
    
    public Permission Permission { get; private set; } = null!;
    public Group Group { get; private set; } = null!;
    
    private  GroupPermission() {}
    
    public GroupPermission(int userId, int permissionId)
    {
        if (userId <= 0 || permissionId <= 0)
            throw new ArgumentException("UserId and PermissionId must be valid");
        
        GroupId = userId;
        PermissionId = permissionId;
    }
}