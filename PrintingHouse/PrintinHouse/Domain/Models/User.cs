using Microsoft.AspNetCore.Identity;

namespace PrintingHouse.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool UserIsVerified { get; set; }
        public bool IsManager { get; set; }
        public bool IsOperator { get; set; }
        public bool IsSeniorOperator { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
