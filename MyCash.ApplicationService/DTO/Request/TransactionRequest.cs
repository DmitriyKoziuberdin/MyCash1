namespace MyCash.ApplicationService.DTO.Request
{
    public class TransactionRequest
    {
        public decimal Amount { get; set; }
        public string CategoryTransaction { get; set; } = string.Empty;
        public string CategoryTypeTransaction { get; set; } = string.Empty;
    }
}
