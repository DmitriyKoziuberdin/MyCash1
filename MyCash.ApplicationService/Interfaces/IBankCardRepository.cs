using MyCash.Domain;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface IBankCardRepository
    {
        Task<List<BankCard>> GetAllBankCards();
        Task<BankCard> GetCardById(int id);
        Task<int> DeleteCard(int id);
        Task CreateBankCard(BankCard card);
        Task UpdateCard(BankCard card);
        Task<bool> AnyCardById(int id);
        Task<bool> AnyCardByNumber(string cardNumber);
        Task AddCardForAccount(int accountId, int cardId);

        //Task AddTransaction(int accountId, int transactionId);
    }
}
