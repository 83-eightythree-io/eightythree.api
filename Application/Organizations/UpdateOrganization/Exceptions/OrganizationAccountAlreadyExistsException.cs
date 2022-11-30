namespace Application.Organizations.UpdateOrganization.Exceptions;

public class OrganizationAccountAlreadyExistsException : ApplicationException
{
    public OrganizationAccountAlreadyExistsException(string message) : base(message)
    {
    }
}