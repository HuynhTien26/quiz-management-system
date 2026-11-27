using System;
using System.Data;
using System.Data.SqlClient;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class TaiKhoanDAO
    {
        private static TaiKhoanDAO? instance;
        public static TaiKhoanDAO Instance => instance ??= new TaiKhoanDAO();
        private TaiKhoanDAO() { }

        public string KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            const string sql = @"
                SELECT LoaiTaiKhoan
                FROM TaiKhoan
                WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = (tenDangNhap ?? "").Trim();
                cmd.Parameters.Add("@MatKhau", SqlDbType.VarChar, 50).Value = (matKhau ?? "").Trim();

                conn.Open();
                object? res = cmd.ExecuteScalar();
                return res == null || res == DBNull.Value ? string.Empty : res.ToString()?.Trim() ?? string.Empty;
            }
        }
    }
}