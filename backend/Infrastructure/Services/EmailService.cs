using System.Threading.Tasks;
using Core.Interfaces;
using Core.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly FrontendOptions _frontendOptions;

        public EmailService
        (
            IOptions<EmailOptions> emailOptions,
            IOptions<FrontendOptions> frontendOptions
        )
        {
            _emailOptions = emailOptions.Value;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task SendPasswordRecoveryEmail(string email, string token)
        {
            string link = $"{_frontendOptions.Host}/auth/password-recovery/{email}/{token}";
            string linkHtml = $"<a href=\"{link}\">here</a>";

            MimeMessage message = CreateMessage(
                email,
                "Password recovery",
                $"<p>Please change your password by clicking {linkHtml}.<p>"
            );
            await SendAsync(message);
        }

        public async Task SendEmailConfirmationEmail(string email, string token)
        {
            string link = $"{_frontendOptions.Host}/auth/email-confirmation/{email}/{token}";
            string linkHtml = $"<a href=\"{link}\">here</a>";

            MimeMessage message = CreateMessage(
                email,
                "Email confirmation",
                $"<p>Please confirm your email by clicking {linkHtml}.<p>"
            );
            await SendAsync(message);
        }

        private async Task SendAsync(MimeMessage message)
        {
            using SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect(_emailOptions.Host, _emailOptions.Port, true);
            smtpClient.Authenticate(_emailOptions.UserName, _emailOptions.Password);
            await smtpClient.SendAsync(message);
            smtpClient.Disconnect(true);
        }

        private MimeMessage CreateMessage(string emailDestination, string subject, string message)
        {
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;

            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Learn ASL", _emailOptions.UserName));
            mailMessage.To.Add(MailboxAddress.Parse(emailDestination));
            mailMessage.Subject = subject;
            mailMessage.Body = bodyBuilder.ToMessageBody();

            return mailMessage;
        }
    }
}
