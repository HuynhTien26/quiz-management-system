using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class ExamConfigDAO
    {
        private DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        private object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        private int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // ==================== 1. KỲ THI (Ky_Thi) ====================
        public DataTable GetAllKyThi()
        {
            return ExecuteQuery("SELECT Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc FROM Ky_Thi ORDER BY Ngay_Thi DESC");
        }

        public bool IsDuplicateNgayThi(DateTime ngayThi, string? excludeMaKT = null)
        {
            string sql = "SELECT COUNT(*) FROM Ky_Thi WHERE CAST(Ngay_Thi AS DATE) = CAST(@NgayThi AS DATE)";
            var p = new List<SqlParameter> { new SqlParameter("@NgayThi", ngayThi.Date) };
            if (!string.IsNullOrEmpty(excludeMaKT))
            {
                sql += " AND Ma_KT <> @MaKT";
                p.Add(new SqlParameter("@MaKT", excludeMaKT));
            }
            return Convert.ToInt32(ExecuteScalar(sql, p.ToArray())) > 0;
        }

        public bool HasStudentsInKyThi(string maKT)
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM Bai_Thi bt
                JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De
                WHERE dt.Ma_KT = @MaKT";
            return Convert.ToInt32(ExecuteScalar(sql, new SqlParameter("@MaKT", maKT))) > 0;
        }

        public bool InsertKyThi(string maKT, string ten, DateTime ngayThi, string? namHoc = null)
        {
            string namHocChuan = string.IsNullOrWhiteSpace(namHoc)
                ? $"{ngayThi.Year}-{ngayThi.Year + 1}"
                : namHoc.Trim();

            const string sql = "INSERT INTO Ky_Thi (Ma_KT, Ten_KT, Ngay_Thi, Nam_Hoc) VALUES (@MaKT, @Ten, @NgayThi, @NamHoc)";
            return ExecuteNonQuery(sql,
                new SqlParameter("@MaKT", maKT),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@NgayThi", ngayThi),
                new SqlParameter("@NamHoc", namHocChuan)) > 0;
        }

        public bool UpdateKyThi(string maKT, string ten, DateTime ngayThi, string? namHoc = null)
        {
            string namHocChuan = string.IsNullOrWhiteSpace(namHoc)
                ? $"{ngayThi.Year}-{ngayThi.Year + 1}"
                : namHoc.Trim();

            const string sql = @"
        UPDATE Ky_Thi
        SET Ten_KT = @Ten,
            Ngay_Thi = @NgayThi,
            Nam_Hoc = @NamHoc
        WHERE Ma_KT = @MaKT";

            return ExecuteNonQuery(sql,
                new SqlParameter("@MaKT", maKT),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@NgayThi", ngayThi),
                new SqlParameter("@NamHoc", namHocChuan)) > 0;
        }

        public bool DeleteKyThi(string maKT)
        {
            const string sql = "DELETE FROM Ky_Thi WHERE Ma_KT = @MaKT";
            return ExecuteNonQuery(sql, new SqlParameter("@MaKT", maKT)) > 0;
        }

        // ==================== 2. MÔN HỌC & MA TRẬN ĐỀ (Mon, Ma_Tran) ====================
        public DataTable GetAllMon()
        {
            return ExecuteQuery("SELECT Ma_Mon, Ten_Mon FROM Mon ORDER BY Ma_Mon");
        }

        public DataTable GetAllMaTran()
        {
            const string sql = @"
                SELECT mt.Ma_MT, mt.Ma_Mon, m.Ten_Mon,
                       mt.Ty_le_co_ban, mt.Ty_le_van_dung, mt.Ty_le_vd_nang_cao
                FROM Ma_Tran mt
                LEFT JOIN Mon m ON mt.Ma_Mon = m.Ma_Mon
                ORDER BY mt.Ma_MT";
            return ExecuteQuery(sql);
        }

        public bool SaveMaTran(string maMT, string maMon, double tyLeCoBan, double tyLeVanDung, double tyLeVDNangCao)
        {
            const string sql = @"
                IF EXISTS (SELECT 1 FROM Ma_Tran WHERE Ma_MT = @MaMT)
                    UPDATE Ma_Tran
                    SET Ma_Mon = @MaMon,
                        Ty_le_co_ban = @CB,
                        Ty_le_van_dung = @VD,
                        Ty_le_vd_nang_cao = @VDNC
                    WHERE Ma_MT = @MaMT
                ELSE
                    INSERT INTO Ma_Tran (Ma_MT, Ma_Mon, Ty_le_co_ban, Ty_le_van_dung, Ty_le_vd_nang_cao)
                    VALUES (@MaMT, @MaMon, @CB, @VD, @VDNC)";
            return ExecuteNonQuery(sql,
                new SqlParameter("@MaMT", maMT),
                new SqlParameter("@MaMon", maMon),
                new SqlParameter("@CB", tyLeCoBan),
                new SqlParameter("@VD", tyLeVanDung),
                new SqlParameter("@VDNC", tyLeVDNangCao)) > 0;
        }

        public bool DeleteMaTran(string maMT)
        {
            const string sql = "DELETE FROM Ma_Tran WHERE Ma_MT = @MaMT";
            return ExecuteNonQuery(sql, new SqlParameter("@MaMT", maMT)) > 0;
        }

        // ==================== 3. ĐỀ THI & CHI TIẾT ĐỀ (De_Thi, CT_DT) ====================
        public List<string> GetRandomQuestionIds(string maMon, string mucDo, int soLuong)
        {
            var ids = new List<string>();
            if (soLuong <= 0) return ids;

            bool laNangCao = mucDo.Equals("Nâng cao", StringComparison.OrdinalIgnoreCase)
                          || mucDo.Equals("Vận dụng cao", StringComparison.OrdinalIgnoreCase)
                          || mucDo.Equals("Vận dụng nâng cao", StringComparison.OrdinalIgnoreCase);

            string sql = laNangCao
                ? "SELECT TOP (@SL) Ma_CH FROM CAU_HOI WHERE Ma_Mon = @MaMon AND Muc_Do IN (N'Nâng cao', N'Vận dụng cao', N'Vận dụng nâng cao') ORDER BY NEWID()"
                : "SELECT TOP (@SL) Ma_CH FROM CAU_HOI WHERE Ma_Mon = @MaMon AND Muc_Do = @MucDo ORDER BY NEWID()";

            DataTable dt = laNangCao
                ? ExecuteQuery(sql, new SqlParameter("@SL", soLuong), new SqlParameter("@MaMon", maMon))
                : ExecuteQuery(sql, new SqlParameter("@SL", soLuong), new SqlParameter("@MaMon", maMon), new SqlParameter("@MucDo", mucDo));

            foreach (DataRow row in dt.Rows)
                ids.Add(row["Ma_CH"]?.ToString() ?? string.Empty);
            return ids;
        }

        public bool InsertDeThiWithDetails(string maDe, string maKT, string maMT, string maMon, string maGV, int soLuongCH, int thoiGianLamBai, List<string> danhSachMaCH)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        const string insertDe = @"
                                                 INSERT INTO De_Thi (Ma_De, Ma_KT, Ma_MT, Ma_Mon, Ma_GV, So_Cau_Hoi, Thoi_Gian, Trang_Thai)
                                                 VALUES (@MaDe, @MaKT, @MaMT, @MaMon, @MaGV, @SL, @TG, @TrangThai)";
                        using (SqlCommand cmdDe = new SqlCommand(insertDe, conn, trans))
                        {
                            string trangThaiMacDinh = string.Equals(maGV, "ADMIN", StringComparison.OrdinalIgnoreCase)
                                ? "Đã đăng tải"
                                : "Chờ duyệt";

                            cmdDe.Parameters.AddWithValue("@MaDe", maDe);
                            cmdDe.Parameters.AddWithValue("@MaKT", maKT);
                            cmdDe.Parameters.AddWithValue("@MaMT", string.IsNullOrWhiteSpace(maMT) ? (object)DBNull.Value : maMT);
                            cmdDe.Parameters.AddWithValue("@MaMon", maMon);
                            cmdDe.Parameters.AddWithValue("@MaGV", string.IsNullOrWhiteSpace(maGV) ? (object)DBNull.Value : maGV);
                            cmdDe.Parameters.AddWithValue("@SL", soLuongCH);
                            cmdDe.Parameters.AddWithValue("@TG", thoiGianLamBai);
                            cmdDe.Parameters.AddWithValue("@TrangThai", trangThaiMacDinh);
                            cmdDe.ExecuteNonQuery();
                        }

                        const string insertCT = "INSERT INTO CT_DT (Ma_De, Ma_CH) VALUES (@MaDe, @MaCH)";
                        foreach (string maCH in danhSachMaCH)
                        {
                            using (SqlCommand cmdCT = new SqlCommand(insertCT, conn, trans))
                            {
                                cmdCT.Parameters.AddWithValue("@MaDe", maDe);
                                cmdCT.Parameters.AddWithValue("@MaCH", maCH);
                                cmdCT.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool UpdateDeThi(string maDe, string maKT, string maMT, string maMon, int soLuongCH, int thoiGianLamBai)
        {
            const string sql = @"
        UPDATE De_Thi
        SET Ma_Mon = @MaMon,
            Ma_MT = @MaMT,
            Ma_KT = @MaKT,
            So_Cau_Hoi = @SoCau,
            Thoi_Gian = @ThoiGian
        WHERE Ma_De = @MaDe";

            return ExecuteNonQuery(sql,
                new SqlParameter("@MaDe", maDe.Trim()),
                new SqlParameter("@MaMon", string.IsNullOrWhiteSpace(maMon) ? (object)DBNull.Value : maMon.Trim()),
                new SqlParameter("@MaMT", string.IsNullOrWhiteSpace(maMT) ? (object)DBNull.Value : maMT.Trim()),
                new SqlParameter("@MaKT", string.IsNullOrWhiteSpace(maKT) ? (object)DBNull.Value : maKT.Trim()),
                new SqlParameter("@SoCau", soLuongCH),
                new SqlParameter("@ThoiGian", thoiGianLamBai)) > 0;
        }

        public DataTable GetAllDeThi()
        {
            return ExecuteQuery("SELECT * FROM De_Thi ORDER BY Ma_De DESC");
        }

        public DataTable GetChiTietDeThi(string maDe)
        {
            const string sql = @"
                SELECT ch.Ma_CH, ch.Muc_Do, ch.Noi_Dung,
                       ch.Dap_An_A, ch.Dap_An_B, ch.Dap_An_C, ch.Dap_An_D, ch.Dap_An_Dung
                FROM CT_DT ct
                JOIN CAU_HOI ch ON ct.Ma_CH = ch.Ma_CH
                WHERE ct.Ma_De = @MaDe";
            return ExecuteQuery(sql, new SqlParameter("@MaDe", maDe));
        }

        //MỖI GV CHỈ XEM ĐƯỢC ĐỀ DO CHÍNH MÌNH TẠO RA
        public DataTable GetDeThiByGiaoVien(string maGV)
        {
            const string sql = @"
        SELECT *
        FROM De_Thi
        WHERE UPPER(LTRIM(RTRIM(ISNULL(Ma_GV, '')))) = UPPER(LTRIM(RTRIM(@MaGV)))
        ORDER BY Ma_De DESC";
            return ExecuteQuery(sql, new SqlParameter("@MaGV", (maGV ?? "").Trim()));
        }
    }
}