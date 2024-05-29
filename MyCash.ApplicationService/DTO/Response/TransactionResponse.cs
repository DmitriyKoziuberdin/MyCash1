namespace MyCash.ApplicationService.DTO.Response
{
    public class TransactionResponse
    {
        public decimal Amount { get; set; }
        public string CategoryTransaction { get; set; } = string.Empty;
        public string CategoryTypeTransaction { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
