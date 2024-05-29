namespace MyCash.ApplicationService.DTO.Response
{
    public class CardResponse
    {
        public int CardId { get; set; }
        public string? CardName { get; set; }
        public string? Cvv { get; set; }
        public string? NumberCard { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<BankCardAccountResponse>? BankCardAccount { get; set; }
    }
}
