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
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public Fund Fund { get; set; }
        public Client Client { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Response<string>>
    {
        private readonly ITransactionRepositoryAsync<Transaction> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateTransactionCommandHandler(ITransactionRepositoryAsync<Transaction> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = _mapper.Map<Transaction>(request);
            var newTransaction = await HandleTransaction(transaction);
            await _repositoryAsync.InsertTransactionAsync(newTransaction);            
            return new Response<string>(newTransaction.Id.ToString());
        }

        private async Task<Transaction> HandleTransaction(Transaction transaction)
        {
            var lastTransactionByFundId = await _repositoryAsync.GetLastTransactionByFundIdAsync(transaction.Fund.FundId);

            if (lastTransactionByFundId == null)
            {
                if (transaction.Type == 1)
                {
                    await HandleFirstTransactionAsync(transaction);
                }
                else
                {
                    HandleSubsequentTransaction(lastTransactionByFundId, transaction);
                }
            }
            else
            {
                HandleSubsequentTransaction(transaction, lastTransactionByFundId);
            }

            transaction.Date = DateTime.Now;
            return transaction;
        }

        private async Task HandleFirstTransactionAsync(Transaction transaction)
        {
            var lastTransaction = await _repositoryAsync.GetLastTransactionAsync();

            if (lastTransaction == null)
            {
                transaction.Client.Estimate = 500000 - transaction.Fund.MinAmount;
            }
            else
            {
                ValidateAmount(transaction.Fund.MinAmount, lastTransaction.Client.Estimate, transaction.Fund.Name);
                transaction.Client.Estimate = lastTransaction.Client.Estimate - transaction.Fund.MinAmount;
            }
        }

        private void HandleSubsequentTransaction(Transaction transaction, Transaction lastTransactionByFundId)
        {
            if (transaction.Type == 1)
            {
                if (lastTransactionByFundId.Type == 1)
                {
                    throw new ApiException("Ya tiene una suscripción activa en el fondo " + transaction.Fund.Name);
                }

                ValidateAmount(transaction.Fund.MinAmount, lastTransactionByFundId.Client.Estimate, transaction.Fund.Name);
                transaction.Client.Estimate = lastTransactionByFundId.Client.Estimate - transaction.Fund.MinAmount;
            }
            else if (transaction.Type == 2)
            {
                if (lastTransactionByFundId == null || lastTransactionByFundId.Type == 2 )
                {
                    throw new ApiException("No tiene una suscripción activa en el fondo " + transaction.Fund.Name);
                }

                transaction.Client.Estimate = lastTransactionByFundId.Client.Estimate + transaction.Fund.MinAmount;
            }
            else
            {
                throw new ApiException("Tipo de transacción no reconocido");
            }
        }

        private void ValidateAmount(decimal fundMinAmount, decimal clientEstimate, string fundName)
        {
            if (fundMinAmount > clientEstimate)
            {
                throw new ApiException("No tiene saldo disponible para vincularse al fondo " + fundName);
            }
        }
    }
}
