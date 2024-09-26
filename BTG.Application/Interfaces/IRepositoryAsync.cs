using BTG.Domain.Entities;

namespace BTG.Application.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        Task<Transaction> GetLastAsync();
        Task<Transaction> GetLastByFundIdAsync(int fundId);
        Task<IEnumerable<Transaction>> GetLatestAsync();
        Task InsertAsync(Transaction transaction);
        Task UpdateAsync(string id, Transaction transaction);
    }
}
