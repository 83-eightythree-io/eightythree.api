using Business.Organizations;
using Business.Users;

namespace Application.Organizations.CreateOrganization;

public interface ICreateOrganizationRepository
{
    Organization FindByAccount(string account);
    void SaveOrganization(User user, Organization organization);
}