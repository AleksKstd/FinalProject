using System.ComponentModel.DataAnnotations;

public class CreatePaymentViewModel
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Трябва да посочите Ваша сметка")]
    public int BankAccountId { get; set; }

    [Required(ErrorMessage = "IBAN на получателят е задължителен")]
    [RegularExpression(@"^[A-Z]{2}\d{2}[A-Z0-9]{18}$", ErrorMessage = "Невалиден IBAN")]
    public string RecieverIBAN { get; set; }

    [Required(ErrorMessage = "Сумата за превода е задължителна")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумата трябва да е по голяма от 0.00 BGN")]
    public decimal Credit { get; set; }

    [Required(ErrorMessage = "Цел на превода е задължителна")]
    [StringLength(32, ErrorMessage = "Целта трябва да е по малко от 32 символа")]
    public string Purpose { get; set; }
}
