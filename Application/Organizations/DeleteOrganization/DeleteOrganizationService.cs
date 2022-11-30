using Application.Organizations.DeleteOrganization.Exceptions;

namespace Application.Organizations.DeleteOrganization;

public class DeleteOrganizationService : IService<DeleteOrganizationCommand, bool> 
{
    private readonly IUserDeleteOrganizationRepository _userRepository;
    private readonly IDeleteOrganizationRepository _repository;

    public DeleteOrganizationService(IUserDeleteOrganizationRepository userRepository, IDeleteOrganizationRepository repository)
    {
        _userRepository = userRepository;
        _repository = repository;
    }
    
    public bool Execute(DeleteOrganizationCommand command)
    {
        var userOrganization = _userRepository.FindByUserEmailAndOrganizationId(command.UserEmail, command.OrgnizationId);
        if (userOrganization is null)
            throw new UserNotFoundException("User not found");

        if (!userOrganization.IsUserAdmin)
            return false;
        
        _repository.Delete(userOrganization.Organization);
        return true;
    }
}