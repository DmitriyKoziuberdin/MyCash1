namespace MyCash.ApplicationService.DTO.Response
{
    public class TransactionGetAllResponse
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CategoryTransaction { get; set; } = string.Empty;
        public string CategoryTypeTransaction { get; set; } = string.Empty;
    }
}
