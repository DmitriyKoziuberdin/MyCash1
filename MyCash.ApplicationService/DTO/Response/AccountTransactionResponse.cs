using MyCash.Domain.Entity;

namespace MyCash.ApplicationService.DTO.Response
{
    public class AccountTransactionResponse
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string CategoryTransaction { get; set; } = string.Empty;
        public string CategoryTypeTransaction { get; set; } = string.Empty;


    }
}
