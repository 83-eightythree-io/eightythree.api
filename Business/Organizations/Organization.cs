using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Organizations;

public class Organization
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; protected set; }
    
    public string Name { get; protected set; }
    
    public string Account { get; protected set; }
    
    public bool TermsAndConditionsAccepted { get; protected set; }

    public Organization()
    {
    }

    public Organization(string name, string account, bool termsAndConditionsAccepted)
    {
        Name = name;
        Account = account;
        TermsAndConditionsAccepted = termsAndConditionsAccepted;
    }

    public void UpdateName(string name) => Name = name;

    public void UpdateAccount(string account) => Account = account;
}