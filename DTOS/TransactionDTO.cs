using System.ComponentModel.DataAnnotations;

namespace BankApi.DTOS
{
    public class TransactionDTO
    {
        [Key]
        public int tansactionId { get; set; }
        public int AccountId { get; set; }

        public int targetAccountId { get; set; }
        public string accountLogin { get; set; }
        public int transactionValue { get; set; }
    }
}
