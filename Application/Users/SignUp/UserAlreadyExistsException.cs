namespace Application.Users.SignUp;

public class UserAlreadyExistsException : ApplicationException
{
    public UserAlreadyExistsException(string message) : base(message)
    {
    }
}