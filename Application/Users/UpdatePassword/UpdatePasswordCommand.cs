using System.Text.Json.Serialization;

namespace Application.Users.UpdatePassword;

public class UpdatePasswordCommand
{
    public string OldPassword { get; }
    public string NewPassword { get; }
    public Guid UserId { get; }

    public UpdatePasswordCommand(Guid userId, string oldPassword, string newPassword)
    {
        UserId = userId;
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}