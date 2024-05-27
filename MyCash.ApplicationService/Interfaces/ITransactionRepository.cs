using MyCash.ApplicationService.DTO.Response;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionGetAllResponse>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int id);
        Task<int> DeleteTransaction(int id);
        Task CreateTransaction(Transaction transaction);
        Task UpdateTransaction(Transaction transaction);
        Task<bool> AnyTransactionById(int id);
        Task<bool> AnyTransactionByCategory(string categoryTransaction);
    }
}
