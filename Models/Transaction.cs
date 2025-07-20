using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Transaction
    {
        [Key]
        public int ID { get; set; }


        public decimal Amount { get; set; }
        public string Descryption { get; set; }

        public int SenderAccountId { get; set; }
        public BankAccount SenderAccount { get; set; }

        public int ReceiverAccountId { get; set; }
        public BankAccount ReceiverAccount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
