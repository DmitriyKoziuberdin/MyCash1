using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccountById(int id);
        Task<int> DeleteAccount(int id);
        Task CreateAccount(Account account);
        Task UpdateAccount(Account account);
        Task<bool> AnyAccountById(int id);
    }
}
