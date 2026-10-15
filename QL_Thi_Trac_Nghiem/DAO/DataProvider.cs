using System;
using System.Data;
using System.Data.SqlClient;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        // LƯU Ý QUAN TRỌNG: Sửa chữ 'localhost' thành tên Server của bạn nếu cần
        private string connectionSTR = @"Data Source=localhost;Initial Catalog=QL_ThiTracNghiem;Integrated Security=True;Encrypt=False";

        public DataTable ExecuteQuery(string query)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }

        // Thêm hàm này vào bên trong class DataProvider (ngay dưới hàm ExecuteQuery)
        public int ExecuteNonQuery(string query)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }
    }
}