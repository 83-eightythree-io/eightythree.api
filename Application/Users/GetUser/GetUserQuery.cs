namespace Application.Users.GetUser;

public class GetUserQuery
{
    public Guid UserId { get; }

    public GetUserQuery(Guid userId)
    {
        UserId = userId;
    }
}