using BTG.Application.Interfaces;
using BTG.Domain.Entities;
using BTG.Persistence.Context;
using MongoDB.Driver;

namespace BTG.Persistence.Repository
{
    public class MyRepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        private readonly IMongoCollection<Transaction> _collection;

        public MyRepositoryAsync(ApplicationDbContext context)
        {
            _collection = context.GetCollection<Transaction>("Transactions");
        }

        public async Task<Transaction> GetLastAsync()
        {
            return await _collection.Find(transaction => true).SortByDescending(transaction => transaction.Id).FirstOrDefaultAsync();
        }

        public async Task<Transaction> GetLastByFundIdAsync(int fundId)
        {
            return await _collection.Find(transaction => transaction.FundId == fundId).SortByDescending(transaction => transaction.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetLatestAsync()
        {
            return await _collection.Find(transaction => true).ToListAsync();
            //return await _collection.Find(transaction => true).SortByDescending(transaction => transaction.Id).Limit(10).ToListAsync();
        }

        public async Task InsertAsync(Transaction transaction)
        {
            await _collection.InsertOneAsync(transaction);
        }

        public async Task UpdateAsync(string id, Transaction transaction)
        {
            await _collection.ReplaceOneAsync(Builders<Transaction>.Filter.Eq("Id",id), transaction);
        }
    }
}
