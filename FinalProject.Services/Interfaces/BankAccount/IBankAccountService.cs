using FinalProject.Services.DTOs.BankAccount;

namespace FinalProject.Services.Interfaces.BankAccount
{
    public interface IBankAccountService
    {
        Task<GetAllUserBankAccountsResponse> GetAllUserBankAccounts(int userId);
    }
}
