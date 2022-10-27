using Application.Services.Hashing;
using Business.Users;

namespace BusinessTests.Users;

[TestClass]
public class PasswordTest
{
    [TestMethod]
    [ExpectedException(typeof(NullOrEmptyPasswordException))]
    public void PasswordShouldNot_BeNullOrEmpty()
    {
        var password = new Password("", new HashStub().Hash);
    }

    [TestMethod]
    [ExpectedException(typeof(ShortPasswordException))]
    public void PasswordShould_HaveMinimumLength()
    {
        var password = new Password("1234567", new HashStub().Hash);
    }

    [TestMethod]
    [ExpectedException(typeof(LongPasswordException))]
    public void PasswordShould_HaveMaximumLength()
    {
        var password = new Password("1234567890123456712345678901234567", new HashStub().Hash);
    }

    [TestMethod]
    public void PasswordShould_BeHidden_AfterCreated()
    {
        var password = new Password("12345678", new HashStub().Hash);
            
        Assert.AreEqual("12345678@#$", password.Value);
    }

    private class HashStub : IHash
    {
        public string Hash(string value)
        {
            return value + "@#$";
        }
    }
}