using System.Text.Json.Serialization;

namespace Application.Users.GetUsersList;

public class GetUsersListResult
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("users")]
    public IEnumerable<GetUserResult> Users { get; set; }
}