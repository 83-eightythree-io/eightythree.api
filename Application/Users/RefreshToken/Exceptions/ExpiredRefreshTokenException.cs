namespace Application.Users.RefreshToken.Exceptions;

public class ExpiredRefreshTokenException : ApplicationException
{
    public ExpiredRefreshTokenException(string message) : base(message)
    {
    }
}