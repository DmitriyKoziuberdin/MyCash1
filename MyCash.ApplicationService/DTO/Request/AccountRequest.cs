namespace MyCash.ApplicationService.DTO.Request
{
    public class AccountRequest
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
