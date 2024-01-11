using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SendEmail.Models;
using SimpleEmailApp.Models;

namespace SendEmail.Services.EmailService;
public class EmailService : IEmailService
{
    private readonly EmailConfigDto _config;

    public EmailService(EmailConfigDto config)
    {
        _config = config;
    }

    public async Task SendEmail(EmailDto request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.Username));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_config.Username, _config.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}

