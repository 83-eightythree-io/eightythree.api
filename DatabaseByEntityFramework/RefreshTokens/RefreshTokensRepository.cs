using Application.Accesses;
using Application.Users.RefreshToken;
using Business.RefreshTokens;
using Microsoft.EntityFrameworkCore;

namespace DatabaseByEntityFramework.RefreshTokens;

public class RefreshTokensRepository : IRefreshTokenRepository, IGetRefreshTokenRepository
{
    private readonly Context _context;

    public RefreshTokensRepository(Context context)
    {
        _context = context;
    }
    
    public void Save(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        _context.SaveChanges();
    }

    public RefreshToken FindByToken(string token)
    {
        return _context
            .RefreshTokens
            .Include(rf => rf.User)
            .AsEnumerable()
            .First(rf => rf.Token.Equals(token));
    }
}