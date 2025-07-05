using BankApi.DTOS;

namespace BankApi.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDTO toTransactionDTO(this TransactionDTO transactionModel)
        {
            return new TransactionDTO
            {
                ID = transactionModel.ID,
                Amount = transactionModel.Amount,
                CreatedAt = transactionModel.CreatedAt,
                SendAccountId = transactionModel.SendAccountId,
                ReciveAccountId = transactionModel.ReciveAccountId

            };
        }
    }
}
