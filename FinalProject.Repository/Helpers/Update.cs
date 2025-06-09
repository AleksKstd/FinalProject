using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace FinalProject.Repository.Helpers
{
    public class Update : IDisposable
    {
        private List<string> setClauses = new List<string>();
        private SqlCommand command;
        private readonly string idDbFieldName;
        private readonly int idDbFieldValue;

        public Update(
            SqlConnection connection,
            string tableName,
            string idDbFieldName,
            int idDbFieldValue)
        {
            this.idDbFieldName = idDbFieldName;
            this.idDbFieldValue = idDbFieldValue;
            command = connection.CreateCommand();
            command.CommandText = $"UPDATE {tableName}";
        }

        public void AddSetClause(string fieldName, INullable? fieldValue)
        {
            if (fieldValue != null)
            {
                setClauses.Add($"{fieldName} = @{fieldName}");
                command.Parameters.AddWithValue($"@{fieldName}", fieldValue);
            }
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            if (setClauses.Count == 0)
            {
                throw new InvalidOperationException("No set clauses have been added.");
            }

            command.CommandText += @$" SET {string.Join(", ", setClauses)} WHERE {idDbFieldName} = @{idDbFieldName}";

            command.Parameters.AddWithValue($"@{idDbFieldName}", idDbFieldValue);

            SqlTransaction transaction = command.Connection.BeginTransaction();
            command.Transaction = transaction;
            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                throw new InvalidOperationException($"Only one row should be updated! Aborted update because -> {rowsAffected} rows could have been updated.");
            }

            transaction.Commit();
            return rowsAffected;
        }
        public void Dispose()
        {
            command?.Dispose();
        }
    }
}
