using System.Text;
using Application.Services.Tokenizer;

namespace TokenGeneratorViaBase64;

public class TokenGeneratorViaBase64 : ITokenizer
{
    public string Generate(string information)
    {
        var dateTime = BitConverter.GetBytes(DateTime.Now.ToBinary());
        var key = Guid.NewGuid().ToByteArray();

        var token = Encoding.UTF8.GetBytes($"{information}|")
            .Concat(dateTime)
            .Concat(key)
            .ToArray();

        return Convert.ToBase64String(token);
    }
}