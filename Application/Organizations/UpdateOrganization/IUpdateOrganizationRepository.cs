using Business.Organizations;
using Business.UserOrganizations;

namespace Application.Organizations.UpdateOrganization;

public interface IUpdateOrganizationRepository
{
    UserOrganization FindByUserEmailAndOrganizationId(string email, Guid organizationId);
    Organization FindByAccount(string account);
    void Update(Organization organization);
}