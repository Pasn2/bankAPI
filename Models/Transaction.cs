using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class Transaction
    {
        [Key]
        public int tansactionId { get; set; }
        public int AccountId { get; set; }
        
        public int targetAccountId { get; set; }
        public int transactionValue { get; set; }
    }
}
