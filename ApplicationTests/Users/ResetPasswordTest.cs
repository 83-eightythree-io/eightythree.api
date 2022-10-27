using Application.Services.Hashing;
using Application.Services.Tokenizer;
using Application.Users.ResetPassword;
using Application.Users.ResetPassword.Exceptions;
using Business.Users;

namespace ApplicationTests.Users;

[TestClass]
public class ResetPasswordTest
{
    [TestMethod]
    [ExpectedException(typeof(PasswordsDoNotMatchException))]
    public void UserShouldNot_ResetPassword_IfPasswordsDoNotMatch()
    {
        var service = new ResetPasswordService(
            new ResetPasswordHashStub(), 
            new ResetPasswordTokenDecoderStub(), 
            new PasswordResetRepositoryNullableStub());

        service.Execute(new ResetPasswordCommand("token", "12345678", "11111111"));
    }
    
    [TestMethod]
    [ExpectedException(typeof(ExpiredTokenException))]
    public void UserShouldNot_ResetPassword_IfTokenIsExpired()
    {
        var service = new ResetPasswordService(
            new ResetPasswordHashStub(), 
            new ResetPasswordTokenDecoderExpiredStub(), 
            new PasswordResetRepositoryNullableStub());

        service.Execute(new ResetPasswordCommand("token", "12345678", "12345678"));
    }
    
    [TestMethod]
    [ExpectedException(typeof(UserNotFoundException))]
    public void UserShouldNot_ResetPassword_IfUserNotFound()
    {
        var service = new ResetPasswordService(
            new ResetPasswordHashStub(),
            new ResetPasswordTokenDecoderStub(),
            new PasswordResetRepositoryNullableStub());

        service.Execute(new ResetPasswordCommand("token", "12345678", "12345678"));
    }

    [TestMethod]
    public void UserShould_ResetPassword()
    {
        var service = new ResetPasswordService(
            new ResetPasswordHashStub(),
            new ResetPasswordTokenDecoderStub(),
            new PasswordResetRepositoryStub());

        var result = service.Execute(new ResetPasswordCommand("token", "12345678", "12345678"));
        
        Assert.IsTrue(result);
    }
}

class ResetPasswordHashStub : IHash
{
    public string Hash(string value)
    {
        return value;
    }
}

class ResetPasswordTokenDecoderExpiredStub : ITokenDecoder
{
    public string Decode(string token)
    {
        return $"john.doe@email.com;url;{DateTime.Now.AddHours(-3).ToBinary()}";
    }
}

class ResetPasswordTokenDecoderStub : ITokenDecoder
{
    public string Decode(string token)
    {
        return $"john.doe@email.com;url;{DateTime.Now.ToBinary()}";
    }
}

class PasswordResetRepositoryNullableStub : IResetPasswordRepository
{
    public User FindByEmail(string email)
    {
        return null;
    }

    public void SavePassword(User user)
    {
    }
}

class PasswordResetRepositoryStub : IResetPasswordRepository
{
    public User FindByEmail(string email)
    {
        return User.Standard("John Doe", new Email("john.doe@email.com"), new Password("12345678", s => s));
    }

    public void SavePassword(User user)
    {
    }
}