﻿using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;


namespace MyCash.ApplicationService.Interfaces
{
    public interface IAccountService
    {
        public Task<List<AccountGetAllResponse>> GetAllAccount();
        public Task<AccountResponse> GetAccountById(int id);
        public Task CreateAccount(AccountRequest account);
        public Task<AccountResponse> UpdateAccount(int accountId, AccountRequest account);
        public Task DeleteAccount(int id);
        public Task AddTransaction(int accountId, int transactionId);
    }
}
