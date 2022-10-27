namespace Business.Users;

public class Role
{
    public const string Standard = "standard";
    public const string Admin = "admin";
    
    public string Value { get; }

    public Role(string role)
    {
        Value = role;
    }

    public static Role CreateStandard => new(Standard);
    public static Role CreateAdmin => new(Admin);
}