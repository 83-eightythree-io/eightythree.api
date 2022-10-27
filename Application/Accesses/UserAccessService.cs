using Application.Accesses.Exceptions;
using Application.Services.Hashing;
using Business.RefreshTokens;
using Business.Users;

namespace Application.Accesses;

public class UserAccessService : IService<UserAccessCommand, UserAccessResult>
{
    private readonly IHash _hash;
    private readonly IUserAccessRepository _repository;
    private readonly IAuthorizationAccessToken _authorizationAccessToken;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public UserAccessService(IHash hash, IUserAccessRepository repository, IAuthorizationAccessToken authorizationAccessToken, IRefreshTokenRepository refreshTokenRepository)
    {
        _hash = hash;
        _repository = repository;
        _authorizationAccessToken = authorizationAccessToken;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public UserAccessResult Execute(UserAccessCommand command)
    {
        var email = new Email(command.Email);
        var password = new Password(command.Password, _hash.Hash);

        var user = _repository.FindByEmailAndPassword(email.Value, password.Value);
        if (user is null)
            throw new InvalidUserCredentialsException("Invalid credentials");

        var token = _authorizationAccessToken.GrantAccessToken(new Dictionary<string, string>
        {
            { "id", user.Id.ToString() },
            { "email", user.Email.Value },
            { "name", user.Name },
            { "role", user.Role.Value },
        });

        var refreshToken = RefreshToken.Generate(user);
        _refreshTokenRepository.Save(refreshToken);

        return new UserAccessResult(token, refreshToken);
    }
}