using Business.Organizations;

namespace Application.Organizations.DeleteOrganization;

public interface IDeleteOrganizationRepository
{
    void Delete(Organization organization);
}