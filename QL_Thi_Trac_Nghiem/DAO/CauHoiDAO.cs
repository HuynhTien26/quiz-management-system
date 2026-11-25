using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class CauHoiDAO
    {
        private const string SelectCauHoi = @"
            SELECT ch.Ma_CH, ch.Ma_Mon, ISNULL(ch.Ma_GV, '') AS Ma_GV, ISNULL(m.Ten_Mon, '') AS Ten_Mon,
                   ch.Noi_Dung, ch.Dap_An_A, ch.Dap_An_B, ch.Dap_An_C, ch.Dap_An_D, ch.Dap_An_Dung, ch.Muc_Do
            FROM CAU_HOI ch
            LEFT JOIN Mon m ON ch.Ma_Mon = m.Ma_Mon";

        // 1. Đọc danh sách câu hỏi, lọc tổng quát theo Môn / Giảng viên / Mức độ (null = bỏ qua)
        public List<CauHoiDTO> GetCauHoi(string? maMon = null, string? maGV = null, string? mucDo = null)
        {
            List<CauHoiDTO> list = new List<CauHoiDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = SelectCauHoi + @"
                    WHERE (@MaMon IS NULL OR ch.Ma_Mon = @MaMon)
                      AND (@MaGV IS NULL OR ch.Ma_GV = @MaGV)
                      AND (@MucDo IS NULL OR ch.Muc_Do = @MucDo)
                    ORDER BY ch.Ma_CH";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaMon", string.IsNullOrWhiteSpace(maMon) ? (object)DBNull.Value : maMon.Trim());
                cmd.Parameters.AddWithValue("@MaGV", string.IsNullOrWhiteSpace(maGV) ? (object)DBNull.Value : maGV.Trim());
                cmd.Parameters.AddWithValue("@MucDo", string.IsNullOrWhiteSpace(mucDo) ? (object)DBNull.Value : mucDo.Trim());
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(DocCauHoi(rd));
                    }
                }
            }
            return list;
        }

        // 2. Đọc các câu hỏi chưa gán giảng viên (câu do Admin tạo)
        public List<CauHoiDTO> GetCauHoiChuaGanGiaoVien()
        {
            List<CauHoiDTO> list = new List<CauHoiDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = SelectCauHoi + @"
                    WHERE ch.Ma_GV IS NULL
                    ORDER BY ch.Ma_CH";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(DocCauHoi(rd));
                    }
                }
            }
            return list;
        }

        // 3. Thống kê số câu theo mức độ (theo môn nếu có)
        public Dictionary<string, int> GetThongKeMucDo(string? maMon = null)
        {
            Dictionary<string, int> thongKe = new Dictionary<string, int>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    SELECT Muc_Do, COUNT(1) AS SoLuong
                    FROM CAU_HOI
                    WHERE (@MaMon IS NULL OR Ma_Mon = @MaMon)
                    GROUP BY Muc_Do";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaMon", string.IsNullOrWhiteSpace(maMon) ? (object)DBNull.Value : maMon.Trim());
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        thongKe[rd["Muc_Do"].ToString() ?? ""] = Convert.ToInt32(rd["SoLuong"]);
                    }
                }
            }
            return thongKe;
        }

        // 4. Kiểm tra trùng mã câu hỏi
        public bool KiemTraTrungMaCauHoi(string maCh)
        {
            if (string.IsNullOrWhiteSpace(maCh)) return false;

            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT COUNT(1) FROM CAU_HOI WHERE Ma_CH = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maCh.Trim());
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Kiểm tra môn tồn tại để chặn lỗi khóa ngoại trước khi thêm/sửa
        public bool KiemTraTonTaiMon(string maMon)
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

        // Kiểm tra giảng viên tồn tại để chặn lỗi khóa ngoại trước khi thêm/sửa
        public bool KiemTraTonTaiGiangVien(string maGV)
        {
            if (string.IsNullOrWhiteSpace(maGV)) return false;

            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT COUNT(1) FROM Giang_Vien WHERE Ma_GV = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maGV.Trim());
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // Tập mã câu hỏi đã có, dùng để chặn trùng khi nhập hàng loạt từ Excel
        public HashSet<string> GetTatCaMaCauHoi()
        {
            HashSet<string> tapMa = new HashSet<string>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT Ma_CH FROM CAU_HOI";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string ma = rd["Ma_CH"].ToString() ?? "";
                        if (!string.IsNullOrWhiteSpace(ma)) tapMa.Add(ma.Trim());
                    }
                }
            }
            return tapMa;
        }

        // Tập mã môn hợp lệ, dùng để chặn lỗi khóa ngoại trước khi nhập hàng loạt
        public HashSet<string> GetTatCaMaMon()
        {
            HashSet<string> tapMa = new HashSet<string>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT Ma_Mon FROM Mon";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string ma = rd["Ma_Mon"].ToString() ?? "";
                        if (!string.IsNullOrWhiteSpace(ma)) tapMa.Add(ma.Trim());
                    }
                }
            }
            return tapMa;
        }

        // Bảng tra tên môn -> mã môn, dùng khi file Excel ghi tên môn thay vì mã
        public Dictionary<string, string> GetTenMonTheoMaMon()
        {
            Dictionary<string, string> bangTra = new Dictionary<string, string>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT Ma_Mon, Ten_Mon FROM Mon";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string ma = (rd["Ma_Mon"].ToString() ?? "").Trim();
                        string ten = (rd["Ten_Mon"].ToString() ?? "").Trim();
                        if (!string.IsNullOrWhiteSpace(ma) && !string.IsNullOrWhiteSpace(ten))
                        {
                            bangTra[ten] = ma;
                        }
                    }
                }
            }
            return bangTra;
        }

        // 5. Thêm câu hỏi mới
        public bool InsertCauHoi(CauHoiDTO c)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    INSERT INTO CAU_HOI (Ma_CH, Ma_Mon, Ma_GV, Noi_Dung, Dap_An_A, Dap_An_B, Dap_An_C, Dap_An_D, Dap_An_Dung, Muc_Do)
                    VALUES (@Ma, @MaMon, @MaGV, @NoiDung, @A, @B, @C, @D, @DapAnDung, @MucDo)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", c.MaCH);
                cmd.Parameters.AddWithValue("@MaMon", c.MaMon);
                cmd.Parameters.AddWithValue("@MaGV", string.IsNullOrWhiteSpace(c.MaGV) ? (object)DBNull.Value : c.MaGV);
                cmd.Parameters.AddWithValue("@NoiDung", c.NoiDung);
                cmd.Parameters.AddWithValue("@A", c.DapAnA);
                cmd.Parameters.AddWithValue("@B", c.DapAnB);
                cmd.Parameters.AddWithValue("@C", c.DapAnC);
                cmd.Parameters.AddWithValue("@D", c.DapAnD);
                cmd.Parameters.AddWithValue("@DapAnDung", c.DapAnDung);
                cmd.Parameters.AddWithValue("@MucDo", c.MucDo);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 6. Cập nhật câu hỏi
        public bool UpdateCauHoi(CauHoiDTO c)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    UPDATE CAU_HOI
                    SET Ma_Mon = @MaMon, Ma_GV = @MaGV, Noi_Dung = @NoiDung,
                        Dap_An_A = @A, Dap_An_B = @B, Dap_An_C = @C, Dap_An_D = @D,
                        Dap_An_Dung = @DapAnDung, Muc_Do = @MucDo
                    WHERE Ma_CH = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", c.MaCH);
                cmd.Parameters.AddWithValue("@MaMon", c.MaMon);
                cmd.Parameters.AddWithValue("@MaGV", string.IsNullOrWhiteSpace(c.MaGV) ? (object)DBNull.Value : c.MaGV);
                cmd.Parameters.AddWithValue("@NoiDung", c.NoiDung);
                cmd.Parameters.AddWithValue("@A", c.DapAnA);
                cmd.Parameters.AddWithValue("@B", c.DapAnB);
                cmd.Parameters.AddWithValue("@C", c.DapAnC);
                cmd.Parameters.AddWithValue("@D", c.DapAnD);
                cmd.Parameters.AddWithValue("@DapAnDung", c.DapAnDung);
                cmd.Parameters.AddWithValue("@MucDo", c.MucDo);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 7. Xóa câu hỏi
        public bool DeleteCauHoi(string maCh)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "DELETE FROM CAU_HOI WHERE Ma_CH = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maCh);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private static CauHoiDTO DocCauHoi(SqlDataReader rd)
        {
            return new CauHoiDTO
            {
                MaCH = rd["Ma_CH"].ToString() ?? "",
                MaMon = rd["Ma_Mon"].ToString() ?? "",
                MaGV = rd["Ma_GV"].ToString() ?? "",
                TenMon = rd["Ten_Mon"].ToString() ?? "",
                NoiDung = rd["Noi_Dung"].ToString() ?? "",
                DapAnA = rd["Dap_An_A"].ToString() ?? "",
                DapAnB = rd["Dap_An_B"].ToString() ?? "",
                DapAnC = rd["Dap_An_C"].ToString() ?? "",
                DapAnD = rd["Dap_An_D"].ToString() ?? "",
                DapAnDung = rd["Dap_An_Dung"].ToString() ?? "",
                MucDo = rd["Muc_Do"].ToString() ?? ""
            };
        }
    }
}