using Microsoft.Extensions.Caching.Memory;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;
using MyCash.Infrastructure.Repositories;

namespace MyCash.Infrastructure.CachedRpositories
{
    public class CachedBankCardRepository : IBankCardRepository
    {
        private readonly BankCardRepository _bankCardRepository;
        private readonly IMemoryCache _memoryCache;
        private static string _cacheKey = "bankcard";

        public CachedBankCardRepository(BankCardRepository bankCardRepository, IMemoryCache memoryCache)
        {
            _bankCardRepository = bankCardRepository;
            _memoryCache = memoryCache;
        }

        public async Task<List<BankCard>> GetAllBankCards()
        {
            var cards = await _memoryCache
                .GetOrCreateAsync(_cacheKey, (entry) => _bankCardRepository.GetAllBankCards());
            return cards!.ToList();
        }

        public async Task<BankCard> GetCardById(int id)
        {
            return await _bankCardRepository.GetCardById(id);
        }

        public async Task CreateBankCard(BankCard card)
        {
            _memoryCache.Remove(_cacheKey);
            await _bankCardRepository.CreateBankCard(card);
        }

        public async Task UpdateCard(BankCard card)
        {
            _memoryCache.Remove(_cacheKey);
            await _bankCardRepository.UpdateCard(card);
        }

        public async Task<int> DeleteCard(int id)
        {
            _memoryCache.Remove(_cacheKey);
            return await _bankCardRepository.DeleteCard(id);
        }

        public async Task AddCardForAccount(int accountId, int cardId)
        {
            _memoryCache.Remove(_cacheKey);
            await _bankCardRepository.AddCardForAccount(accountId, cardId);
        }

        public async Task<bool> AnyCardById(int id)
        {
            return await _bankCardRepository.AnyCardById(id);
        }

        public async Task<bool> AnyCardByNumber(string cardNumber)
        {
            return await _bankCardRepository.AnyCardByNumber(cardNumber);
        }   
    }
}
