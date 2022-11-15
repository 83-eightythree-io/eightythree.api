using Application.Organizations.CreateOrganization.Exceptions;
using Business.Organizations;

namespace Application.Organizations.CreateOrganization;

public class CreateOrganizationService : IService<CreateOrganizationCommand, Organization>
{
    private readonly IUserOrganizationRepository _userRepository;
    private readonly ICreateOrganizationRepository _repository;

    public CreateOrganizationService(IUserOrganizationRepository userRepository, ICreateOrganizationRepository repository)
    {
        _userRepository = userRepository;
        _repository = repository;
    }
    
    public Organization Execute(CreateOrganizationCommand command)
    {
        if (!command.TermsAndConditionsAccepted)
            throw new TermsAndConditionsNotAcceptedException("User has not accepted terms and conditions");
        
        var user = _userRepository.FindByEmail(command.UserEmail);
        if (user is null)
            throw new UserNotFoundException("User not found");

        var organization = _repository.FindByAccount(command.Account);
        if (organization is not null)
            throw new OrganizationAccountIsAlreadyBeingUsedException($"There is an organization already with the same account {command.Account}");
        
        organization = new Organization(command.Name, command.Account, command.TermsAndConditionsAccepted);
        user.CreateOrganization(organization);

        _repository.SaveOrganization(user, organization);

        return organization;
    }
}