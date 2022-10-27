namespace Application.Users.ResetPassword.Exceptions;

public class PasswordsDoNotMatchException : ApplicationException
{
    public PasswordsDoNotMatchException(string message) : base(message)
    {
    }
}