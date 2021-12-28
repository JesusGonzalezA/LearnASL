using System.Threading.Tasks;
using Infraestructure.Interfaces;
using Core.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infraestructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task SendPasswordRecoveryEmail(string email, string token)
        {
            MimeMessage message = CreateMessage(
                email,
                "Password recovery",
                "Please change your password by clicking on this link."
            );
            await SendAsync(message);
        }

        public async Task SendEmailConfirmationEmail(string email, string token)
        {
            MimeMessage message = CreateMessage(
                email,
                "Email confirmation",
                "Please confirm your email by clicking on this link."
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
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Learn ASL", _emailOptions.UserName));
            mailMessage.To.Add(MailboxAddress.Parse(emailDestination));
            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = message
            };
            return mailMessage;
        }
    }
}
