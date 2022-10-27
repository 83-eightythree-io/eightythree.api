namespace Business.Users;

public class NullOrEmptyPasswordException : BusinessException
{
    public NullOrEmptyPasswordException(string message) : base(message)
    {
    }
}