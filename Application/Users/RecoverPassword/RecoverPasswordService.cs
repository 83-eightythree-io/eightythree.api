using Application.Services.Mailing;
using Application.Services.Tokenizer;
using Application.Users.RecoverPassword.Exceptions;
using Business.Users;

namespace Application.Users.RecoverPassword;

public class RecoverPasswordService : IService<RecoverPasswordCommand, string>
{
    private readonly IRecoverPasswordRepository _repository;
    private readonly ITokenizer _tokenizer;
    private readonly IEmail _email;

    public RecoverPasswordService(IRecoverPasswordRepository repository, ITokenizer tokenizer, IEmail email)
    {
        _repository = repository;
        _tokenizer = tokenizer;
        _email = email;
    }
    
    public string Execute(RecoverPasswordCommand command)
    {
        var email = new Email(command.Email);

        var user = _repository.FindByEmail(email.Value);
        if (user is null)
            throw new UserDoesNotExistException($"There is no user with email {email.Value}");

        var token = _tokenizer.Generate($"{user.Email.Value};{command.Url};{DateTime.Now.ToBinary()}");
        var resetLink = $"{command.Url}/{token}";

        _email.Send(
            "eightythree@eightythree.io",
            new List<string>() { user.Email.Value },
            "Eightythree | Password reset",
            "<h2>" +
                    "Eightythree | Password reset" +
                "</h2>" +
                $"<p>Hello {user.Name}, here's the link to reset your password:</p>" +
                $"<a href='{resetLink}'>Click here</a>" +
                $"<p>Or copy and paste this link in your browser: {resetLink}</p>" +
                $"<p>Thank you,</p>" +
                $"<p>Team Eightythree</p>"
            );

        return token;
    }
}