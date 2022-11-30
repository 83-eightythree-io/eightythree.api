namespace Application.Organizations.DeleteOrganization;

public class DeleteOrganizationCommand
{
    public string UserEmail { get; }
    public Guid OrgnizationId { get; }
    
    public DeleteOrganizationCommand(string userEmail, Guid id)
    {
        UserEmail = userEmail;
        OrgnizationId = id;
    }
}