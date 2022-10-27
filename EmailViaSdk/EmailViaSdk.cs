using System.Net;
using System.Net.Mail;
using Application.Services.Mailing;

namespace EmailViaSdk;

public class EmailViaSdk : IEmail
{
    private readonly SmtpClient _client;

    public EmailViaSdk(string server, int port, bool secure, string user, string password)
    {
        this._client = new SmtpClient(server)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(user, password),
            EnableSsl = secure,
            Port = port
        };
    }

    public void Send(string from, IList<string> to, string subject, string body)
    {
        var addresses = new MailAddressCollection();
        foreach (var address in to)
        {
            addresses.Add(address);
        }
        
        _client.Send(new MailMessage
        {
            From = new MailAddress(from),
            Subject = subject,
            Body = body,
            To =
            {
                to.First()
            },
            IsBodyHtml = true
        });
    }
}