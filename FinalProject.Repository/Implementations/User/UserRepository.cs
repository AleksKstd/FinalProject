using FinalProject.Repository.Base;
using FinalProject.Repository.Helpers;
using FinalProject.Repository.Interfaces.User;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Implementations.User
{
    public class UserRepository : BaseRepository<Models.User>, IUserRepository
    {
        protected override string[] GetColumns()
        {
            return new[]
            {
                "UserId",
                "Username",
                "Password",
                "Email",
                "FullName"
            };
        }

        protected override string GetTableName()
        {
            return "Users";
        }

        protected override Models.User MapToEntity(SqlDataReader reader)
        {
            return new Models.User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"]),
                Email = Convert.ToString(reader["Email"]),
                FullName = Convert.ToString(reader["FullName"])
            };
        }
        public async Task<int> CreateAsync(Models.User entity)
        {
            return await base.CreateAsync(entity, "UserId");
        }

        public async Task<bool> DeleteAsync(int objectId)
        {
            return await base.DeleteAsync("UserId", objectId);
        }

        public async Task<Models.User> RetrieveAsync(int objectId)
        {
            return await base.RetrieveAsync("UserId", objectId);
        }

        public IAsyncEnumerable<Models.User> RetrieveCollectionAsync(UserFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Username is not null)
            {
                commandFilter.AddCondition("Username", filter.Username);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, UserUpdate update)
        {
            throw new NotImplementedException("Users cannot be updated.");
        }
    }
}
