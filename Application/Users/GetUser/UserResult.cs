using System.Text.Json.Serialization;

namespace Application.Users.GetUser;

public class UserResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; }
    
    [JsonPropertyName("name")]
    public string Name { get; }
    
    [JsonPropertyName("email")]
    public string Email { get; }
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; }

    public UserResult(Guid id, string name, string email, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Email = email;
        CreatedAt = createdAt;
    }
}