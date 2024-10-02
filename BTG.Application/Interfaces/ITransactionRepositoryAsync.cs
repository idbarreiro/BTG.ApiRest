using BTG.Domain.Entities;

namespace BTG.Application.Interfaces
{
    public interface ITransactionRepositoryAsync<T> where T : class
    {
        Task<Transaction> GetLastTransactionAsync();
        Task<Transaction> GetLastTransactionByFundIdAsync(int fundId);
        Task<IEnumerable<Transaction>> GetLatestTransactionsAsync();
        Task InsertTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(string id, Transaction transaction);
    }
}
