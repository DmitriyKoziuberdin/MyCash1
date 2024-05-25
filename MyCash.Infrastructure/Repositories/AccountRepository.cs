using Microsoft.EntityFrameworkCore;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain;
using MyCash.Domain.Entity;

namespace MyCash.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;

        public AccountRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _appDbContext.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountById(int id)
        {
            var getAccount = await _appDbContext.Accounts
                .Include(accountTransaction => accountTransaction.AccountTransactions)
                .ThenInclude(transaction => transaction.Transaction)
                .FirstAsync(accountId => accountId.AccountId == id);
            return getAccount;
        }

        public async Task CreateAccount(Account account)
        {
            await _appDbContext.Accounts.AddAsync(account);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAccount(Account account)
        {
            _appDbContext.Accounts.Update(account);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAccount(int id)
        {
            var deleteAccount = await _appDbContext.Accounts
                .Where(accountId => accountId.AccountId == id)
                .ExecuteDeleteAsync();
            await _appDbContext.SaveChangesAsync();
            return deleteAccount;
        }

        public async Task<bool> AnyAccountById(int id)
        {
            return await _appDbContext.Accounts.AnyAsync(accountId => accountId.AccountId == id);
        }
    }
}
