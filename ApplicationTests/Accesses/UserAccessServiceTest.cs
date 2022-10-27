using Application.Accesses;
using Application.Accesses.Exceptions;
using Application.Services.Hashing;
using Business.RefreshTokens;
using Business.Users;

namespace ApplicationTests.Accesses;

[TestClass]
public class UserAccessServiceTest
{
    private const string MockToken = "token";
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserCredentialsException))]
    public void UserShouldNot_GetAccessToken_IfInvalidCredentials()
    {
        var service = new UserAccessService(
            new UserAccessHashStub(), 
            new NullableUserAccessRepositoryStub(), 
            new AuthorizationAccessTokenStub(string.Empty),
            new RefreshTokensRepositoryStub());

        service.Execute(new UserAccessCommand("john.doe@email.com", "12345678"));
    }

    [TestMethod]
    public void UserShould_GetAccessToken()
    {
        var service = new UserAccessService(
            new UserAccessHashStub(), 
            new UserAccessRepositoryStub(), 
            new AuthorizationAccessTokenStub(MockToken),
            new RefreshTokensRepositoryStub());

        var token = service.Execute(new UserAccessCommand("john.doe@email.com", "12345678"));
        
        Assert.AreEqual(MockToken, token);
    }
}

class UserAccessHashStub : IHash
{
    public string Hash(string value)
    {
        return value;
    }
}

class NullableUserAccessRepositoryStub : IUserAccessRepository
{
    public User FindByEmailAndPassword(string email, string password)
    {
        return null;
    }
}

class UserAccessRepositoryStub : IUserAccessRepository
{
    public User FindByEmailAndPassword(string email, string password)
    {
        return User.Standard("John Doe", new Email("john.doe@email.com"), new Password("12345678", s => s));
    }
}

class AuthorizationAccessTokenStub : IAuthorizationAccessToken
{
    public string Token { get; }

    public AuthorizationAccessTokenStub(string token)
    {
        Token = token;
    }
    
    public string GrantAccessToken(IDictionary<string, string> claims)
    {
        return Token;
    }
}

class RefreshTokensRepositoryStub : IRefreshTokenRepository
{
    public void Save(RefreshToken refreshToken)
    {
    }
}