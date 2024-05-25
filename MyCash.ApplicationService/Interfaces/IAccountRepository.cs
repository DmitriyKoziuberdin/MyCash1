using MyCash.ApplicationService.DTO.Response;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountGetAllResponse>> GetAllAccounts();
        Task<Account> GetAccountById(int id);
        Task<int> DeleteAccount(int id);
        Task CreateAccount(Account account);
        Task UpdateAccount(Account account);
        Task<bool> AnyAccountById(int id);
        Task AddTransaction(int accountId, int transactionId);
        Task<decimal> CalculateAmountTransactionForAccount(int accountId);
    }
}
