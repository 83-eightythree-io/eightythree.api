using Business.Users;

namespace Application.Users.DeleteUser;

public interface IDeleteUserRepository
{
    User FindById(Guid id);
    void Delete(User user);
}