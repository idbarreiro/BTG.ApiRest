using BTG.Application.Interfaces;
using BTG.Domain.Entities;
using BTG.Persistence.Context;
using MongoDB.Driver;

namespace BTG.Persistence.Repository
{
    public class FundRepositoryAsync<T> : IFundRepositoryAsync<T> where T : class
    {
        private readonly IMongoCollection<Fund> _collectionFund;

        public FundRepositoryAsync(ApplicationDbContext context)
        {
            _collectionFund = context.GetCollectionFund<Fund>("Funds");
        }

        public async Task<IEnumerable<Fund>> GetAllFundsAsync()
        {
            return await _collectionFund.Find(fund => true).ToListAsync();
        }
    }
}
