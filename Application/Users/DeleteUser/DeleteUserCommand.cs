namespace Application.Users.DeleteUser;

public class DeleteUserCommand
{
    public Guid Id { get; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}