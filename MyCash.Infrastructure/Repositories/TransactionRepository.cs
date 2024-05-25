using Microsoft.EntityFrameworkCore;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain;
using MyCash.Domain.Entity;

namespace MyCash.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;

        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<TransactionGetAllResponse>> GetAllTransactions()
        {
            return await _appDbContext.Transactions
                 .Select(transaction => new TransactionGetAllResponse
                 {
                     TransactionId = transaction.TransactionId,
                     CategoryTransaction = transaction.CategoryTransaction,
                     CategoryTypeTransaction = transaction.CategoryTypeTransaction,
                     Amount = transaction.Amount
                 })
                 .ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            var transactionById = await _appDbContext.Transactions
                .FirstOrDefaultAsync(transactionId => transactionId.TransactionId == id);
            return transactionById;
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            await _appDbContext.Transactions.AddAsync(transaction);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            _appDbContext.Transactions.Update(transaction);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteTransaction(int id)
        {
            var deletTransaction = await _appDbContext.Transactions
                .Where(transactionId => transactionId.TransactionId == id)
                .ExecuteDeleteAsync();
            return deletTransaction;
        }

        public async Task<bool> AnyTransactionById(int id)
        {
            return await _appDbContext.Transactions
                .AnyAsync(transactionId => transactionId.TransactionId == id);
        }
    }
}
