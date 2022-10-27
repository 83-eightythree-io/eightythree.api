using Business.RefreshTokens;

namespace Application.Accesses;

public class UserAccessResult
{
    public string AccessToken { get; }
    public RefreshToken RefreshToken { get; }

    public UserAccessResult(string accessToken, RefreshToken refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}