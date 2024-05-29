using Microsoft.EntityFrameworkCore;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain;
using MyCash.Domain.Entity;

namespace MyCash.Infrastructure.Repositories
{
    public class BankCardRepository : IBankCardRepository
    {
        protected readonly AppDbContext _appDbContext;

        public BankCardRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<BankCard>> GetAllBankCards()
        {
            return await _appDbContext.BankCards.ToListAsync();
        }

        public async Task<BankCard> GetCardById(int id)
        {
            var getCardId = await _appDbContext.BankCards
                .Include(bankCardAccount => bankCardAccount.BankCardAccounts)
                .ThenInclude(account => account.Account)
                .FirstOrDefaultAsync(cardId => cardId.CardId == id);
            return getCardId;
        }

        public async Task CreateBankCard(BankCard card)
        {
             _appDbContext.BankCards.Add(card);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCard(int id)
        {
            var deleteCard = await _appDbContext.BankCards
                .Where(cardId => cardId.CardId == id)
                .ExecuteDeleteAsync();
            await _appDbContext.SaveChangesAsync();
            return deleteCard;
        }

        public async Task UpdateCard(BankCard card)
        {
            _appDbContext.BankCards.Update(card);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> AnyCardById(int id)
        {
            return await _appDbContext.BankCards.AnyAsync(cardId => cardId.CardId == id);
        }

        public async Task<bool> AnyCardByNumber(string cardNumber)
        {
            return await _appDbContext.BankCards.AnyAsync(number => number.NumberCard == cardNumber);
        }

        public async Task AddCardForAccount(int accountId, int cardId)
        {
            _appDbContext.Set<BankCardAccount>().Add(new BankCardAccount
            {
                AccountId = accountId,
                CardId = cardId
            });
            await _appDbContext.SaveChangesAsync();
        }
    }
}
