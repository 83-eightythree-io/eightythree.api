using System.Text.Json.Serialization;

namespace Application.Users.ResetPassword;

public class ResetPasswordCommand
{
    [JsonPropertyName("token")]
    public string Token { get; }
    
    [JsonPropertyName("password")]
    public string Password { get; }
    
    [JsonPropertyName("passwordConfirmation")]
    public string PasswordConfirmation { get; }

    public ResetPasswordCommand(string token, string password, string passwordConfirmation)
    {
        Token = token;
        Password = password;
        PasswordConfirmation = passwordConfirmation;
    }

    [JsonIgnore]
    public bool ArePasswordsEquals => Password.Equals(PasswordConfirmation);
}