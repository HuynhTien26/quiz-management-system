using System;
using System.Data;

namespace QL_Thi_Trac_Nghiem.DTO
{
    public class TaiKhoanDTO
    {
        public int UserID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string LoaiTaiKhoan { get; set; }

        public TaiKhoanDTO(int userID, string tenDangNhap, string matKhau, string hoTen, string loaiTaiKhoan)
        {
            UserID = userID;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            HoTen = hoTen;
            LoaiTaiKhoan = loaiTaiKhoan;
        }

        // Khởi tạo từ một dòng dữ liệu (DataRow) truy vấn từ SQL
        public TaiKhoanDTO(DataRow row)
        {
            UserID = (int)row["UserID"];
            TenDangNhap = row["TenDangNhap"].ToString();
            MatKhau = row["MatKhau"].ToString();
            HoTen = row["HoTen"].ToString();
            LoaiTaiKhoan = row["LoaiTaiKhoan"].ToString();
        }
    }
}