namespace MyCash.ApplicationService.DTO.Response
{
    public class AccountResponse
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<AccountTransactionResponse>? AccountTransactions { get; set; }
        public List<AccountBankCardResponse>? AccountBankCard { get; set; }
    }
}
