namespace MyCash.ApplicationService.DTO.Response
{
    public class UserAccountResponse
    {
        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
