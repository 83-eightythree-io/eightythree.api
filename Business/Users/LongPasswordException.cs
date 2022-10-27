namespace Business.Users;

public class LongPasswordException : BusinessException
{
    public LongPasswordException(string message) : base(message)
    {
    }
}