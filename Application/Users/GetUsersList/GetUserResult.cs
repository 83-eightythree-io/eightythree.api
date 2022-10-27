using System.Text.Json.Serialization;

namespace Application.Users.GetUsersList;

public class GetUserResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; }
    
    [JsonPropertyName("name")]
    public string Name { get; }
    
    [JsonPropertyName("email")]
    public string Email { get; }
    
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; }
    
    [JsonPropertyName("deletedAt")]
    public DateTime DeletedAt { get; }
    
    [JsonPropertyName("deleted")]
    public bool Deleted { get; }
}