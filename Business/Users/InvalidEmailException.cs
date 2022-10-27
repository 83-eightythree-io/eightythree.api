namespace Business.Users;

public class InvalidEmailException : BusinessException
{
    public InvalidEmailException(string message) : base(message)
    {
    }
}