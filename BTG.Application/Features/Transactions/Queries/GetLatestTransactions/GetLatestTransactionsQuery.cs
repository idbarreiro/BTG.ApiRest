using AutoMapper;
using BTG.Application.DTOs;
using BTG.Application.Interfaces;
using BTG.Application.Wrappers;
using BTG.Domain.Entities;
using MediatR;

namespace BTG.Application.Features.Transactions.Queries.GetLatestTransactions
{
    public  class GetLatestTransactionsQuery : IRequest<Response<IEnumerable<TransactionDto>>>
    {
        public class GetLatestTransactionsQueryHandler : IRequestHandler<GetLatestTransactionsQuery, Response<IEnumerable<TransactionDto>>>
        {
            private readonly IRepositoryAsync<Transaction> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetLatestTransactionsQueryHandler(IRepositoryAsync<Transaction> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<IEnumerable<TransactionDto>>> Handle(GetLatestTransactionsQuery request, CancellationToken cancellationToken)
            {
                var transactions = await _repositoryAsync.GetLatestAsync();
                if (transactions.Count() == 0)
                {
                    throw new KeyNotFoundException($"No hay transacciones realizadas.");
                }
                else
                {
                    var transactionDto = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
                    return new Response<IEnumerable<TransactionDto>>(transactionDto);
                }
            }
        }
    }
}
