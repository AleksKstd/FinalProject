namespace FinalProject.Services.DTOs.BankAccount
{
    public class GetAllUserBankAccountsResponse
    {
        public List<BankAccountInfo> Accounts { get; set; }
        public int TotalCount { get; set; }
    }
}
