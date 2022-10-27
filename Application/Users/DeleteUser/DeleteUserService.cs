using Application.Users.DeleteUser.Exceptions;

namespace Application.Users.DeleteUser;

public class DeleteUserService : IService<DeleteUserCommand, bool>
{
    private readonly IDeleteUserRepository _repository;

    public DeleteUserService(IDeleteUserRepository repository)
    {
        _repository = repository;
    }
    
    public bool Execute(DeleteUserCommand command)
    {
        var user = _repository.FindById(command.Id);
        if (user is null)
            throw new UserNotFoundException($"User not found");
        
        user.Delete();
        _repository.Delete(user);

        return true;
    }
}