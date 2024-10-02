using AutoMapper;
using BTG.Application.DTOs;
using BTG.Application.Interfaces;
using BTG.Application.Wrappers;
using BTG.Domain.Entities;
using MediatR;

namespace BTG.Application.Features.Transactions.Queries.GetAllFunds
{
    public class GetAllFundsQuery : IRequest<Response<IEnumerable<FundDto>>>
    {
        public class GetAllFundsQueryHandler : IRequestHandler<GetAllFundsQuery, Response<IEnumerable<FundDto>>>
        {
            private readonly IFundRepositoryAsync<Fund> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetAllFundsQueryHandler(IFundRepositoryAsync<Fund> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<IEnumerable<FundDto>>> Handle(GetAllFundsQuery request, CancellationToken cancellationToken)
            {
                var funds = await _repositoryAsync.GetAllFundsAsync();
                if (funds.Count() == 0)
                {
                    throw new KeyNotFoundException($"No hay fondos creados.");
                }
                else
                {
                    var fundsDto = _mapper.Map<IEnumerable<FundDto>>(funds);
                    return new Response<IEnumerable<FundDto>>(fundsDto);
                }
            }
        }
        
    }
}
