namespace Application.Organizations.UpdateOrganization.Exceptions;

public class OrganizationNotFoundException : ApplicationException
{
    public OrganizationNotFoundException(string message) : base(message)
    {
    }
}