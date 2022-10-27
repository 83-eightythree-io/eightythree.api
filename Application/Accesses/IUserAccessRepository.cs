using Business.Users;

namespace Application.Accesses;

public interface IUserAccessRepository
{
    User FindByEmailAndPassword(string email, string password);
}