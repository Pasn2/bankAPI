namespace BankApi.DTOS
{
    public class LoginDTO
    {
        public int ID { get; set; }
        public string login { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
    }
}
