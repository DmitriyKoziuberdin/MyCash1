using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task<bool> AnyAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteTransaction(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>> GetAllTransactions()
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetTransactionById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
