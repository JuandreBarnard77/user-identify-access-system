namespace UserIdentityAccess.Domain.Entities;

public class UserGroup
{
    public int UserId { get; private set; }
    public int GroupId { get; private set; }
    
    public User User { get; private set; } = null!;
    public Group Group { get; private set; } = null!;
    
    private  UserGroup() {}
    
    public UserGroup(int userId, int groupId)
    {
        UserId = userId;
        GroupId = groupId;
    }
}