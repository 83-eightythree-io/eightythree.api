using System.Text.Json.Serialization;

namespace Application.Users.SignUp;

public class SignUpCommand
{
    [JsonPropertyName("name")]
    public string Name { get; }
    
    [JsonPropertyName("email")]
    public string Email { get; }
    
    [JsonPropertyName("password")]
    public string Password { get; }

    public SignUpCommand(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}