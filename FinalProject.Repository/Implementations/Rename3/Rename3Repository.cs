using FinalProject.Repository.Base;
using FinalProject.Repository.Interfaces.Rename3;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Implementations.Rename3
{
    public class Rename3Repository : BaseRepository<Models.Rename3>, IRename3Repository
    {
        protected override string[] GetColumns()
        {
            return new[]
            {
                "Rename3",
                "Rename3",
                "Rename3",
                "Rename3",
                "Rename3",
                "Rename3",
                "Rename3"
            };
        }

        protected override string GetTableName()
        {
            return "Rename3";
        }

        protected override Models.Rename3 MapToEntity(SqlDataReader reader)
        {
            return new Models.Rename3
            {
                Rename3 = Convert.ToInt32(reader["Rename3"]),
                Rename3 = Convert.ToInt32(reader["Rename3"]),
                Rename3 = Convert.ToString(reader["Rename3"]),
                Rename3 = Convert.ToBoolean(reader["Rename3"]),
                Rename3 = Convert.ToBoolean(reader["Rename3"]),
                Rename3 = Convert.ToBoolean(reader["Rename3"]),
                Rename3 = Convert.ToBoolean(reader["Rename3"])
            };
        }
        public Task<int> CreateAsync(Models.Rename3 entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Rename3> RetrieveAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Models.Rename3> RetrieveCollectionAsync(Rename3Filter filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int objectId, Rename3Update update)
        {
            throw new NotImplementedException();
        }
    }
}
