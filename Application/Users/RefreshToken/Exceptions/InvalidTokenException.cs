namespace Application.Users.RefreshToken.Exceptions;

public class InvalidTokenException : ApplicationException
{
    public InvalidTokenException(string message) : base(message)
    {
    }
}