using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class ExamConfigDAO
    {
        // ==================== 1. KỲ THI (Ky_Thi) ====================
        public DataTable GetAllKyThi()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT Ma_KT, Ten, Ngay_thi FROM Ky_Thi ORDER BY Ngay_thi DESC");
        }

        public bool IsDuplicateNgayThi(DateTime ngayThi, string? excludeMaKT = null)
        {
            string sql = "SELECT COUNT(*) FROM Ky_Thi WHERE CAST(Ngay_thi AS DATE) = CAST(@NgayThi AS DATE)";
            var p = new List<SqlParameter> { new SqlParameter("@NgayThi", ngayThi.Date) };
            if (!string.IsNullOrEmpty(excludeMaKT))
            {
                sql += " AND Ma_KT <> @MaKT";
                p.Add(new SqlParameter("@MaKT", excludeMaKT));
            }
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sql, p.ToArray())) > 0;
        }

        public bool HasStudentsInKyThi(string maKT)
        {
            string sql = @"SELECT COUNT(*) FROM Bai_Thi bt 
                           JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De 
                           WHERE dt.Ma_KT = @MaKT";
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sql, new[] { new SqlParameter("@MaKT", maKT) })) > 0;
        }

        public bool InsertKyThi(string maKT, string ten, DateTime ngayThi)
        {
            string sql = "INSERT INTO Ky_Thi (Ma_KT, Ten, Ngay_thi) VALUES (@MaKT, @Ten, @NgayThi)";
            return DataProvider.Instance.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@MaKT", maKT),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@NgayThi", ngayThi)
            }) > 0;
        }

        public bool UpdateKyThi(string maKT, string ten, DateTime ngayThi)
        {
            string sql = "UPDATE Ky_Thi SET Ten = @Ten, Ngay_thi = @NgayThi WHERE Ma_KT = @MaKT";
            return DataProvider.Instance.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@MaKT", maKT),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@NgayThi", ngayThi)
            }) > 0;
        }

        public bool DeleteKyThi(string maKT)
        {
            string sql = "DELETE FROM Ky_Thi WHERE Ma_KT = @MaKT";
            return DataProvider.Instance.ExecuteNonQuery(sql, new[] { new SqlParameter("@MaKT", maKT) }) > 0;
        }

        // ==================== 2. MA TRẬN ĐỀ (Ma_Tran) ====================
        public DataTable GetAllMaTran()
        {
            string sql = @"SELECT mt.*, m.Ten_mon 
                           FROM Ma_Tran mt 
                           LEFT JOIN Mon m ON mt.Ma_Mon = m.Ma_Mon";
            return DataProvider.Instance.ExecuteQuery(sql);
        }

        public bool SaveMaTran(string maMT, string maMon, double tyLeCoBan, double tyLeVanDung, double tyLeVDNangCao)
        {
            string sql = @"IF EXISTS (SELECT 1 FROM Ma_Tran WHERE Ma_MT = @MaMT)
                               UPDATE Ma_Tran 
                               SET Ma_Mon = @MaMon, Ty_le_co_ban = @CB, Ty_le_van_dung = @VD, Ty_le_vd_nang_cao = @VDNC 
                               WHERE Ma_MT = @MaMT
                           ELSE
                               INSERT INTO Ma_Tran (Ma_MT, Ma_Mon, Ty_le_co_ban, Ty_le_van_dung, Ty_le_vd_nang_cao) 
                               VALUES (@MaMT, @MaMon, @CB, @VD, @VDNC)";
            return DataProvider.Instance.ExecuteNonQuery(sql, new[]
            {
                new SqlParameter("@MaMT", maMT),
                new SqlParameter("@MaMon", maMon),
                new SqlParameter("@CB", tyLeCoBan),
                new SqlParameter("@VD", tyLeVanDung),
                new SqlParameter("@VDNC", tyLeVDNangCao)
            }) > 0;
        }

        public bool DeleteMaTran(string maMT)
        {
            string sql = "DELETE FROM Ma_Tran WHERE Ma_MT = @MaMT";
            return DataProvider.Instance.ExecuteNonQuery(sql, new[] { new SqlParameter("@MaMT", maMT) }) > 0;
        }

        // ==================== 3. ĐỀ THI & CHI TIẾT ĐỀ (De_Thi, CT_DT) ====================
        public List<string> GetRandomQuestionIds(string maMon, string mucDo, int soLuong)
        {
            var ids = new List<string>();
            if (soLuong <= 0) return ids;
            string sql = "SELECT TOP (@SL) Ma_CH FROM CAU_HOI WHERE Ma_Mon = @MaMon AND Muc_do = @MucDo ORDER BY NEWID()";
            DataTable dt = DataProvider.Instance.ExecuteQuery(sql, new[]
            {
                new SqlParameter("@SL", soLuong),
                new SqlParameter("@MaMon", maMon),
                new SqlParameter("@MucDo", mucDo)
            });
            foreach (DataRow row in dt.Rows)
                ids.Add(row["Ma_CH"]?.ToString() ?? string.Empty);
            return ids;
        }

        public bool InsertDeThiWithDetails(string maDe, string maKT, string maMT, string maMon, string maGV, int soLuongCH, int thoiGianLamBai, List<string> danhSachMaCH)
        {
            string insertDe = @"INSERT INTO De_Thi (Ma_De, Ma_KT, Ma_MT, Ma_Mon, Ma_GV, So_luong_cau_hoi, Thoi_gian_lam_bai) 
                                VALUES (@MaDe, @MaKT, @MaMT, @MaMon, @MaGV, @SL, @TG)";
            bool ok = DataProvider.Instance.ExecuteNonQuery(insertDe, new[]
            {
                new SqlParameter("@MaDe", maDe),
                new SqlParameter("@MaKT", maKT),
                new SqlParameter("@MaMT", maMT),
                new SqlParameter("@MaMon", maMon),
                new SqlParameter("@MaGV", maGV),
                new SqlParameter("@SL", soLuongCH),
                new SqlParameter("@TG", thoiGianLamBai)
            }) > 0;
            if (!ok) return false;

            foreach (string maCH in danhSachMaCH)
            {
                string insertCT = "INSERT INTO CT_DT (Ma_De, Ma_CH) VALUES (@MaDe, @MaCH)";
                DataProvider.Instance.ExecuteNonQuery(insertCT, new[]
                {
                    new SqlParameter("@MaDe", maDe),
                    new SqlParameter("@MaCH", maCH)
                });
            }
            return true;
        }

        public DataTable GetAllDeThi()
        {
            return DataProvider.Instance.ExecuteQuery("SELECT * FROM De_Thi ORDER BY Ma_De DESC");
        }

        public DataTable GetChiTietDeThi(string maDe)
        {
            string sql = @"SELECT ch.Ma_CH, ch.Muc_do, ch.Noi_dung, ch.Dap_an 
                           FROM CT_DT ct 
                           JOIN CAU_HOI ch ON ct.Ma_CH = ch.Ma_CH 
                           WHERE ct.Ma_De = @MaDe";
            return DataProvider.Instance.ExecuteQuery(sql, new[] { new SqlParameter("@MaDe", maDe) });
        }
    }
}