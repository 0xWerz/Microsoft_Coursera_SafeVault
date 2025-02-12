using System.ComponentModel.DataAnnotations;

namespace SafeVault.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Username { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User"; // Default role
    }
}
