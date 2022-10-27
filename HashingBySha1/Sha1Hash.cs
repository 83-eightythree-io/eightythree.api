using Application.Services.Hashing;

namespace HashingBySha1;

public class Sha1Hash : IHash
{
    public string Hash(string value)
    {
        return value;
    }
}