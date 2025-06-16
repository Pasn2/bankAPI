using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class BankAccount
    {
        [Key]
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public double balance { get; set; }
        public List<Transaction> transactions { get; set; } = new List<Transaction>();

    }
}
