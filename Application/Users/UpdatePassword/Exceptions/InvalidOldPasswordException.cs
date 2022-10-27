namespace Application.Users.UpdatePassword.Exceptions;

public class InvalidOldPasswordException : ApplicationException
{
    public InvalidOldPasswordException(string message) : base(message)
    {
    }
}