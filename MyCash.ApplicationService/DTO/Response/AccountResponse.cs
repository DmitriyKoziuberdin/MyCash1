using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.DTO.Response
{
    public class AccountResponse
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public List<AccountTransactionResponse>? AccountTransactions { get; set; }
    }
}
