using Microsoft.Data.SqlClient;

namespace DAL.Helpers
{
    public static class SqlConnectionExtension
    {
        public static void Execute(this SqlConnection connection, string expression)
        {
            connection.Open();
            var command = new SqlCommand(expression, connection);
            command.ExecuteNonQuery();
        } 
        
        public static SqlDataReader ExecuteReader(this SqlConnection connection, string expression)
        {
            connection.Open();
            var command = new SqlCommand(expression, connection);
            return command.ExecuteReader();
        }

    }
}