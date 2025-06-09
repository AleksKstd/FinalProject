using FinalProject.Repository.Base;
using FinalProject.Repository.Interfaces.Rename2;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Implementations.Rename2
{
    public class Rename2Repository : BaseRepository<Models.Rename2>, IRename2Repository
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

        protected override Models.Rename2 MapToEntity(SqlDataReader reader)
        {
            return new Models.Rename2
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


        public Task<int> CreateAsync(Models.Rename2 entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Rename2> RetrieveAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Models.Rename2> RetrieveCollectionAsync(Rename2Filter filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int objectId, Rename2Update update)
        {
            throw new NotImplementedException();
        }

    }
}
