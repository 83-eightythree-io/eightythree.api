using Business.Users;

namespace Application.Organizations.CreateOrganization;

public interface IUserOrganizationRepository
{
    User FindByEmail(string email);
}