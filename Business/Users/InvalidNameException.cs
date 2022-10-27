namespace Business.Users;

public class InvalidNameException : BusinessException
{
    public InvalidNameException(string message) : base(message)
    {
    }
}