using FinalProject.Repository.Base;
using FinalProject.Repository.Helpers;
using FinalProject.Repository.Interfaces.UserToAccount;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Implementations.UserToAccount
{
    public class UserToAccountRepository : BaseRepository<Models.UserToAccount>, IUserToAccountRepository
    {
        protected override string[] GetColumns()
        {
            return new[]
            {
                "BankAccountId",
                "UserId"
            };
        }

        protected override string GetTableName()
        {
            return "UsersToAccounts";
        }

        protected override Models.UserToAccount MapToEntity(SqlDataReader reader)
        {
            return new Models.UserToAccount
            {
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                UserId = Convert.ToInt32(reader["UserId"])
            };
        }


        public Task<int> CreateAsync(Models.UserToAccount entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.UserToAccount> RetrieveAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Models.UserToAccount> RetrieveCollectionAsync(UserToAccountFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.UserId is not null)
            {
                commandFilter.AddCondition("UserId", filter.UserId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, UserToAccountUpdate update)
        {
            throw new NotImplementedException();
        }

    }
}
