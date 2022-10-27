using Application.Services.Hashing;
using Business.Users;

namespace Application.Users.SignUp;

public class SignUpService : IService<SignUpCommand, User>
{
    private readonly ISignUpRepository _repository;
    private readonly IHash _hash;

    public SignUpService(ISignUpRepository repository, IHash hash)
    {
        _repository = repository;
        _hash = hash;
    }
    
    public User? Execute(SignUpCommand command)
    {
        var user = _repository.FindByEmail(command.Email);
        if (user is not null)
            throw new UserAlreadyExistsException($"There's already an user with email {command.Email}");

        user = User.Standard(
            command.Name,
            new Email(command.Email),
            new Password(command.Password, _hash.Hash)
            );

        return _repository.Save(user);
    }
}