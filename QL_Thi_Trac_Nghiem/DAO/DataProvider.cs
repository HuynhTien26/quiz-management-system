using System;
using System.Data;
using System.Data.SqlClient;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class DataProvider
    {
        private static DataProvider? _instance;
        public static DataProvider Instance => _instance ??= new DataProvider();

        public static string ConnectionString { get; set; } = string.Empty;

        private DataProvider() { }

        public DataTable ExecuteQuery(string query, SqlParameter[]? parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(data);
                    }
                }
            }
            return data;
        }

        public int ExecuteNonQuery(string query, SqlParameter[]? parameters = null)
        {
            int affectedRows = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    affectedRows = command.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public object? ExecuteScalar(string query, SqlParameter[]? parameters = null)
        {
            object? result = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    result = command.ExecuteScalar();
                }
            }
            return result;
        }
    }
}
