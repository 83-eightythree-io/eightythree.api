namespace Application.Users.RefreshToken;

public class RefreshTokenResult
{
    public string Token { get; }
    public Business.RefreshTokens.RefreshToken RefreshToken { get; }

    public RefreshTokenResult(string token, Business.RefreshTokens.RefreshToken refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }
}