namespace UserIdentityAccess.Domain.Entities;
public class User
{
    public int Id { get;  private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    
    private readonly List<UserGroup> _userGroups = new();
    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    private User() { }

    public User(string firstName, string lastName , string email)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
    }

    public void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));

        FirstName = firstName;
    }
    
    public void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

        LastName = lastName;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Invalid email address.", nameof(email));

        Email = email;
    }

    public void AddUserGroup(UserGroup userGroup)
    {
        _userGroups.Add(userGroup);
    }
}

