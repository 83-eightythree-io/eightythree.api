using System.Text.Json.Serialization;

namespace API;

public class Error
{
    [JsonPropertyName("error")]
    public string Value { get; }

    public Error(string value)
    {
        Value = value;
    }
}