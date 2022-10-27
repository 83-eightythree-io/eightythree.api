using System.Net.Mail;

namespace Application.Services.Mailing;

public interface IEmail
{
    void Send(string from, IList<string> to, string subject, string body);
}