namespace Application.Users.RefreshToken;

public class RefreshTokenCommand
{
    public string RefreshToken { get; }

    public RefreshTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}