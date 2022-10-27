namespace Business.Users;

public class NullOrEmptyNameException : BusinessException
{
    public NullOrEmptyNameException(string message) : base(message)
    {
    }
}