using Business.Users;

namespace BusinessTests.Users;

[TestClass]
public class UserTest
{
    [TestMethod]
    [ExpectedException(typeof(NullOrEmptyNameException))]
    public void NameShouldNot_BeNullOrEmpty()
    {
        var user = User.Standard(null, new Email("john.doe@email.com"), new Password("12345678", s => s));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void NameShouldNot_BeInvalid()
    {
        var user = User.Standard("Joe", new Email("john.doe@email.com"), new Password("12345678", s => s));
    }
}