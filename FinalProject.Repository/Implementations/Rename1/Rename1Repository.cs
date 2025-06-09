using FinalProject.Repository.Base;
using FinalProject.Repository.Interfaces.Rename1;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Implementations.Rename1
{
    public class Rename1Repository : BaseRepository<Models.Rename1>, IRename1Repository
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

        protected override Models.Rename1 MapToEntity(SqlDataReader reader)
        {
            return new Models.Rename1
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

        public Task<int> CreateAsync(Models.Rename1 entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Rename1> RetrieveAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Models.Rename1> RetrieveCollectionAsync(Rename1Filter filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int objectId, Rename1Update update)
        {
            throw new NotImplementedException();
        }
    }
}
