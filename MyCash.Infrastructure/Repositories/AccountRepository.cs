using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Task<bool> AnyAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Account>> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
