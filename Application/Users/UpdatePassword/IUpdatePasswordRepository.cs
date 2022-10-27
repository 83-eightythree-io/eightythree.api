using Business.Users;

namespace Application.Users.UpdatePassword;

public interface IUpdatePasswordRepository
{
    User FindById(Guid id);
    void SavePassword(User user);
}