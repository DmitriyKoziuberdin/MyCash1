using Microsoft.Extensions.Caching.Memory;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;
using MyCash.Infrastructure.Repositories;
using System.Security.Principal;

namespace MyCash.Infrastructure.CachedRpositories
{
    public class CachedTransactionRepository : ITransactionRepository
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "transaction";

        public CachedTransactionRepository(TransactionRepository transactionRepository, IMemoryCache memoryCache)
        {
            _transactionRepository = transactionRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<TransactionGetAllResponse>> GetAllTransactions()
        {
            var transaction = await _memoryCache
                .GetOrCreateAsync(_cacheKey, (entry) => _transactionRepository.GetAllTransactions());
            return transaction!.ToList();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _transactionRepository.GetTransactionById(id);
        }

        public async Task<bool> AnyTransactionById(int id)
        {
            return await _transactionRepository.AnyTransactionById(id);
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            _memoryCache.Remove(_cacheKey);
            await _transactionRepository.CreateTransaction(transaction);
        }

        public async Task<int> DeleteTransaction(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _transactionRepository.DeleteTransaction(id);
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            _memoryCache.Remove(_cacheKey);
            await _transactionRepository.UpdateTransaction(transaction);
        }

        public async Task<bool> AnyTransactionByCategory(string categoryTransaction)
        {
            return await _transactionRepository.AnyTransactionByCategory(categoryTransaction);
        }
    }
}
