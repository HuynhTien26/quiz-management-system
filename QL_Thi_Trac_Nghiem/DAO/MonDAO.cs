using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class MonDAO
    {
        // 1. Đọc danh sách Môn học (có Join với Khoa để lấy Tên Khoa nếu cần)
        public List<MonDTO> GetAllMon()
        {
            List<MonDTO> list = new List<MonDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT Ma_Mon, Ten_Mon, ISNULL(Tin_Chi, 3) AS Tin_Chi, ISNULL(Ma_Khoa, '') AS Ma_Khoa FROM Mon ORDER BY Ma_Mon";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new MonDTO
                        {
                            MaMon = rd["Ma_Mon"].ToString() ?? "",
                            TenMon = rd["Ten_Mon"].ToString() ?? "",
                            TinChi = Convert.ToInt32(rd["Tin_Chi"]),
                            MaKhoa = rd["Ma_Khoa"].ToString() ?? ""
                        });
                    }
                }
            }
            return list;
        }

        // 2. Kiểm tra trùng Mã môn
        public bool KiemTraTrungMaMon(string maMon)
        {
            if (string.IsNullOrWhiteSpace(maMon)) return false;

            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT COUNT(1) FROM Mon WHERE Ma_Mon = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maMon.Trim());
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Kiểm tra khoa có tồn tại để BUS chặn lỗi khóa ngoại trước khi thêm/sửa môn
        public bool KiemTraTonTaiKhoa(string maKhoa)
        {
            if (string.IsNullOrWhiteSpace(maKhoa)) return false;

            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT COUNT(1) FROM Khoa WHERE Ma_Khoa = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maKhoa.Trim());
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // 3. Thêm Môn học mới
        public bool InsertMon(MonDTO m)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "INSERT INTO Mon (Ma_Mon, Ten_Mon, Tin_Chi, Ma_Khoa) VALUES (@Ma, @Ten, @TC, @MaKhoa)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", m.MaMon);
                cmd.Parameters.AddWithValue("@Ten", m.TenMon);
                cmd.Parameters.AddWithValue("@TC", m.TinChi);
                cmd.Parameters.AddWithValue("@MaKhoa", string.IsNullOrWhiteSpace(m.MaKhoa) ? DBNull.Value : m.MaKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 4. Cập nhật Môn học
        public bool UpdateMon(MonDTO m)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "UPDATE Mon SET Ten_Mon = @Ten, Tin_Chi = @TC, Ma_Khoa = @MaKhoa WHERE Ma_Mon = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", m.MaMon);
                cmd.Parameters.AddWithValue("@Ten", m.TenMon);
                cmd.Parameters.AddWithValue("@TC", m.TinChi);
                cmd.Parameters.AddWithValue("@MaKhoa", string.IsNullOrWhiteSpace(m.MaKhoa) ? DBNull.Value : m.MaKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Đếm tham chiếu tới môn để chặn xóa trước khi dữ liệu bị xóa dây chuyền
        public int DemThamChieuMon(string maMon)
        {
            if (string.IsNullOrWhiteSpace(maMon)) return 0;

            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    SELECT (SELECT COUNT(1) FROM De_Thi     WHERE Ma_Mon = @Ma)
                         + (SELECT COUNT(1) FROM Giang_Vien WHERE Ma_Mon = @Ma)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maMon.Trim());
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // 5. Xóa Môn học
        public bool DeleteMon(string maMon)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "DELETE FROM Mon WHERE Ma_Mon = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maMon);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}