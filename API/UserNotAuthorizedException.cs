namespace API;

public class UserNotAuthorizedException : Exception
{
    public UserNotAuthorizedException(string message) : base(message)
    {
    }
}