namespace Business.Users;

public class ShortPasswordException : BusinessException
{
    public ShortPasswordException(string message) : base(message)
    {
    }
}