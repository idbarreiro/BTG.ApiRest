namespace BTG.Application.Interfaces
{
    public interface INotificationTransactionServiceAsync
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        void SendSms(string toPhoneNumber, string message);
    }
}
