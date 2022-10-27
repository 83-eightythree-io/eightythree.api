namespace Application.Accesses.Exceptions;

public class InvalidUserCredentialsException : ApplicationException
{
    public InvalidUserCredentialsException(string message) : base(message)
    {
    }
}