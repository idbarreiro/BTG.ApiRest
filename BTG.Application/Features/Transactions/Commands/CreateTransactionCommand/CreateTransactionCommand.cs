using AutoMapper;
using BTG.Application.Exceptions;
using BTG.Application.Interfaces;
using BTG.Application.Services;
using BTG.Application.Wrappers;
using BTG.Domain.Entities;
using MediatR;

namespace BTG.Application.Features.Transactions.Commands.CreateTransactionCommand
{
    public class CreateTransactionCommand : IRequest<Response<string>>
    {
        public int FundId { get; set; }
        public string FundName { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<string>>
    {
        private readonly IRepositoryAsync<Transaction> _repositoryAsync;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;

        public CreateTransactionCommandHandler(IRepositoryAsync<Transaction> repositoryAsync, IMapper mapper, EmailService emailService, SmsService smsService)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task<Response<string>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<Transaction>(request);
            var newTransaction = await HandleTransaction(transaction);
            await _repositoryAsync.InsertAsync(newTransaction);
            await sendNotification(newTransaction);
            return new Response<string>(newTransaction.Id.ToString());
        }

        private async Task<Transaction> HandleTransaction(Transaction transaction)
        {
            var lastTransactionByFund = await _repositoryAsync.GetLastByFundIdAsync(transaction.FundId);

            if (lastTransactionByFund == null)
            {
                if (transaction.Type == "suscripcion")
                {
                    await HandleFirstTransactionAsync(transaction);
                }
                else
                {
                    HandleSubsequentTransaction(lastTransactionByFund, transaction);
                }
            }
            else
            {
                HandleSubsequentTransaction(transaction, lastTransactionByFund);
            }

            transaction.Date = DateTime.Now;
            return transaction;
        }

        private async Task HandleFirstTransactionAsync(Transaction transaction)
        {
            var lastTransaction = await _repositoryAsync.GetLastAsync();

            if (lastTransaction == null)
            {
                transaction.Client.Estimate = 500000 - transaction.Amount;
            }
            else
            {
                ValidateAmount(transaction.Amount, lastTransaction.Client.Estimate, transaction.FundName);
                transaction.Client.Estimate = lastTransaction.Client.Estimate - transaction.Amount;
            }
        }

        private void HandleSubsequentTransaction(Transaction transaction, Transaction lastTransactionByFund)
        {
            if (transaction.Type == "suscripcion")
            {
                if (lastTransactionByFund.Type == "suscripcion")
                {
                    throw new ApiException("Ya tiene una suscripción activa en el fondo " + transaction.FundName);
                }

                ValidateAmount(transaction.Amount, lastTransactionByFund.Client.Estimate, transaction.FundName);
                transaction.Client.Estimate = lastTransactionByFund.Client.Estimate - transaction.Amount;
            }
            else if (transaction.Type == "cancelacion")
            {
                if (lastTransactionByFund == null || lastTransactionByFund.Type == "cancelacion" )
                {
                    throw new ApiException("No tiene una suscripción activa en el fondo " + transaction.FundName);
                }

                transaction.Client.Estimate = lastTransactionByFund.Client.Estimate + transaction.Amount;
            }
            else
            {
                throw new ApiException("Tipo de transacción no reconocido");
            }
        }

        private void ValidateAmount(decimal transactionAmount, decimal clientEstimate, string fundName)
        {
            if (transactionAmount > clientEstimate)
            {
                throw new ApiException("No tiene saldo disponible para vincularse al fondo " + fundName);
            }
        }

        private async Task sendNotification(Transaction transaction)
        {
            if (transaction.Client.TypeNotification == "email")
            {
                var subject = "Notificación de Suscripción";
                var body = "Se ha realizado una suscripción al fondo " + transaction.FundName + " por un valor de " + transaction.Amount;
                await _emailService.SendEmailAsync(transaction.Client.Email, subject, body);
            }
            else
            {
                var message = "Se ha realizado una suscripción al fondo " + transaction.FundName + " por un valor de " + transaction.Amount;
                _smsService.SendSms(transaction.Client.Phone, message);
            }
        }
    }
}
