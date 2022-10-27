using Business.Users;

namespace Application.Users.RecoverPassword;

public interface IRecoverPasswordRepository
{
    User FindByEmail(string email);
}