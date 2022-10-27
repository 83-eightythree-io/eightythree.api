namespace Application.Users.ResetPassword.Exceptions;

public class ExpiredTokenException : ApplicationException
{
    public ExpiredTokenException(string message) : base(message)
    {
    }
}