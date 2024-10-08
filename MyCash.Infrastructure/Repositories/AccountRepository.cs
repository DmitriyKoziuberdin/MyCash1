﻿using Microsoft.EntityFrameworkCore;
using MyCash.ApplicationService.DTO.Response;
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

        public async Task<List<AccountGetAllResponse>> GetAllAccounts()
        {
            return await _appDbContext.Accounts
                 .Select(account => new AccountGetAllResponse
                 {
                     AccountId = account.AccountId,
                     AccountName = account.AccountName,
                     CreatedAt = account.CreatedAt,
                     UpdatedAt = account.UpdatedAt
                     //Balance = account.Balance
                 })
                 .ToListAsync();
        }

        public async Task<Account> GetAccountById(int id)
        {
            var getAccount = await _appDbContext.Accounts
                .Include(accountTransaction => accountTransaction.AccountTransactions)
                .ThenInclude(transaction => transaction.Transaction)
                .Include(bankCardAccount =>  bankCardAccount.BankCardAccounts)
                .ThenInclude(bankCard => bankCard.BankCard)
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
            return await _appDbContext.Accounts
                .AnyAsync(accountId => accountId.AccountId == id);
        }

        public async Task AddTransaction(int accountId, int transactionId)
        {
            _appDbContext.Set<AccountTransaction>().Add(new AccountTransaction
            {
                AccountId = accountId,
                TransactionId = transactionId
            });
            await _appDbContext.SaveChangesAsync();
        }

        


        public async Task<decimal> CalculateAmountTransactionForAccount(int accountId)
        {
            var account = await _appDbContext.Accounts
                .Include(accountTransaction => accountTransaction.AccountTransactions)
                .ThenInclude(transaction => transaction.Transaction)
                .FirstAsync(id => id.AccountId == accountId);

            decimal totalBudgetOfAccount = account.AccountTransactions.Sum(accountTransaction => accountTransaction.Transaction.Amount);
            return totalBudgetOfAccount;
        }
    }
}
