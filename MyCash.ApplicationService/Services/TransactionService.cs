using Common.Exceptions;
using MyCash.ApplicationService.DTO.Request;
using MyCash.ApplicationService.DTO.Response;
using MyCash.ApplicationService.Interfaces;
using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionGetAllResponse>> GetAllTransactions()
        {
            return await _transactionRepository.GetAllTransactions();
        }

        public async Task<TransactionResponse> GetTransactionById(int id)
        {
            var isExist = await _transactionRepository.AnyTransactionById(id);
            if (!isExist)
            {
                throw new TransactionNotFoundException($"Transaction with this ID: {id} not found.");
            }

            var transactionId = await _transactionRepository.GetTransactionById(id);
            var transactionResponse = new TransactionResponse
            {
                Amount = transactionId.Amount,
                CategoryTransaction = transactionId.CategoryTransaction,
                CategoryTypeTransaction = transactionId.CategoryTypeTransaction
            };
            return transactionResponse;
        }

        public async Task CreateTransaction(TransactionRequest transaction)
        {
            var isExist = await _transactionRepository.AnyTransactionByCategory(transaction.CategoryTransaction);
            if (isExist)
            {
                throw new TransactionDuplicateCategoryNameException($"Transaction with this CategoryName: {transaction.CategoryTransaction} already use.");
            }

            await _transactionRepository.CreateTransaction(new Transaction
            {
                Amount = transaction.Amount,
                CategoryTransaction = transaction.CategoryTransaction,
                CategoryTypeTransaction = transaction.CategoryTypeTransaction
            });
        }

        public async Task<TransactionResponse> UpdateTransaction(int transactionId, TransactionRequest transaction)
        {
            var isExist = await _transactionRepository.AnyTransactionById(transactionId);
            if (!isExist)
            {
                throw new TransactionNotFoundException($"Transaction with this ID: {transactionId} not found.");
            }

            var newTransaction = new Transaction
            {
                TransactionId = transactionId,
                Amount = transaction.Amount,
                CategoryTransaction = transaction.CategoryTransaction,
                CategoryTypeTransaction = transaction.CategoryTransaction
            };

            await _transactionRepository.UpdateTransaction(newTransaction);
            Transaction transactionResponse = await _transactionRepository.GetTransactionById(newTransaction.TransactionId);
            return new TransactionResponse
            {
                Amount = transactionResponse.Amount,
                CategoryTransaction = transactionResponse.CategoryTransaction,
                CategoryTypeTransaction = transactionResponse.CategoryTypeTransaction
            };
        }

        public async Task DeleteTransaction(int id)
        {
            var isExist = await _transactionRepository.AnyTransactionById(id);
            if (!isExist)
            {
                throw new TransactionNotFoundException($"Transaction with this ID: {id} not found.");
            }

            await _transactionRepository.DeleteTransaction(id);
        }    
    }
}
