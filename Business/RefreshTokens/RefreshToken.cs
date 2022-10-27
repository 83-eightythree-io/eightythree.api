using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Business.Users;

namespace Business.RefreshTokens;

public class RefreshToken
{
    public const string Key = "refreshToken";
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; protected set; }
    
    public string Token { get; protected set; }
    
    public DateTime CreatedAt { get; protected set; }
    
    public DateTime ExpiresAt { get; protected set; }
    
    public User User { get; protected set; }
    
    public Guid UserId { get; protected set; }

    public RefreshToken()
    {}

    public RefreshToken(string token, DateTime expiresAt, User user)
    {
        Token = token;
        CreatedAt = DateTime.Now;
        ExpiresAt = expiresAt;
        User = user;
    }

    public static RefreshToken Generate(User user) => new(
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), 
        DateTime.Now.AddDays(7),
        user
        );

    public bool isValid => ExpiresAt > DateTime.Now;
}