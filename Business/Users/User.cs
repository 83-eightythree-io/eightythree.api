using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Business.Organizations;
using Business.RefreshTokens;

namespace Business.Users;

public class User
{
    private const int MinimumNameLength = 4;
    private const int MaximumNameLength = 60;
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; protected set; }
    
    [Required]
    [MinLength(MinimumNameLength), MaxLength(MaximumNameLength)]
    public string Name { get; protected set; }
    
    public Email Email { get; protected set; }
    
    public Password Password { get; protected set; }
    
    public DateTime CreatedAt { get; }
    
    public bool Deleted { get; private set; }
    
    public DateTime DeletedAt { get; private set; }
    
    public Role Role { get; protected set; }
    
    public ICollection<RefreshToken> RefreshTokens { get; protected set; }
    
    public ICollection<Organization> Organizations { get; protected set; }

    public User()
    {
        RefreshTokens = new List<RefreshToken>();
        Organizations = new List<Organization>();
    }

    private User(string name, Email email, Password password, Role role)
    {
        if (string.IsNullOrEmpty(name))
            throw new NullOrEmptyNameException($"Name can not be empty");

        if (name.Length is < MinimumNameLength or > MaximumNameLength)
            throw new InvalidNameException($"Name is invalid. Name length must be between {MinimumNameLength} and {MaximumNameLength}");
        
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.Now;
        Role = role;
        RefreshTokens = new List<RefreshToken>();
        Organizations = new List<Organization>();
    }

    public static User Standard(string name, Email email, Password password)
    {
        return new User(name, email, password, Role.CreateStandard);
    }

    public static User Admin(string name, Email email, Password password)
    {
        return new User(name, email, password, Role.CreateAdmin);
    }

    public void UpdatePassword(Password password)
    {
        Password = password;
    }

    public void Delete()
    {
        Deleted = true;
        DeletedAt = DateTime.Now;
        Email = new Email($"{GetHashString(Email.Value)}@eightythree.io");
        Name = GetHashString(Name);
    }

    public void CreateOrganization(Organization organization)
    {
        Organizations.Add(organization);
    }
    
    private static byte[] GetHash(string inputString)
    {
        using (HashAlgorithm algorithm = SHA256.Create())
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    private static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString().Replace('-', '.').Replace('@', '.')[..20];
    }
}