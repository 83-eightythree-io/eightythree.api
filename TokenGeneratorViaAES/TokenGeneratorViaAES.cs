using System.Security.Cryptography;
using System.Text;
using Application.Services.Tokenizer;

namespace TokenGeneratorViaAES;

public class TokenGeneratorViaAES : ITokenizer, ITokenDecoder
{
    private readonly string _key;

    public TokenGeneratorViaAES(string key)
    {
        _key = key;
    }
    
    public string Generate(string information)
    {
        var iv = new byte[16];  
        byte[] array;  
  
        using (var aes = Aes.Create())  
        {  
            aes.Key = Encoding.UTF8.GetBytes(_key);  
            aes.IV = iv;  
  
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  
  
            using (var memoryStream = new MemoryStream())  
            {  
                using (var cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))  
                {  
                    using (var streamWriter = new StreamWriter((Stream)cryptoStream))  
                    {  
                        streamWriter.Write(information);  
                    }  
  
                    array = memoryStream.ToArray();  
                }  
            }  
        }  
  
        return Convert.ToBase64String(array);  
    }

    public string Decode(string token)
    {
        var iv = new byte[16];  
        var buffer = Convert.FromBase64String(token);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_key);  
        aes.IV = iv;
        var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream((Stream)memoryStream, decrypt, CryptoStreamMode.Read);
        using var streamReader = new StreamReader((Stream)cryptoStream);
        return streamReader.ReadToEnd();
    }
}