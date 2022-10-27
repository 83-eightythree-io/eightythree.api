namespace Application.Users.RefreshToken;

public interface IGetRefreshTokenRepository
{
    void Save(Business.RefreshTokens.RefreshToken refreshToken);
    Business.RefreshTokens.RefreshToken FindByToken(string token);
}