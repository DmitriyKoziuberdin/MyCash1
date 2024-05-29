namespace MyCash.ApplicationService.DTO.Response
{
    public class AccountBankCardResponse
    {
        public int CardId { get; set; }
        public string? CardName { get; set; }
        public string? Cvv { get; set; }
        public string? NumberCard { get; set; }
    }
}
