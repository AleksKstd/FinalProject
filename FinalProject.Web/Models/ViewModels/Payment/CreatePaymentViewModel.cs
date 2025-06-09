public class CreatePaymentViewModel
{
    public int UserId { get; set; }
    public int BankAccountId { get; set; }
    public string RecieverIBAN { get; set; }
    public decimal Credit { get; set; }
    public string Purpose { get; set; }
}
