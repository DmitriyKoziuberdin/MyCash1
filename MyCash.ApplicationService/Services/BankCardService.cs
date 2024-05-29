using Common.Exceptions;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;


namespace MyCash.ApplicationService.Services
{
    public class BankCardService : IBankCardService
    {
        private readonly IBankCardRepository _bankCardRepository;
        private readonly IAccountRepository _accountRepository;

        public BankCardService(IBankCardRepository bankCardRepository, IAccountRepository accountRepository)
        {
            _bankCardRepository = bankCardRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<BankCard>> GetAllBankCards()
        {
            return await _bankCardRepository.GetAllBankCards();
        }

        public async Task<CardResponse> GetCardById(int id)
        {
            var isExist = await _bankCardRepository.AnyCardById(id);
            if (!isExist)
            {
                throw new BankCardNotFoundException($"BankCard with this ID: {id} not found.");
            }

            var card = await _bankCardRepository.GetCardById(id);

            var cardResponse = new CardResponse
            {
                CardId = card.CardId,
                CardName = card.CardName,
                Cvv = card.Cvv,
                NumberCard = card.NumberCard,
                BankCardAccount = card.BankCardAccounts.Select(bca => new BankCardAccountResponse
                {
                    AccountId = bca.AccountId,
                    AccountName = bca.Account.AccountName,
                }).ToList(),
                CreatedAt = card.CreatedAt,
                UpdatedAt = card.UpdatedAt
            };

            return cardResponse;
        }

        public async Task CreateCard(CardRequest card)
        {
            var isExist = await _bankCardRepository.AnyCardByNumber(card.NumberCard);
            if (isExist)
            {
                throw new BankCardDuplicateNumberException($"Bank card with this NumberCard: {card.NumberCard} already use.");
            }
            await _bankCardRepository.CreateBankCard(new BankCard
            {
                CardName = card.CardName,
                Cvv = card.Cvv,
                NumberCard = card.NumberCard,
            });
        }

        public async Task<CardResponse> UpdateCard(int cardId, CardRequest card)
        {
            var isExist = await _bankCardRepository.AnyCardById(cardId);
            if (!isExist)
            {
                throw new BankCardNotFoundException($"BankCard with this ID: {cardId} not found.");
            }

            var newCard = new BankCard
            {
                CardId = cardId,
                CardName = card.CardName,
                Cvv = card.Cvv,
                NumberCard = card.NumberCard,
            };

            await _bankCardRepository.UpdateCard(newCard);
            BankCard cardResponse = await _bankCardRepository.GetCardById(newCard.CardId);
            return new CardResponse
            {
                CardId = cardResponse.CardId,
                CardName = cardResponse.CardName,
                Cvv = cardResponse.Cvv,
                NumberCard = cardResponse.NumberCard,
                CreatedAt = cardResponse.CreatedAt,
                UpdatedAt = cardResponse.UpdatedAt
            };
        }

        public async Task DeleteCard(int id)
        {
            var isExist = await _bankCardRepository.AnyCardById(id);
            if (!isExist)
            {
                throw new BankCardNotFoundException($"BankCard with this ID: {id} not found.");
            }
            await _bankCardRepository.DeleteCard(id);
        }

        public async Task AddCardForAccount(int accountId, int cardId)
        {
            var isExistForAccountId = await _accountRepository.AnyAccountById(accountId);
            if (!isExistForAccountId)
            {
                throw new AccountNotFoundException($"Account with this ID: {accountId} not found.");
            }
            var isExistForardId = await _bankCardRepository.AnyCardById(cardId);
            if (!isExistForardId)
            {
                throw new BankCardNotFoundException($"BankCard with this ID: {cardId} not found.");
            }
            await _bankCardRepository.AddCardForAccount(accountId, cardId);
        }
    }
}
