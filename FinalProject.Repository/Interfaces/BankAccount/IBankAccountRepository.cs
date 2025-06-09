using FinalProject.Repository.Base;

namespace FinalProject.Repository.Interfaces.BankAccount
{
    public interface IBankAccountRepository : IBaseRepository<Models.BankAccount, BankAccountFilter, BankAccountUpdate>
    {

    }
}
