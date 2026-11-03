using System;
using System.Data.SqlClient;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class TaiKhoanDAO
    {
        // Giữ nguyên cấu trúc Singleton cho DAO của bạn
        private static TaiKhoanDAO instance;
        public static TaiKhoanDAO Instance
        {
            get { if (instance == null) instance = new TaiKhoanDAO(); return instance; }
            private set { instance = value; }
        }
        private TaiKhoanDAO() { }

        // Hàm kiểm tra đăng nhập chuẩn 3-Layer (dùng SqlDataReader thay cho DataTable)
        public string KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            string loaiTaiKhoan = "";
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                // Câu lệnh SQL kiểm tra cả user và pass
                string query = $"SELECT LoaiTaiKhoan FROM TaiKhoan WHERE TenDangNhap = '{tenDangNhap}' AND MatKhau = '{matKhau}'";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Nếu đọc được dữ liệu (tức là đúng user/pass)
                    {
                        loaiTaiKhoan = reader.GetString(0); // Lấy giá trị cột LoaiTaiKhoan ('AD', 'GV' hoặc 'TS')
                    }
                }
            }
            return loaiTaiKhoan; // Trả về rỗng ("") nếu sai tài khoản/mật khẩu
        }
    }
}