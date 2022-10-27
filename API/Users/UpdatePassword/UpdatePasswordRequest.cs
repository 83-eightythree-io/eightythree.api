using System.Text.Json.Serialization;

namespace API.Users.UpdatePassword;

public class UpdatePasswordRequest
{
    [JsonPropertyName("oldPassword")]
    public string OldPassword { get; }
    
    [JsonPropertyName("newPassword")]
    public string NewPassword { get; }

    public UpdatePasswordRequest(string oldPassword, string newPassword)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}