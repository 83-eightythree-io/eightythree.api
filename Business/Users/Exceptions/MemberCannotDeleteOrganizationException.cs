namespace Business.Users.Exceptions;

public class MemberCannotDeleteOrganizationException : BusinessException
{
    public MemberCannotDeleteOrganizationException(string message) : base(message)
    {
    }
}