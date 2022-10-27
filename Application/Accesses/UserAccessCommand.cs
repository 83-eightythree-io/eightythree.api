using Newtonsoft.Json;

namespace Application.Accesses;

public class UserAccessCommand
{
    [JsonProperty("email")]
    public string Email { get; }
    
    [JsonProperty("password")]
    public string Password { get; }

    public UserAccessCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}