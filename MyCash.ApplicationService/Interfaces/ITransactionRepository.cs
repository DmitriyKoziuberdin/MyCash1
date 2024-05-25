using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int id);
        Task<int> DeleteTransaction(int id);
        Task CreateTransaction(Transaction transaction);
        Task UpdateTransaction(Transaction transaction);
        Task<bool> AnyTransactionById(int id);
    }
}
