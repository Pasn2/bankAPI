namespace BankApi.DTOS
{
    public class RegisterDTO
    {
        public int ID { get; set; }
        public string email { get; set; } = String.Empty;
        public string login { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
    }
}
