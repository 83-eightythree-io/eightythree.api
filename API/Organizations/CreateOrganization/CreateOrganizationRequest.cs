using System.Text.Json.Serialization;

namespace API.Organizations.CreateOrganization;

public class CreateOrganizationRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("account")]
    public string Account { get; set; }

    [JsonPropertyName("termsAndConditionsAccepted")]
    public bool TermsAndConditionsAccepted { get; set; }
}