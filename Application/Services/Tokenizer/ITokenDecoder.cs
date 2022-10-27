namespace Application.Services.Tokenizer;

public interface ITokenDecoder
{
    string Decode(string token);
}