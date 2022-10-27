using Application.Services.Hashing;
using Application.Users.SignUp;
using Business.Users;

namespace ApplicationTests.Users;

[TestClass]
public class SignUpTest
{
    [TestMethod]
    [ExpectedException(typeof(UserAlreadyExistsException))]
    public void UserShouldNot_SignUp_IfEmailAlreadyExists()
    {
        var service = new SignUpService(new SignUpStubRepository(), new HashStub());

        service.Execute(new SignUpCommand("John", "john.doe@email.com", "12345678"));
    }

    [TestMethod]
    public void UserShould_SignUp_Successfully()
    {
        var service = new SignUpService(new SignUpStubNullableRepository(), new HashStub());

        var user = service.Execute(new SignUpCommand("John", "john.smith@email.com", "12345678"));
        
        Assert.AreNotEqual(Guid.Empty, user.Id);
    }
}

class SignUpStubRepository : ISignUpRepository
{
    public User? FindByEmail(string email)
    {
        return User.Standard("John", new Email("john.doe@email.com"), new Password("12345678", s => s));
    }

    public User? Save(User? user)
    {
        return user;
    }
}

class SignUpStubNullableRepository : ISignUpRepository
{
    public User? FindByEmail(string email)
    {
        return null;
    }

    public User? Save(User? user)
    {
        var userTest = new UserTest(user.Name, user.Email, user.Password);
        return userTest;
    }
}

class UserTest : User
{
    public UserTest(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = Role.CreateStandard;
        Id = Guid.NewGuid();
    }
}

class HashStub : IHash
{
    public string Hash(string value)
    {
        return value;
    }
}