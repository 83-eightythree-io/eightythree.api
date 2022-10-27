using Application.Accesses;
using Application.Users.DeleteUser;
using Application.Users.RecoverPassword;
using Application.Users.ResetPassword;
using Application.Users.SignUp;
using Application.Users.UpdatePassword;
using Business.Users;

namespace DatabaseByEntityFramework.Users;

public class UsersRepository : ISignUpRepository, IUserAccessRepository, IRecoverPasswordRepository, IResetPasswordRepository,
    IUpdatePasswordRepository, IDeleteUserRepository
{
    private readonly Context _context;

    public UsersRepository(Context context)
    {
        _context = context;
    }
    
    public User FindByEmail(string email)
    {
        return _context.Users
            .AsEnumerable()
            .FirstOrDefault(u => u.Email.Value.Equals(email));
    }

    public User FindById(Guid id)
    {
        return _context.Users
            .AsEnumerable()
            .FirstOrDefault(u => u.Id.Equals(id));
    }

    public void Delete(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void SavePassword(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public User? Save(User? user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return _context.Users.AsEnumerable().First(u => u.Email.Value.Equals(user.Email.Value));
    }

    public User FindByEmailAndPassword(string email, string password)
    {
        return _context.Users
            .AsEnumerable()
            .FirstOrDefault(u => u.Email.Value.Equals(email) && u.Password.Value.Equals(password));
    }
}