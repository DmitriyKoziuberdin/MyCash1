using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<Account>> GetAllAccount()
        {
            return await _accountRepository.GetAllAccounts();
        }

        public async Task<AccountResponse> GetAccountById(int id)
        {
            var isExist = await _accountRepository.AnyAccountById(id);
            if (!isExist)
            {
                throw new InvalidOperationException("Not found id");
            }

            var accountId = await _accountRepository.GetAccountById(id);
            var accountResponse = new AccountResponse 
            {
                AccountName = accountId.AccountName,
                Balance = accountId.Balance,
                AccountTransactions = accountId.AccountTransactions.Select(at => new AccountTransactionResponse
                {
                    Id = at.TransactionId,
                    Amount = at.Transaction.Amount,
                    CategoryTransaction = at.Transaction.CategoryTransaction,
                    CategoryTypeTransaction = at.Transaction.CategoryTypeTransaction
                }).ToList(),
            };
            return accountResponse;
        }

        public async Task CreateAccount(AccountRequest account)
        {
            await _accountRepository.CreateAccount(new Account
            {
                AccountName = account.AccountName,
                Balance = account.Balance
            });
        }

        public async Task<AccountResponse> UpdateAccount(int accountId, AccountRequest account)
        {
            var isExist = await _accountRepository.AnyAccountById(accountId);
            if (!isExist)
            {
                throw new InvalidOperationException("Not found id");
            }

            var newAccount = new Account
            {
                AccountId = accountId,
                AccountName = account.AccountName,
                Balance = account.Balance
            };

            await _accountRepository.UpdateAccount(newAccount);
            Account accountResponse = await _accountRepository.GetAccountById(newAccount.AccountId);
            return new AccountResponse
            {
                AccountName = accountResponse.AccountName,
                Balance = accountResponse.Balance
            };
        }

        public async Task DeleteAccount(int id)
        {
            var isExist = await _accountRepository.AnyAccountById(id);
            if (!isExist)
            {
                throw new InvalidOperationException("Not found id");
            }
            await _accountRepository.DeleteAccount(id);
        }
    }
}
