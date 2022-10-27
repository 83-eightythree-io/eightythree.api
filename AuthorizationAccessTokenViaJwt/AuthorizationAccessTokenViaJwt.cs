using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Accesses;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationAccessTokenViaJwt;

public class AuthorizationAccessTokenViaJwt : IAuthorizationAccessToken
{
    private readonly string _subject;
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;

    public AuthorizationAccessTokenViaJwt(string subject, string key, string issuer, string audience)
    {
        _subject = subject;
        _key = key;
        _issuer = issuer;
        _audience = audience;
    }
    
    public string GrantAccessToken(IDictionary<string, string> info)
    {
        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, _subject),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
        };
        
        claims.AddRange(info.Select(claim => new Claim(claim.Key, claim.Value)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signIn);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}