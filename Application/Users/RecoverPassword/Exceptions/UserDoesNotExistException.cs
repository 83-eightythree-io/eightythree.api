namespace Application.Users.RecoverPassword.Exceptions;

public class UserDoesNotExistException : ApplicationException
{
    public UserDoesNotExistException(string message) : base(message)
    {
    }
}