using System;
using System.Data.SqlClient;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class MyConnection
    {
        // Chuỗi kết nối giữ nguyên thông số của bạn
        private static string connectionSTR = @"Data Source=localhost;Initial Catalog=QL_ThiTracNghiem;Integrated Security=True;Encrypt=False";

        // Hàm này chỉ làm đúng 1 nhiệm vụ: Cung cấp chìa khóa kết nối cho các DAO
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionSTR);
        }
    }
}