using Business.RefreshTokens;

namespace Application.Accesses;

public interface IRefreshTokenRepository
{
    void Save(RefreshToken refreshToken);
}