using FinalProject.Repository.Helpers;
using Microsoft.Data.SqlClient;

namespace FinalProject.Repository.Base
{
    public abstract class BaseRepository<TObj>
    {
        protected abstract string GetTableName();
        protected abstract string[] GetColumns();
        protected virtual string SelectAllCommandText()
        {
            var columns = string.Join(", ", GetColumns());
            return $"SELECT {columns} FROM {GetTableName()}";
        }

        protected abstract TObj MapToEntity(SqlDataReader reader);

        protected async Task<int> CreateAsync(TObj entity, string idDbFieldEnumeratorName = null)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand command = connection.CreateCommand();

            var properties = typeof(TObj).GetProperties()
                .Where(p => p.Name != idDbFieldEnumeratorName)
                .ToList();

            string columns = string.Join(", ", properties.Select(p => p.Name));
            string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

            command.CommandText = $@"INSERT INTO {GetTableName()} ({columns}) 
                                VALUES ({parameters});
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            foreach (var prop in properties)
            {
                command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(entity) ?? DBNull.Value);
            }

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        protected async Task<TObj> RetrieveAsync(string idDbFieldName, int idDbFieldValue)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand sqlCommand = connection.CreateCommand();

            sqlCommand.CommandText =
                $"{SelectAllCommandText()} " +
                $"WHERE {idDbFieldName} = @${idDbFieldName}";

            sqlCommand.Parameters.AddWithValue($"@${idDbFieldName}", idDbFieldValue);
            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                TObj result = MapToEntity(reader);
                if (await reader.ReadAsync())
                {
                    throw new Exception($"Multiple records found for {idDbFieldName} = {idDbFieldValue}");
                }
                return result;
            }
            else
            {
                throw new Exception($"No record found for {idDbFieldName} = {idDbFieldValue}");
            }
        }

        protected async IAsyncEnumerable<TObj> RetrieveCollectionAsync(Filter filter)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = @$"{SelectAllCommandText()} WHERE 1 = 1";

            foreach (var condition in filter.Conditions)
            {
                sqlCommand.CommandText += $" AND {condition.Key} = @{condition.Key}";
                sqlCommand.Parameters.AddWithValue($"@{condition.Key}", condition.Value);
            }
            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                TObj result = MapToEntity(reader);
                yield return result;
            }
        }
        protected async Task<bool> DeleteAsync(string idDbFieldName, int objectId)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand command = connection.CreateCommand();
            using SqlTransaction transaction = command.Connection.BeginTransaction();

            command.CommandText = $"DELETE FROM {GetTableName()} WHERE {idDbFieldName} = @{idDbFieldName}";
            command.Parameters.AddWithValue($"@{idDbFieldName}", objectId);
            command.Transaction = transaction;

            // Execute the delete command and return the number of affected rows
            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                throw new Exception($"Just one row should be deleted! Command aborted, because {rowsAffected} could have been deleted!");
            }

            transaction.Commit();
            return true;
        }
    }
}
