using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Application.Users.RecoverPassword;

public class RecoverPasswordCommand
{
    [JsonPropertyName("email")]
    public string Email { get; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }

    public RecoverPasswordCommand(string email, string url)
    {
        Email = email;
        Url = url;
    }
}