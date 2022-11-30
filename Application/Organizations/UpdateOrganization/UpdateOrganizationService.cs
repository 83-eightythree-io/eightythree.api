using Application.Organizations.UpdateOrganization.Exceptions;
using Business.Organizations;

namespace Application.Organizations.UpdateOrganization;

public class UpdateOrganizationService : IService<UpdateOrganizationCommand, Organization>
{
    private readonly IUpdateOrganizationRepository _repository;

    public UpdateOrganizationService(IUpdateOrganizationRepository repository)
    {
        _repository = repository;
    }
    
    public Organization Execute(UpdateOrganizationCommand command)
    {
        var userOrganization = _repository.FindByUserEmailAndOrganizationId(command.UserEmail, command.Id);
        if (userOrganization is null)
            throw new UserIsNotPartOfOrganizationException($"User is not part of organization");

        var organizationAccount = _repository.FindByAccount(command.Account);
        if (organizationAccount is not null)
            throw new OrganizationAccountAlreadyExistsException($"Organization account {command.Account} already exists");

        userOrganization.Organization.UpdateName(command.Name);
        userOrganization.Organization.UpdateAccount(command.Account);
        
        _repository.Update(userOrganization.Organization);
        return userOrganization.Organization;
    }
}