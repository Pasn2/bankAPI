using BankApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BankApi.DTOS
{
    public class TransactionDTO
    {
        [Key]
        public int ID { get; set; }     
        public int SenderAccountId { get; set; }
        public int ReceiverAccountId { get; set; }
        public decimal Amount { get; set; }
        

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
