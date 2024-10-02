using BTG.Application.Interfaces;
using BTG.Domain.Entities;

namespace BTG.Application.Features.Transactions.Services.SendNotificationTransactionService
{
    public class SendNotificationTransactionService
    {
        private readonly INotificationTransactionServiceAsync _notificationServiceAsync;

        public SendNotificationTransactionService(INotificationTransactionServiceAsync notificationServiceAsync)
        {
            _notificationServiceAsync = notificationServiceAsync;
        }

        public async Task sendNotification(Transaction transaction)
        {
            if (transaction.Client.TypeNotification == 1)
            {
                var subject = "Notificación de Suscripción";
                var body = "Se ha realizado una suscripción al fondo " + transaction.Fund.Name + " por un valor de " + transaction.Fund.MinAmount;
                await _notificationServiceAsync.SendEmailAsync(transaction.Client.Email, subject, body);
            }
            else
            {
                var message = "Se ha realizado una suscripción al fondo " + transaction.Fund.Name + " por un valor de " + transaction.Fund.MinAmount;
                _notificationServiceAsync.SendSms(transaction.Client.Phone, message);
            }
        }
    }
}
