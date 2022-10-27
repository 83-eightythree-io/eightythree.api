namespace Application.Users.UpdatePassword.Exceptions;

public class UserNotFoundException : ApplicationException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}