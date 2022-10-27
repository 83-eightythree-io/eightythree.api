using Application.Services.Hashing;
using Application.Users.UpdatePassword.Exceptions;
using Business.Users;

namespace Application.Users.UpdatePassword;

public class UpdatePasswordService : IService<UpdatePasswordCommand, bool>
{
    private readonly IHash _hash;
    private readonly IUpdatePasswordRepository _repository;

    public UpdatePasswordService(IHash hash, IUpdatePasswordRepository repository)
    {
        _hash = hash;
        _repository = repository;
    }
    
    public bool Execute(UpdatePasswordCommand command)
    {
        var oldPassword = new Password(command.OldPassword, _hash.Hash);
        var newPassword = new Password(command.NewPassword, _hash.Hash);

        var user = _repository.FindById(command.UserId);
        if (user is null)
            throw new UserNotFoundException($"User not found");

        if (!user.Password.Value.Equals(oldPassword.Value))
            throw new InvalidOldPasswordException($"Invalid old password exception");
        
        user.UpdatePassword(newPassword);
        _repository.SavePassword(user);

        return true;
    }
}