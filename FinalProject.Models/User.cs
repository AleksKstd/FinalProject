using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Username can't be longer than 50.")]
        public string Username { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "Password can't be longer than 64.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "FullName can't be longer than 100.")]
        public string FullName { get; set; }
    }
}
