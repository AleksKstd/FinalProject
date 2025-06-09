using FinalProject.Repository.Base;
using FinalProject.Repository.Helpers;
using FinalProject.Repository.Interfaces.Payment;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Implementations.Payment
{
    public class PaymentRepository : BaseRepository<Models.Payment>, IPaymentRepository
    {
        protected override string[] GetColumns()
        {
            return new[]
            {
                "PaymentId",
                "UserId",
                "BankAccountId",
                "RecieverIBAN",
                "Credit",
                "Purpose",
                "PaymentDate",
                "Status",
            };
        }

        protected override string GetTableName()
        {
            return "Payments";
        }

        protected override Models.Payment MapToEntity(SqlDataReader reader)
        {
            return new Models.Payment
            {
                PaymentId = Convert.ToInt32(reader["PaymentId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                RecieverIBAN = Convert.ToString(reader["RecieverIBAN"]),
                Credit = Convert.ToDecimal(reader["Credit"]),
                Purpose = Convert.ToString(reader["Purpose"]),
                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                Status = Convert.ToString(reader["Status"]),
            };
        }
        public Task<int> CreateAsync(Models.Payment entity)
        {
            return base.CreateAsync(entity, "PaymentId");
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Payment> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync("PaymentId", objectId);
        }

        public IAsyncEnumerable<Models.Payment> RetrieveCollectionAsync(PaymentFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.UserId is not null)
            {
                commandFilter.AddCondition("UserId", filter.UserId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, PaymentUpdate update)
        {
            SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            Update updateCommand = new Update(
                connection,
                GetTableName(),
                "PaymentId",
                objectId);

            updateCommand.AddSetClause("Status", update.Status);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
