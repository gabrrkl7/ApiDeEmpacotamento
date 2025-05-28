using System.ComponentModel.DataAnnotations;

namespace LojaDoManoel.Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        public string Role { get; set; } = "User"; 
    }
}