namespace UserIdentityAccess.Domain.Entities;
public class Permission
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    
    private readonly List<GroupPermission> _groupPermissions = new();
    public IReadOnlyCollection<GroupPermission> GroupPermissions => _groupPermissions.AsReadOnly();
    
    private Permission() { }

    public Permission(string name)
    {
        SetName(name);
    }
    
    public Permission(int id, string name)
    {
        Id = id;
        SetName(name);
    }
    
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Permission name cannot be empty.", nameof(name));

        Name = name;
    }
    
    public void AddGroupPermission(GroupPermission groupPermissions)
    {
        _groupPermissions.Add(groupPermissions);
    }
}