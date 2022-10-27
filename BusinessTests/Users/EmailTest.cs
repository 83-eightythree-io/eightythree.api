using Business.Users;

namespace BusinessTests.Users;

[TestClass]
public class EmailTest
{
    [TestMethod]
    [ExpectedException(typeof(NullOrEmptyEmailException))]
    public void EmailShouldNot_BeNullOrEmpty()
    {
        var email = new Email("");
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidEmailException))]
    public void EmailShouldNot_BeInvalid()
    {
        var email = new Email("joe@company");
    }
}