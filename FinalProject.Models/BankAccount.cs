using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        [Required]
        [StringLength(22, ErrorMessage = "IBAN can't be longer than 22.")]
        public string IBAN { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Balance { get; set; }
    }
}
