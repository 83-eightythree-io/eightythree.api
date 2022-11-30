using Business.UserOrganizations;
using Business.Users;

namespace Application.Organizations.DeleteOrganization;

public interface IUserDeleteOrganizationRepository
{
    UserOrganization FindByUserEmailAndOrganizationId(string email, Guid organizationId);
    User FindByEmailWithOrganizations(string email);
}