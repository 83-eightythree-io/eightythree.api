using System.Text.Json.Serialization;

namespace API.Users.GetUsersList;

public class UsersListParameters
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    
    [JsonPropertyName("size")]
    public int Size { get; set; }
    
    [JsonPropertyName("sort")]
    public string Sort { get; set; }
    
    [JsonPropertyName("order")]
    public string Order { get; set; }
    
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
    
    [JsonPropertyName("search")]
    public string Search { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    public UsersListParameters()
    {
    }
}