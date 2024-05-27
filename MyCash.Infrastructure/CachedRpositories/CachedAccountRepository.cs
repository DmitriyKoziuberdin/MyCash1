using Microsoft.Extensions.Caching.Memory;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;
using MyCash.Infrastructure.Repositories;

namespace MyCash.Infrastructure.CachedRpositories
{
    public class CachedAccountRepository : IAccountRepository
    {
        private readonly AccountRepository _accountRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "account";

        public CachedAccountRepository(AccountRepository accountRepository, IMemoryCache memoryCache)
        {
            _accountRepository = accountRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<AccountGetAllResponse>> GetAllAccounts()
        {
            var accounts = await _memoryCache
                .GetOrCreateAsync(_cacheKey, (entry) => _accountRepository.GetAllAccounts());
            return accounts!.ToList();
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _accountRepository.GetAccountById(id);
        }

        public async Task CreateAccount(Account account)
        {
            _memoryCache.Remove(_cacheKey);
            await _accountRepository.CreateAccount(account);
        }

        public async Task UpdateAccount(Account account)
        {
            _memoryCache.Remove(_cacheKey);
            await _accountRepository.UpdateAccount(account);
        }

        public async Task<int> DeleteAccount(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _accountRepository.DeleteAccount(id);
        }

        public Task<decimal> CalculateAmountTransactionForAccount(int accountId)
        {
            return _accountRepository.CalculateAmountTransactionForAccount(accountId);
        }

        public async Task<bool> AnyAccountById(int id)
        {
            return await _accountRepository.AnyAccountById(id);
        }

        public async Task AddTransaction(int accountId, int transactionId)
        {
            _memoryCache.Remove(_cacheKey);
            await _accountRepository.AddTransaction(accountId, transactionId);
        }
    }
}
