namespace MyCash.Domain.Entity
{
    public class BankCard : BaseEntity
    {
        public int CardId { get; set; }
        public string? CardName { get; set; }
        public string? Cvv {  get; set; }
        public string? NumberCard { get; set; }

        public List<BankCardAccount>? BankCardAccounts { get; set; }
    }
}
