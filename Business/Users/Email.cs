using System.Text.RegularExpressions;

namespace Business.Users;

public class Email
{
    public string Value { get; protected set; }

    public Email(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new NullOrEmptyEmailException($"The email cannot be empty");

        if (!Valid(value))
            throw new InvalidEmailException($"The email {value} is invalid");

        Value = value;
    }
    
    public static bool Valid(string email) => new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").IsMatch(email);
}