namespace BankingApp.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string TransactionType { get; set; } // "Deposit" or "Withdraw"
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
