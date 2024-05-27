namespace MyCash.ApplicationService.DTO.Response
{
    public class AccountGetAllResponse
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        //public decimal Balance { get; set; }
    }
}
