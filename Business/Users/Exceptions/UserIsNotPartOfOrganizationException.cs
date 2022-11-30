namespace Business.Users.Exceptions;

public class UserIsNotPartOfOrganizationException : BusinessException
{
    public UserIsNotPartOfOrganizationException(string message) : base(message)
    {
    }
}