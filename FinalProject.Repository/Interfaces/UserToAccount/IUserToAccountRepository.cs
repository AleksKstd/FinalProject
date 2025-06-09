using FinalProject.Repository.Base;

namespace FinalProject.Repository.Interfaces.UserToAccount
{
    public interface IUserToAccountRepository : IBaseRepository<Models.UserToAccount, UserToAccountFilter, UserToAccountUpdate>
    {
    }
}
