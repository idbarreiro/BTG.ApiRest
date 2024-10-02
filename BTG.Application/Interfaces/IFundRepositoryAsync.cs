using BTG.Domain.Entities;

namespace BTG.Application.Interfaces
{
    public interface IFundRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<Fund>> GetAllFundsAsync();
    }
}
