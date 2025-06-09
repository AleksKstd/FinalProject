using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class UserToAccount
    {
        [Required]
        public int BankAccountId { get; set; }
        [Required]
        public int UserId { get; set; }

    }
}
