using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Interfaces
{
    public interface ITransactionService
    {
        public Task<List<Transaction>> GetAllTransactions();
        public Task<TransactionResponse> GetTransactionById(int id);
        public Task CreateTransaction(TransactionRequest transaction);
        public Task<TransactionResponse> UpdateTransaction(int transactionId, TransactionRequest transaction);
        public Task DeleteTransaction(int id);

        //public Task<OrderCountDto> GetOrderSummary(string orderName);
    }
}
