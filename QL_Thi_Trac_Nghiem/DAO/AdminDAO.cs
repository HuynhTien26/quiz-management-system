using System;
using System.Collections.Generic;
using System.Data;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class AdminDAO
    {
        private static AdminDAO instance;
        public static AdminDAO Instance
        {
            get { if (instance == null) instance = new AdminDAO(); return instance; }
            private set { instance = value; }
        }
        private AdminDAO() { }

        // ================= LẤY DANH SÁCH =================
        public DataTable LayDanhSachGiaoVien()
        {
            string query = "SELECT Ma_GV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Ma_Mon FROM Giang_Vien";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable LayDanhSachSinhVien()
        {
            string query = "SELECT Ma_SV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Lop FROM Sinh_Vien";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // ================= KIỂM TRA TRÙNG MÃ =================
        public bool KiemTraTrungMaGV(string maGV)
        {
            string query = $"SELECT 1 FROM Giang_Vien WHERE Ma_GV = '{maGV}'";
            return DataProvider.Instance.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool KiemTraTrungMaSV(string maSV)
        {
            string query = $"SELECT 1 FROM Sinh_Vien WHERE Ma_SV = '{maSV}'";
            return DataProvider.Instance.ExecuteQuery(query).Rows.Count > 0;
        }

        // ================= THÊM MỚI =================
        public bool ThemGiaoVien(string ma, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string mon)
        {
            string ns = ngaySinh.ToString("yyyy-MM-dd");
            string query = $@"
                INSERT INTO Giang_Vien (Ma_GV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Ma_Mon)
                VALUES ('{ma}', N'{ho}', N'{ten}', '{ns}', N'{gioiTinh}', N'{khoa}', N'{mon}');

                IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = '{ma}')
                BEGIN
                    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, LoaiTaiKhoan)
                    VALUES ('{ma}', '123456', N'{ho} {ten}', 'GV');
                END";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool ThemSinhVien(string ma, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string lop)
        {
            string ns = ngaySinh.ToString("yyyy-MM-dd");
            string query = $@"
                INSERT INTO Sinh_Vien (Ma_SV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Lop)
                VALUES ('{ma}', N'{ho}', N'{ten}', '{ns}', N'{gioiTinh}', N'{khoa}', '{lop}');

                IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = '{ma}')
                BEGIN
                    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, LoaiTaiKhoan)
                    VALUES ('{ma}', '123456', N'{ho} {ten}', 'SV');
                END";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // ================= CẬP NHẬT =================
        public bool CapNhatGiaoVien(string maCu, string maMoi, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string mon)
        {
            string ns = ngaySinh.ToString("yyyy-MM-dd");
            string query = $@"
                UPDATE Giang_Vien 
                SET Ma_GV = '{maMoi}', Ho_lot = N'{ho}', Ten = N'{ten}', Ngay_sinh = '{ns}', Gioi_tinh = N'{gioiTinh}', Ma_Khoa = N'{khoa}', Ma_Mon = N'{mon}'
                WHERE Ma_GV = '{maCu}';

                UPDATE TaiKhoan 
                SET TenDangNhap = '{maMoi}', HoTen = N'{ho} {ten}' 
                WHERE TenDangNhap = '{maCu}';";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool CapNhatSinhVien(string maCu, string maMoi, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string lop)
        {
            string ns = ngaySinh.ToString("yyyy-MM-dd");
            string query = $@"
                UPDATE Sinh_Vien 
                SET Ma_SV = '{maMoi}', Ho_lot = N'{ho}', Ten = N'{ten}', Ngay_sinh = '{ns}', Gioi_tinh = N'{gioiTinh}', Ma_Khoa = N'{khoa}', Lop = '{lop}'
                WHERE Ma_SV = '{maCu}';

                UPDATE TaiKhoan 
                SET TenDangNhap = '{maMoi}', HoTen = N'{ho} {ten}' 
                WHERE TenDangNhap = '{maCu}';";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // ================= XÓA =================
        public bool XoaGiaoVien(string ma)
        {
            string query = $@"
                DELETE FROM Giang_Vien WHERE Ma_GV = '{ma}';
                DELETE FROM TaiKhoan WHERE TenDangNhap = '{ma}';";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool XoaSinhVien(string ma)
        {
            string query = $@"
                DELETE FROM Sinh_Vien WHERE Ma_SV = '{ma}';
                DELETE FROM TaiKhoan WHERE TenDangNhap = '{ma}';";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // ================= BỘ LỌC TÌM KIẾM THEO CONTROL =================
        public DataTable TimGiaoVien(string ma, string ho, string ten, string gioiTinh, string khoa, List<string> dsMon)
        {
            string query = "SELECT Ma_GV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Ma_Mon FROM Giang_Vien WHERE 1=1";

            if (!string.IsNullOrEmpty(ma))
                query += $" AND Ma_GV LIKE '%{ma}%'";
            if (!string.IsNullOrEmpty(ho))
                query += $" AND Ho_lot LIKE N'%{ho}%'";
            if (!string.IsNullOrEmpty(ten))
                query += $" AND Ten LIKE N'%{ten}%'";
            if (!string.IsNullOrEmpty(gioiTinh))
                query += $" AND Gioi_tinh = N'{gioiTinh}'";
            if (!string.IsNullOrEmpty(khoa))
                query += $" AND Ma_Khoa = N'{khoa}'";

            // Lọc theo từng môn học được chọn trong CheckedListBox
            if (dsMon != null && dsMon.Count > 0)
            {
                foreach (string mon in dsMon)
                {
                    query += $" AND Ma_Mon LIKE N'%{mon}%'";
                }
            }

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable TimSinhVien(string ma, string ho, string ten, string gioiTinh, string khoa, string lop)
        {
            string query = "SELECT Ma_SV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Lop FROM Sinh_Vien WHERE 1=1";

            if (!string.IsNullOrEmpty(ma))
                query += $" AND Ma_SV LIKE '%{ma}%'";
            if (!string.IsNullOrEmpty(ho))
                query += $" AND Ho_lot LIKE N'%{ho}%'";
            if (!string.IsNullOrEmpty(ten))
                query += $" AND Ten LIKE N'%{ten}%'";
            if (!string.IsNullOrEmpty(gioiTinh))
                query += $" AND Gioi_tinh = N'{gioiTinh}'";
            if (!string.IsNullOrEmpty(khoa))
                query += $" AND Ma_Khoa = N'{khoa}'";
            if (!string.IsNullOrEmpty(lop))
                query += $" AND Lop = '{lop}'";

            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}