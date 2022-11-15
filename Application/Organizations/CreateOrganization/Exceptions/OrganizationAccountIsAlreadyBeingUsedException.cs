namespace Application.Organizations.CreateOrganization.Exceptions;

public class OrganizationAccountIsAlreadyBeingUsedException : Exception
{
    public OrganizationAccountIsAlreadyBeingUsedException(string message) : base(message)
    {
        
    }
}