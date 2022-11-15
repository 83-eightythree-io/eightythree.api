namespace Application.Organizations.DeleteOrganization;

public class DeleteOrganizationCommand
{
    public Guid Id { get; }
    
    public DeleteOrganizationCommand(Guid id)
    {
        Id = id;
    }
}