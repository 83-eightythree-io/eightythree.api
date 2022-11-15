using System.Text.Json.Serialization;

namespace API.Organizations.UpdateOrganization;

public class UpdateOrganizationRequest
{
    [JsonPropertyName("name")]
    public string Name { get; }
    
    [JsonPropertyName("account")]
    public string Account { get; }
}