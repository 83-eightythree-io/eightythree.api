using Business.Users;

namespace Application.Users.ResetPassword;

public interface IResetPasswordRepository
{
    User FindByEmail(string email);
    void SavePassword(User user);
}