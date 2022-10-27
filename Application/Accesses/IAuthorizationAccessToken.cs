namespace Application.Accesses;

public interface IAuthorizationAccessToken
{
    string GrantAccessToken(IDictionary<string, string> claims);
}