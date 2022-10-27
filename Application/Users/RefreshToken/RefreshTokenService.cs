using Application.Accesses;
using Application.Users.RefreshToken.Exceptions;

namespace Application.Users.RefreshToken;

public class RefreshTokenService : IService<RefreshTokenCommand, RefreshTokenResult>
{
    private readonly IGetRefreshTokenRepository _repository;
    private readonly IAuthorizationAccessToken _authorization;

    public RefreshTokenService(IGetRefreshTokenRepository repository, IAuthorizationAccessToken authorization)
    {
        _repository = repository;
        _authorization = authorization;
    }
    
    public RefreshTokenResult Execute(RefreshTokenCommand command)
    {
        var refreshToken = _repository.FindByToken(command.RefreshToken);
        if (refreshToken is null)
            throw new InvalidTokenException("The refresh token is invalid");

        if (!refreshToken.isValid)
            throw new ExpiredRefreshTokenException("Refresh token is expired");

        var token = _authorization.GrantAccessToken(new Dictionary<string, string>
        {
            { "id", refreshToken.User.Id.ToString() },
            { "email", refreshToken.User.Email.Value },
            { "name", refreshToken.User.Name },
            { "role", refreshToken.User.Role.Value },
        });

        var newRefreshToken = Business.RefreshTokens.RefreshToken.Generate(refreshToken.User);
        _repository.Save(newRefreshToken);

        return new RefreshTokenResult(token, refreshToken);
    }
}