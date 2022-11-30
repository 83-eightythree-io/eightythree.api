namespace Application.Organizations.UpdateOrganization.Exceptions;

public class UserIsNotPartOfOrganizationException : ApplicationException
{
    public UserIsNotPartOfOrganizationException(string message) : base(message)
    {
    }
}