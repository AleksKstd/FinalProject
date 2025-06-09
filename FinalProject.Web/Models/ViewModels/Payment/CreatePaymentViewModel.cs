using System.ComponentModel.DataAnnotations;

public class CreatePaymentViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Source account is required")]
    public int BankAccountId { get; set; }

    [Required(ErrorMessage = "IBAN is required")]
    [RegularExpression(@"^[A-Z]{2}\d{2}[A-Z0-9]{18}$", ErrorMessage = "Invalid IBAN format")]
    public string RecieverIBAN { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
    public decimal Credit { get; set; }

    [Required(ErrorMessage = "Purpose is required")]
    [StringLength(32, ErrorMessage = "Purpose must be 32 characters or less")]
    public string Purpose { get; set; }
}
