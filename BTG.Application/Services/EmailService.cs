using MailKit.Net.Smtp;
using MimeKit;

namespace BTG.Application.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "{ServerSmtp}"; // Cambia por tu servidor SMTP
        private readonly int _smtpPort = 587; // Puerto del servidor
        private readonly string _smtpUsername = "{UsetSmtp}";
        private readonly string _smtpPassword = "{PasswordSmtp}";

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("BTG Test Notification", _smtpUsername));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = message };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_smtpUsername, _smtpPassword);
                await client.SendAsync(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
