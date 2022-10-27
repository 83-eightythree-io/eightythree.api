namespace Application.Users.ResetPassword.Exceptions;

public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}