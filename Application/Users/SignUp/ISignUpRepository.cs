using Business.Users;

namespace Application.Users.SignUp;

public interface ISignUpRepository
{
    User? FindByEmail(string email);
    User? Save(User? user);
}