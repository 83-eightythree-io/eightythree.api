using Application.Services.Mailing;
using Application.Services.Tokenizer;
using Application.Users.RecoverPassword;
using Application.Users.RecoverPassword.Exceptions;
using Business.Users;

namespace ApplicationTests.Users;

[TestClass]
public class RecoverPasswordTest
{
    [TestMethod]
    [ExpectedException(typeof(UserDoesNotExistException))]
    public void UserShouldNot_RecoverPassword_IfEmailDoesNotExist()
    {
        var service = new RecoverPasswordService(
            new RecoverPasswordRepositoryNullableStub(), 
            new TokenizerStub(), 
            new EmailStub()
            );

        service.Execute(new RecoverPasswordCommand("john.doe@email.com", "url"));
    }

    [TestMethod]
    public void UserShould_RecoverPassword()
    {
        var service = new RecoverPasswordService(
            new RecoverPasswordRepositoryStub(), 
            new TokenizerStub(), 
            new EmailStub()
        );

        var email = "john.doe@email.com";
        var url = "url";
        var token = service.Execute(new RecoverPasswordCommand(email, url));

        Assert.IsTrue(token.Contains(email));
        Assert.IsTrue(token.Contains(url));
    }
}

class RecoverPasswordRepositoryStub : IRecoverPasswordRepository
{
    public User FindByEmail(string email)
    {
        return User.Standard("John Doe", new Email("john.doe@email.com"), new Password("12345678", s => s));
    }
}

class RecoverPasswordRepositoryNullableStub : IRecoverPasswordRepository
{
    public User FindByEmail(string email)
    {
        return null;
    }
}

class TokenizerStub : ITokenizer
{
    public string Generate(string information)
    {
        return information;
    }
}

class EmailStub : IEmail
{
    public void Send(string from, IList<string> to, string subject, string body)
    {
    }
}