using System.Text.Json.Serialization;

namespace Application.Organizations.UpdateOrganization;

public class UpdateOrganizationCommand
{
    public Guid Id { get; }
    
    public string Name { get; }
    
    public string Account { get; }

    public UpdateOrganizationCommand(Guid id, string name, string account)
    {
        Id = id;
        Name = name;
        Account = account;
    }
}