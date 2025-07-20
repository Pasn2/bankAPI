using BankApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BankApi.DTOS
{
    public class TransactionDTO
    {
        [Key]
        public int ID { get; set; }     
        public int ReciveAccountId { get; set; }
        public int SendAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Descryption {  get; set; }
        

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
