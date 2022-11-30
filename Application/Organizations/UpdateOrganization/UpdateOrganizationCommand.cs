using System.Text.Json.Serialization;

namespace Application.Organizations.UpdateOrganization;

public class UpdateOrganizationCommand
{
    public string UserEmail { get; }
    
    public Guid Id { get; }
    
    public string Name { get; }
    
    public string Account { get; }

    public UpdateOrganizationCommand(string userEmail, Guid id, string name, string account)
    {
        UserEmail = userEmail;
        Id = id;
        Name = name;
        Account = account;
    }
}