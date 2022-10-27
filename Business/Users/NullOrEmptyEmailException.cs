namespace Business.Users;

public class NullOrEmptyEmailException : BusinessException
{
    public NullOrEmptyEmailException(string message) : base(message)
    {
    }
}