namespace Business.Users;

public class Password
{
    private const int MinimumLength = 8;
    private const int MaximumLength = 32;
    
    public string Value { get; }

    public Password(string value, Func<string, string> hash)
    {
        if (string.IsNullOrEmpty(value))
            throw new NullOrEmptyPasswordException($"Password can not be empty");

        if (value.Length < MinimumLength)
            throw new ShortPasswordException($"Password is too short. The minimum length is {MinimumLength}");

        if (value.Length > MaximumLength)
            throw new LongPasswordException($"Password is too long. The maximum length is {MaximumLength}");

        Value = hash(value);
    }
}