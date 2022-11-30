namespace Business.UserOrganizations;

public class UserOrganizationRole
{
    private const string AdminRole = "admin";
    private const string MemberRole = "member";
    
    public string Role { get; protected set; }
    
    private UserOrganizationRole(string role)
    {
        Role = role;
    }

    public static UserOrganizationRole Admin => new UserOrganizationRole(AdminRole);

    public static UserOrganizationRole Member => new UserOrganizationRole(MemberRole);

    public bool IsAdmin => Role.Equals(AdminRole);
}