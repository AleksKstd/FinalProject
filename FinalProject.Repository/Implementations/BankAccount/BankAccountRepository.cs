using FinalProject.Repository.Base;
using FinalProject.Repository.Helpers;
using FinalProject.Repository.Interfaces.BankAccount;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Implementations.BankAccount
{
    public class BankAccountRepository : BaseRepository<Models.BankAccount>, IBankAccountRepository
    {
        protected override string[] GetColumns()
        {
            return new[]
            {
                "BankAccountId",
                "IBAN",
                "Balance"
            };
        }

        protected override string GetTableName()
        {
            return "BankAccounts";
        }

        protected override Models.BankAccount MapToEntity(SqlDataReader reader)
        {
            return new Models.BankAccount
            {
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                IBAN = Convert.ToString(reader["IBAN"]),
                Balance = Convert.ToDecimal(reader["Balance"])
            };
        }

        public Task<int> CreateAsync(Models.BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.BankAccount> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync("BankAccountId", objectId);
        }

        public IAsyncEnumerable<Models.BankAccount> RetrieveCollectionAsync(BankAccountFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.BankAccountId is not null)
            {
                commandFilter.AddCondition("BankAccountId", filter.BankAccountId);
            }
            if (filter.IBAN is not null)
            {
                commandFilter.AddCondition("IBAN", filter.IBAN);
            }

                return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, BankAccountUpdate update)
        {
            throw new NotImplementedException();
        }
    }
}
