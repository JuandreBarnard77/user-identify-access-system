namespace UserIdentityAccess.Domain.Entities;
public class Group
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    
    private readonly List<UserGroup> _userGroups = [];
    public IReadOnlyList<UserGroup> UserGroups => _userGroups.AsReadOnly();
    
    private readonly List<GroupPermission> _groupPermissions = [];
    public IReadOnlyList<GroupPermission> GroupPermissions => _groupPermissions.AsReadOnly();

    private Group() { }

    public Group(string name)
    {
        SetName(name);
    }
    
    public Group(int id, string name)
    {
        Id = id;
        SetName(name);
    } 
    
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Group name cannot be empty.", nameof(name));

        Name = name;
    }

    public void AddUser(User user)
    {
        _userGroups.Add(new UserGroup(user.Id, Id));
    }

    public void AddPermission(Permission permission)
    {
        _groupPermissions.Add(new GroupPermission(Id, permission.Id));
    }
}