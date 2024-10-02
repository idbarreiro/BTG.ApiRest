using BTG.Application.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BTG.Application.Services
{
    public class NotificationTransactionService : INotificationTransactionServiceAsync
    {
        private readonly string _smtpServer = "{ServerSmtp}"; // Cambia por tu servidor SMTP
        private readonly int _smtpPort = 587; // Puerto del servidor
        private readonly string _smtpUsername = "{UsetSmtp}";
        private readonly string _smtpPassword = "{PasswordSmtp}";

        private readonly string _accountSid = "{SidTwilio}"; // Obtén esto de Twilio
        private readonly string _authToken = "{TokenTwilio}"; // Obtén esto de Twilio

        public NotificationTransactionService()
        {
            TwilioClient.Init(_accountSid, _authToken);
        }

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

        public void SendSms(string toPhoneNumber, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = new PhoneNumber("{PhoneSend}"), // Tu número Twilio
                Body = message
            };
            var msg = MessageResource.Create(messageOptions);
        }
    }
}
