using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Business.Organizations;
using Business.Users;

namespace Business.UserOrganizations;

public class UserOrganization
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; protected set; }
    
    public Organization Organization { get; protected set; }
    
    public User User { get; protected set; }
    
    public UserOrganizationRole Role { get; protected set; }

    public UserOrganization()
    {
    }

    public UserOrganization(User user, Organization organization, UserOrganizationRole role)
    {
        User = user;
        Organization = organization;
        Role = role;
    }

    public static UserOrganization Admin(User user, Organization organization) =>
        new UserOrganization(user, organization, UserOrganizationRole.Admin);

    public static UserOrganization Member(User user, Organization organization) =>
        new UserOrganization(user, organization, UserOrganizationRole.Member);

    public bool IsUserAdmin => Role.IsAdmin;
}