using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BankAccountId { get; set; }
        [Required]
        [StringLength(22, ErrorMessage = "RecieverIBAN can't be longer than 22.")]
        public string RecieverIBAN { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Credit { get; set; }
        [Required]
        [StringLength(32, ErrorMessage = "Purpose can't be longer than 32.")]
        public string Purpose { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Status can't be longer than 10.")]
        public string Status { get; set; }
    }
}
