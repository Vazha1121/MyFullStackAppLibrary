namespace PrintingHouse.Domain.Models
{
    public class VerificationCode
    {
        public int VerificationCodeID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
    }
}
