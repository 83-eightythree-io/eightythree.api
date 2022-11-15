namespace Application.Organizations.CreateOrganization;

public class CreateOrganizationCommand
{
    public string UserEmail { get; }
    
    public string Name { get; }
    
    public string Account { get; }

    public bool TermsAndConditionsAccepted { get; }

    public CreateOrganizationCommand(string userEmail, string name, string account, bool termsAndConditionsAccepted)
    {
        UserEmail = userEmail;
        Name = name;
        Account = account;
        TermsAndConditionsAccepted = termsAndConditionsAccepted;
    }
}