using SendEmail.Models;

namespace SendEmail.Services.EmailService;
public interface IEmailService
{
    Task SendEmail(EmailDto request);
}
