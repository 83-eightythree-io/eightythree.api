namespace Application.Users.DeleteUser.Exceptions;

public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}