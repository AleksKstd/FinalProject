namespace FinalProject.Services.DTOs.Payment
{
    public class PaymentInfo
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int BankAccountId { get; set; }
        public string RecieverIBAN { get; set; }
        public decimal Credit { get; set; }
        public string Purpose { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
}
