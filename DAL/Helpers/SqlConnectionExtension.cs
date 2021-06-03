using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DAL.Helpers
{
    /// <summary>
    /// Improve readability of code
    /// </summary>
    public static class SqlConnectionExtension
    {
        public static void Execute(this SqlConnection connection, string expression)
        {
            connection.Open();
            var command = new SqlCommand(expression, connection);
            command.ExecuteNonQuery();
        } 
        
        public static async Task ExecuteAsync(this SqlConnection connection, string expression)
        {
            await connection.OpenAsync();
            var command = new SqlCommand(expression, connection);
            await command.ExecuteNonQueryAsync();
        } 
        
        public static SqlDataReader ExecuteReader(this SqlConnection connection, string expression)
        {
            connection.Open();
            var command = new SqlCommand(expression, connection);
            return command.ExecuteReader();
        }

        public static async Task<SqlDataReader> ExecuteReaderAsync(this SqlConnection connection, string expression)
        {
            await connection.OpenAsync();
            var command = new SqlCommand(expression, connection);
            return await command.ExecuteReaderAsync();
        }
    }
}