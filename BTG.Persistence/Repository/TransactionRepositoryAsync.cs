using BTG.Application.Interfaces;
using BTG.Domain.Entities;
using BTG.Persistence.Context;
using MongoDB.Driver;

namespace BTG.Persistence.Repository
{
    public class TransactionRepositoryAsync<T> : ITransactionRepositoryAsync<T> where T : class
    {
        private readonly IMongoCollection<Transaction> _collectionTransaction;

        public TransactionRepositoryAsync(ApplicationDbContext context)
        {
            _collectionTransaction = context.GetCollectionTransaction<Transaction>("Transactions");
        }

        public async Task<Transaction> GetLastTransactionAsync()
        {
            return await _collectionTransaction.Find(transaction => true).SortByDescending(transaction => transaction.Id).FirstOrDefaultAsync();
        }

        public async Task<Transaction> GetLastTransactionByFundIdAsync(int fundId)
        {
            return await _collectionTransaction.Find(transaction => transaction.Fund.FundId == fundId).SortByDescending(transaction => transaction.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetLatestTransactionsAsync()
        {
            //return await _collectionTransaction.Find(transaction => true).ToListAsync();
            return await _collectionTransaction.Find(transaction => true).SortByDescending(transaction => transaction.Id).Limit(10).ToListAsync();
        }

        public async Task InsertTransactionAsync(Transaction transaction)
        {
            await _collectionTransaction.InsertOneAsync(transaction);
        }

        public async Task UpdateTransactionAsync(string id, Transaction transaction)
        {
            await _collectionTransaction.ReplaceOneAsync(Builders<Transaction>.Filter.Eq("Id",id), transaction);
        }
    }
}
