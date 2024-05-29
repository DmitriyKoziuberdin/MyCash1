using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface IBankCardService
    {
        public Task<List<BankCard>> GetAllBankCards();
        public Task<CardResponse> GetCardById(int id);
        public Task CreateCard(CardRequest card);
        public Task<CardResponse> UpdateCard(int cardId, CardRequest card);
        public Task DeleteCard(int id);
        public Task AddCardForAccount(int accountId, int cardId);

    }
}
