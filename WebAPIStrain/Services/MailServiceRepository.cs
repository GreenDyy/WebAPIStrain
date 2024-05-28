
using Microsoft.Extensions.Options;
using MimeKit;
using WebAPIStrain.Models;

namespace WebAPIStrain.Services
{
    public class MailServiceRepository : IMailServiceRepository
    {
        private readonly MailSettings _mailSettings;

        public MailServiceRepository(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendMailAsync(string toEmail, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            email.To.Add(new MailboxAddress(toEmail, toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = message };
            email.Body = builder.ToMessageBody();

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                await smtp.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Username, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }

    }
}
