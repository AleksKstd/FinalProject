using Microsoft.Data.SqlClient;

namespace FinalProject.Repository
{
    public static class ConnectionFactory
    {
        private static string _connectionString;

        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        public static async Task<SqlConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            if (connection is null)
            {
                Console.WriteLine("Not connected");
            }
            return connection;
        }
    }
}
