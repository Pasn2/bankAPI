using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class BankAccount
    {
        [Key]
        public int AccountId { get; set; }
        public decimal balance { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public List<Transaction> SentTransactions { get; set; } = new();
        public List<Transaction> ReceivedTransactions { get; set; } = new();

    }
}
