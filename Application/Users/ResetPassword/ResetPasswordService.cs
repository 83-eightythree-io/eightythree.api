using Application.Services.Hashing;
using Application.Services.Tokenizer;
using Application.Users.ResetPassword.Exceptions;
using Business.Users;

namespace Application.Users.ResetPassword;

public class ResetPasswordService : IService<ResetPasswordCommand, bool>
{
    private const int TokenExpirationTime = 2;
    
    private readonly IHash _hash;
    private readonly ITokenDecoder _decoder;
    private readonly IResetPasswordRepository _repository;

    public ResetPasswordService(IHash hash, ITokenDecoder decoder, IResetPasswordRepository repository)
    {
        _hash = hash;
        _decoder = decoder;
        _repository = repository;
    }
    
    public bool Execute(ResetPasswordCommand command)
    {
        if (!command.ArePasswordsEquals)
            throw new PasswordsDoNotMatchException($"The passwords do not match");

        var password = new Password(command.Password, _hash.Hash);

        var information = _decoder
            .Decode(command.Token)
            .Split(';', StringSplitOptions.TrimEntries);

        var email = information[0];
        var generatedAt = DateTime.FromBinary(Convert.ToInt64(information[2]));

        if (generatedAt.AddHours(TokenExpirationTime) < DateTime.Now)
            throw new ExpiredTokenException($"The token is expired");

        var user = _repository.FindByEmail(email);
        if (user is null)
            throw new UserNotFoundException($"User not found");
        
        user.UpdatePassword(password);
        _repository.SavePassword(user);

        return true;
    }
}