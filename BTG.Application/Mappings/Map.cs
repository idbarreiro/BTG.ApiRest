using AutoMapper;
using BTG.Application.DTOs;
using BTG.Application.Features.Transactions.Commands.CreateTransactionCommand;
using BTG.Domain.Entities;

namespace BTG.Application.Mappings
{
    public class Map : Profile
    {
        public Map()
        {
            #region DTOs
            CreateMap<Transaction, TransactionDto>();
            #endregion

            #region Commands
            CreateMap<CreateTransactionCommand, Transaction>();
            #endregion
        }
    }
}
