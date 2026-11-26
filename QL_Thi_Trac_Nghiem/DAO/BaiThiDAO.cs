using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class BaiThiDAO
    {
        public List<ThongKeDeThiDTO> LayDanhSachDeThiChoSV(string tenKyThi, string namHoc, string tenMon)
        {
            var list = new List<ThongKeDeThiDTO>();
            const string sql = @"
                SELECT dt.Ma_De,
                       ISNULL(m.Ten_Mon, dt.Ma_Mon) AS Ten_Mon,
                       ISNULL(dt.So_Cau_Hoi, (SELECT COUNT(*) FROM CT_DT ct WHERE ct.Ma_De = dt.Ma_De)) AS So_Cau_Hoi,
                       ISNULL(dt.Thoi_Gian, 45) AS Thoi_Gian,
                       ISNULL(kt.Ten_KT, N'Học kỳ 1') AS Hoc_Ky,
                       ISNULL(kt.Nam_Hoc, '2026-2027') AS Nam_Hoc
                FROM De_Thi dt
                LEFT JOIN Mon m ON dt.Ma_Mon = m.Ma_Mon
                LEFT JOIN Ky_Thi kt ON dt.Ma_KT = kt.Ma_KT
                WHERE (dt.Trang_Thai IS NULL OR dt.Trang_Thai = N'Đã đăng tải' OR dt.Trang_Thai = N'Đã duyệt')
                  AND (@KyThi = '' OR @KyThi = N'Tất cả' OR kt.Ten_KT = @KyThi)
                  AND (@NamHoc = '' OR @NamHoc = N'Tất cả' OR kt.Nam_Hoc = @NamHoc)
                  AND (@TenMon = '' OR @TenMon = N'Tất cả' OR m.Ten_Mon = @TenMon OR dt.Ma_Mon = @TenMon)";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@KyThi", tenKyThi ?? "");
                cmd.Parameters.AddWithValue("@NamHoc", namHoc ?? "");
                cmd.Parameters.AddWithValue("@TenMon", tenMon ?? "");
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ThongKeDeThiDTO
                        {
                            MaDe = rd["Ma_De"].ToString() ?? "",
                            TenMon = rd["Ten_Mon"].ToString() ?? "",
                            SoCauHoi = Convert.ToInt32(rd["So_Cau_Hoi"]),
                            ThoiGian = Convert.ToInt32(rd["Thoi_Gian"]),
                            HocKy = rd["Hoc_Ky"].ToString() ?? "",
                            NamHoc = rd["Nam_Hoc"].ToString() ?? ""
                        });
                    }
                }
            }
            return list;
        }

        public List<CauHoiThiDTO> LayDanhSachCauHoiTheoDe(string maDe)
        {
            var ds = new List<CauHoiThiDTO>();
            const string sql = @"
                SELECT DISTINCT ch.Ma_CH, ch.Noi_Dung, ch.Dap_An_A, ch.Dap_An_B, ch.Dap_An_C, ch.Dap_An_D, ch.Dap_An_Dung
                FROM CAU_HOI ch
                JOIN CT_DT ct ON ch.Ma_CH = ct.Ma_CH
                WHERE ct.Ma_De = @MaDe";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaDe", maDe);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ds.Add(new CauHoiThiDTO
                        {
                            MaCH = rd["Ma_CH"].ToString() ?? "",
                            MaDe = maDe,
                            NoiDung = rd["Noi_Dung"].ToString() ?? "",
                            DapAnA = rd["Dap_An_A"].ToString() ?? "",
                            DapAnB = rd["Dap_An_B"].ToString() ?? "",
                            DapAnC = rd["Dap_An_C"].ToString() ?? "",
                            DapAnD = rd["Dap_An_D"].ToString() ?? "",
                            DapAnDung = (rd["Dap_An_Dung"].ToString() ?? "").Trim().ToUpper()
                        });
                    }
                }
            }
            return ds;
        }

        public string BatDauBaiThi(string maSV, string maDe, DateTime thoiGianBatDau)
        {
            string maBT = "BT" + thoiGianBatDau.ToString("yyMMddHHmmss")
                        + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            const string sql = @"
                INSERT INTO Bai_Thi
                    (Ma_BT, Ma_SV, Ma_De, Ngay_Thi, Thoi_Gian_Bat_Dau, Thoi_Gian_Ket_Thuc, So_Cau_Dung, So_Cau_Sai, Tong_Diem, Trang_Thai)
                VALUES
                    (@MaBT, @MaSV, @MaDe, @NgayThi, @TGBatDau, NULL, 0, 0, 0, N'Đang làm')";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaBT", maBT);
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                cmd.Parameters.AddWithValue("@MaDe", maDe);
                cmd.Parameters.AddWithValue("@NgayThi", thoiGianBatDau);
                cmd.Parameters.AddWithValue("@TGBatDau", thoiGianBatDau);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return maBT;
        }

        public BaiThiDTO? LayBaiThiDangLam(string maSV, string maDe)
        {
            const string sql = @"
                SELECT TOP 1 Ma_BT, Ma_SV, Ma_De, Thoi_Gian_Bat_Dau, Trang_Thai
                FROM Bai_Thi
                WHERE Ma_SV = @MaSV AND Ma_De = @MaDe AND Trang_Thai = N'Đang làm'
                ORDER BY Thoi_Gian_Bat_Dau DESC";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                cmd.Parameters.AddWithValue("@MaDe", maDe);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        return new BaiThiDTO
                        {
                            MaBT = rd["Ma_BT"].ToString() ?? "",
                            MaSV = rd["Ma_SV"].ToString() ?? "",
                            MaDe = rd["Ma_De"].ToString() ?? "",
                            ThoiGianBatDau = rd["Thoi_Gian_Bat_Dau"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(rd["Thoi_Gian_Bat_Dau"]),
                            TrangThai = rd["Trang_Thai"].ToString() ?? "Đang làm"
                        };
                    }
                }
            }
            return null;
        }

        public Dictionary<string, string> LayDapAnDaLuuTam(string maBT)
        {
            var ketQua = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            const string sql = "SELECT Ma_CH, Dap_An_Chon FROM Chi_Tiet_Bai_Thi WHERE Ma_BT = @MaBT";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaBT", maBT);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string maCH = rd["Ma_CH"].ToString() ?? "";
                        string dapAn = rd["Dap_An_Chon"] == DBNull.Value ? "" : rd["Dap_An_Chon"].ToString() ?? "";
                        if (!string.IsNullOrEmpty(maCH))
                            ketQua[maCH] = dapAn.Trim().ToUpper();
                    }
                }
            }
            return ketQua;
        }

        public bool LuuTamDapAn(string maBT, string maCH, string dapAnChon)
        {
            const string sql = @"
                IF EXISTS (SELECT 1 FROM Bai_Thi WHERE Ma_BT = @MaBT AND Trang_Thai = N'Đang làm')
                BEGIN
                    IF EXISTS (SELECT 1 FROM Chi_Tiet_Bai_Thi WHERE Ma_BT = @MaBT AND Ma_CH = @MaCH)
                        UPDATE Chi_Tiet_Bai_Thi
                        SET Dap_An_Chon = @DapAnChon
                        WHERE Ma_BT = @MaBT AND Ma_CH = @MaCH
                    ELSE
                        INSERT INTO Chi_Tiet_Bai_Thi (Ma_BT, Ma_CH, Dap_An_Chon, Ket_Qua)
                        VALUES (@MaBT, @MaCH, @DapAnChon, N'Chưa chấm')
                END";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaBT", maBT);
                cmd.Parameters.AddWithValue("@MaCH", maCH);
                cmd.Parameters.AddWithValue("@DapAnChon", dapAnChon ?? "");
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void LuuTamToanBoBaiLam(string maBT, List<CauHoiThiDTO> dsCauHoi)
        {
            if (string.IsNullOrWhiteSpace(maBT) || dsCauHoi == null || dsCauHoi.Count == 0)
                return;

            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        const string sqlCheck = "SELECT COUNT(1) FROM Bai_Thi WHERE Ma_BT = @MaBT AND Trang_Thai = N'Đang làm'";
                        using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, trans))
                        {
                            cmdCheck.Parameters.AddWithValue("@MaBT", maBT);
                            if (Convert.ToInt32(cmdCheck.ExecuteScalar()) == 0)
                            {
                                trans.Rollback();
                                return;
                            }
                        }

                        const string sqlUpsert = @"
                            IF EXISTS (SELECT 1 FROM Chi_Tiet_Bai_Thi WHERE Ma_BT = @MaBT AND Ma_CH = @MaCH)
                                UPDATE Chi_Tiet_Bai_Thi SET Dap_An_Chon = @DapAnChon WHERE Ma_BT = @MaBT AND Ma_CH = @MaCH
                            ELSE
                                INSERT INTO Chi_Tiet_Bai_Thi (Ma_BT, Ma_CH, Dap_An_Chon, Ket_Qua)
                                VALUES (@MaBT, @MaCH, @DapAnChon, N'Chưa chấm')";

                        foreach (var ch in dsCauHoi)
                        {
                            if (string.IsNullOrWhiteSpace(ch.DapAnDaChon)) continue;

                            using (SqlCommand cmd = new SqlCommand(sqlUpsert, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@MaBT", maBT);
                                cmd.Parameters.AddWithValue("@MaCH", ch.MaCH);
                                cmd.Parameters.AddWithValue("@DapAnChon", ch.DapAnDaChon.Trim().ToUpper());
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool GhiNhanThoiGianKetThuc(string maBT, DateTime thoiGianKetThuc)
        {
            const string sql = @"
                UPDATE Bai_Thi
                SET Thoi_Gian_Ket_Thuc = @TGKetThuc,
                    Trang_Thai = N'Đã nộp'
                WHERE Ma_BT = @MaBT AND Trang_Thai = N'Đang làm'";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@TGKetThuc", thoiGianKetThuc);
                cmd.Parameters.AddWithValue("@MaBT", maBT);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void LuuKetQuaChamDiem(BaiThiDTO baiThi, List<CauHoiThiDTO> dsCauHoi)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        const string sqlUpdateBT = @"
                            UPDATE Bai_Thi
                            SET Thoi_Gian_Ket_Thuc = @TGKetThuc,
                                So_Cau_Dung = @SoCauDung,
                                So_Cau_Sai = @SoCauSai,
                                Tong_Diem = @TongDiem,
                                Trang_Thai = N'Đã nộp'
                            WHERE Ma_BT = @MaBT";
                        using (SqlCommand cmdBT = new SqlCommand(sqlUpdateBT, conn, trans))
                        {
                            cmdBT.Parameters.AddWithValue("@TGKetThuc", baiThi.ThoiGianKetThuc);
                            cmdBT.Parameters.AddWithValue("@SoCauDung", baiThi.SoCauDung);
                            cmdBT.Parameters.AddWithValue("@SoCauSai", baiThi.SoCauSai);
                            cmdBT.Parameters.AddWithValue("@TongDiem", baiThi.TongDiem);
                            cmdBT.Parameters.AddWithValue("@MaBT", baiThi.MaBT);
                            cmdBT.ExecuteNonQuery();
                        }

                        using (SqlCommand cmdDel = new SqlCommand("DELETE FROM Chi_Tiet_Bai_Thi WHERE Ma_BT = @MaBT", conn, trans))
                        {
                            cmdDel.Parameters.AddWithValue("@MaBT", baiThi.MaBT);
                            cmdDel.ExecuteNonQuery();
                        }

                        const string sqlCT = @"
                            INSERT INTO Chi_Tiet_Bai_Thi (Ma_BT, Ma_CH, Ma_De, Dap_An_Chon, Ket_Qua)
                            VALUES (@MaBT, @MaCH, @MaDe, @DapAnChon, @KetQua)";

                        foreach (var ch in dsCauHoi)
                        {
                            bool isCorrect = !string.IsNullOrEmpty(ch.DapAnDaChon) &&
                                             string.Equals(ch.DapAnDaChon.Trim(), ch.DapAnDung.Trim(), StringComparison.OrdinalIgnoreCase);
                            string ketQua = isCorrect ? "Đúng" : "Sai";

                            using (SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans))
                            {
                                cmdCT.Parameters.AddWithValue("@MaBT", baiThi.MaBT);
                                cmdCT.Parameters.AddWithValue("@MaCH", ch.MaCH);
                                cmdCT.Parameters.AddWithValue("@MaDe", baiThi.MaDe);
                                cmdCT.Parameters.AddWithValue("@DapAnChon", ch.DapAnDaChon ?? "");
                                cmdCT.Parameters.AddWithValue("@KetQua", ketQua);
                                cmdCT.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<BaiThiDTO> LayLichSuBaiThiSV(string maSV)
        {
            var list = new List<BaiThiDTO>();
            const string sql = @"
                SELECT bt.Ma_BT, bt.Ma_SV, bt.Ma_De,
                       ISNULL(m.Ten_Mon, dt.Ma_Mon) AS Ten_Mon,
                       ISNULL(bt.Thoi_Gian_Bat_Dau, ISNULL(bt.Ngay_Thi, GETDATE())) AS TGBatDau,
                       ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE())) AS TGKetThuc,
                       ISNULL(bt.So_Cau_Dung, 0) AS So_Cau_Dung,
                       ISNULL(bt.So_Cau_Sai, 0) AS So_Cau_Sai,
                       ISNULL(bt.Tong_Diem, 0) AS Tong_Diem
                FROM Bai_Thi bt
                JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De
                LEFT JOIN Mon m ON dt.Ma_Mon = m.Ma_Mon
                WHERE bt.Ma_SV = @MaSV
                ORDER BY ISNULL(bt.Thoi_Gian_Ket_Thuc, bt.Ngay_Thi) DESC";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BaiThiDTO
                        {
                            MaBT = rd["Ma_BT"].ToString() ?? "",
                            MaSV = rd["Ma_SV"].ToString() ?? "",
                            MaDe = rd["Ma_De"].ToString() ?? "",
                            TenMon = rd["Ten_Mon"].ToString() ?? "",
                            ThoiGianBatDau = Convert.ToDateTime(rd["TGBatDau"]),
                            ThoiGianKetThuc = Convert.ToDateTime(rd["TGKetThuc"]),
                            SoCauDung = Convert.ToInt32(rd["So_Cau_Dung"]),
                            SoCauSai = Convert.ToInt32(rd["So_Cau_Sai"]),
                            TongDiem = Convert.ToDouble(rd["Tong_Diem"])
                        });
                    }
                }
            }
            return list;
        }

        public List<ChiTietBaiLamDTO> LayChiTietBaiLam(string maBT)
        {
            var list = new List<ChiTietBaiLamDTO>();
            const string sql = @"
                SELECT ct.Ma_BT, bt.Ma_De, ct.Ma_CH, ct.Dap_An_Chon, ct.Ket_Qua,
                       ch.Noi_Dung, ch.Dap_An_A, ch.Dap_An_B, ch.Dap_An_C, ch.Dap_An_D, ch.Dap_An_Dung
                FROM Chi_Tiet_Bai_Thi ct
                JOIN Bai_Thi bt ON ct.Ma_BT = bt.Ma_BT
                JOIN CAU_HOI ch ON ct.Ma_CH = ch.Ma_CH
                WHERE ct.Ma_BT = @MaBT";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@MaBT", maBT);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string chon = rd["Dap_An_Chon"].ToString() ?? "";
                        string dung = (rd["Dap_An_Dung"].ToString() ?? "").Trim().ToUpper();
                        string kq = rd["Ket_Qua"].ToString() ?? "";
                        if (string.IsNullOrEmpty(kq))
                            kq = string.Equals(chon, dung, StringComparison.OrdinalIgnoreCase) ? "Đúng" : "Sai";

                        list.Add(new ChiTietBaiLamDTO
                        {
                            MaBT = rd["Ma_BT"].ToString() ?? "",
                            MaDe = rd["Ma_De"].ToString() ?? "",
                            MaCH = rd["Ma_CH"].ToString() ?? "",
                            NoiDungCauHoi = rd["Noi_Dung"].ToString() ?? "",
                            DapAnA = rd["Dap_An_A"].ToString() ?? "",
                            DapAnB = rd["Dap_An_B"].ToString() ?? "",
                            DapAnC = rd["Dap_An_C"].ToString() ?? "",
                            DapAnD = rd["Dap_An_D"].ToString() ?? "",
                            DapAnChon = chon,
                            DapAnDung = dung,
                            KetQua = kq
                        });
                    }
                }
            }
            return list;
        }

        public List<BaoCaoDiemThiDTO> ThongKeDiemThi(string tenKyThi, string tenMon, string maLop)
        {
            var list = new List<BaoCaoDiemThiDTO>();
            const string sql = @"
                SELECT bt.Ma_BT, bt.Ma_SV, (sv.Ho_lot + N' ' + sv.Ten) AS HoTenSV, sv.Lop,
                       bt.Ma_De, ISNULL(m.Ten_Mon, dt.Ma_Mon) AS Ten_Mon,
                       ISNULL(kt.Ten_KT, N'Học kỳ 1') AS Ky_Thi,
                       ISNULL(kt.Nam_Hoc, '2026-2027') AS Nam_Hoc,
                       ISNULL(bt.So_Cau_Dung, 0) AS So_Cau_Dung,
                       ISNULL(bt.So_Cau_Sai, 0) AS So_Cau_Sai,
                       ISNULL(bt.Tong_Diem, 0) AS Tong_Diem,
                       ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE())) AS Ngay_Thi
                FROM Bai_Thi bt
                JOIN Sinh_Vien sv ON bt.Ma_SV = sv.Ma_SV
                JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De
                LEFT JOIN Mon m ON dt.Ma_Mon = m.Ma_Mon
                LEFT JOIN Ky_Thi kt ON dt.Ma_KT = kt.Ma_KT
                WHERE (@KyThi = '' OR @KyThi = N'Tất cả' OR kt.Ten_KT = @KyThi)
                  AND (@TenMon = '' OR @TenMon = N'Tất cả' OR m.Ten_Mon = @TenMon OR dt.Ma_Mon = @TenMon)
                  AND (@MaLop = '' OR @MaLop = N'Tất cả' OR sv.Lop = @MaLop)
                ORDER BY bt.Tong_Diem DESC, sv.Ten ASC";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@KyThi", tenKyThi ?? "");
                cmd.Parameters.AddWithValue("@TenMon", tenMon ?? "");
                cmd.Parameters.AddWithValue("@MaLop", maLop ?? "");
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BaoCaoDiemThiDTO
                        {
                            MaBT = rd["Ma_BT"].ToString() ?? "",
                            MaSV = rd["Ma_SV"].ToString() ?? "",
                            HoTenSV = rd["HoTenSV"].ToString() ?? "",
                            Lop = rd["Lop"].ToString() ?? "",
                            MaDe = rd["Ma_De"].ToString() ?? "",
                            TenMon = rd["Ten_Mon"].ToString() ?? "",
                            KyThi = rd["Ky_Thi"].ToString() ?? "",
                            NamHoc = rd["Nam_Hoc"].ToString() ?? "",
                            SoCauDung = Convert.ToInt32(rd["So_Cau_Dung"]),
                            SoCauSai = Convert.ToInt32(rd["So_Cau_Sai"]),
                            TongDiem = Math.Round(Convert.ToDouble(rd["Tong_Diem"]), 2),
                            NgayThi = Convert.ToDateTime(rd["Ngay_Thi"])
                        });
                    }
                }
            }
            return list;
        }
      // Mục 10a: Lọc điểm thi theo Từ ngày -> Đến ngày / Tháng / Quý / Năm
        public List<BaoCaoDiemThiDTO> ThongKeDiemThiNangCao(
            string tenKyThi,
            string tenMon,
            string maLop,
            DateTime? tuNgay,
            DateTime? denNgay,
            int? thang,
            int? quy,
            int? nam)
        {
            var list = new List<BaoCaoDiemThiDTO>();
            const string sql = @"
                SELECT bt.Ma_BT, bt.Ma_SV, (sv.Ho_lot + N' ' + sv.Ten) AS HoTenSV, sv.Lop,
                       bt.Ma_De, ISNULL(m.Ten_Mon, dt.Ma_Mon) AS Ten_Mon,
                       ISNULL(kt.Ten_KT, N'Học kỳ 1') AS Ky_Thi,
                       ISNULL(kt.Nam_Hoc, '2026-2027') AS Nam_Hoc,
                       ISNULL(bt.So_Cau_Dung, 0) AS So_Cau_Dung,
                       ISNULL(bt.So_Cau_Sai, 0) AS So_Cau_Sai,
                       ISNULL(bt.Tong_Diem, 0) AS Tong_Diem,
                       ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE())) AS Ngay_Thi
                FROM Bai_Thi bt
                JOIN Sinh_Vien sv ON bt.Ma_SV = sv.Ma_SV
                JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De
                LEFT JOIN Mon m ON dt.Ma_Mon = m.Ma_Mon
                LEFT JOIN Ky_Thi kt ON dt.Ma_KT = kt.Ma_KT
                WHERE (@KyThi = '' OR @KyThi = N'Tất cả' OR kt.Ten_KT = @KyThi)
                  AND (@TenMon = '' OR @TenMon = N'Tất cả' OR m.Ten_Mon = @TenMon OR dt.Ma_Mon = @TenMon)
                  AND (@MaLop = '' OR @MaLop = N'Tất cả' OR sv.Lop = @MaLop)
                  AND (@TuNgay IS NULL OR CAST(ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE())) AS DATE) >= CAST(@TuNgay AS DATE))
                  AND (@DenNgay IS NULL OR CAST(ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE())) AS DATE) <= CAST(@DenNgay AS DATE))
                  AND (@Thang IS NULL OR MONTH(ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = @Thang)
                  AND (@Quy IS NULL OR DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = @Quy)
                  AND (@Nam IS NULL OR YEAR(ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = @Nam)
                ORDER BY bt.Tong_Diem DESC, sv.Ten ASC";

            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@KyThi", tenKyThi ?? "");
                cmd.Parameters.AddWithValue("@TenMon", tenMon ?? "");
                cmd.Parameters.AddWithValue("@MaLop", maLop ?? "");
                cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime).Value = (object?)tuNgay ?? DBNull.Value;
                cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime).Value = (object?)denNgay ?? DBNull.Value;
                cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = (object?)thang ?? DBNull.Value;
                cmd.Parameters.Add("@Quy", SqlDbType.Int).Value = (object?)quy ?? DBNull.Value;
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = (object?)nam ?? DBNull.Value;

                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BaoCaoDiemThiDTO
                        {
                            MaBT = rd["Ma_BT"].ToString() ?? "",
                            MaSV = rd["Ma_SV"].ToString() ?? "",
                            HoTenSV = rd["HoTenSV"].ToString() ?? "",
                            Lop = rd["Lop"].ToString() ?? "",
                            MaDe = rd["Ma_De"].ToString() ?? "",
                            TenMon = rd["Ten_Mon"].ToString() ?? "",
                            KyThi = rd["Ky_Thi"].ToString() ?? "",
                            NamHoc = rd["Nam_Hoc"].ToString() ?? "",
                            SoCauDung = Convert.ToInt32(rd["So_Cau_Dung"]),
                            SoCauSai = Convert.ToInt32(rd["So_Cau_Sai"]),
                            TongDiem = Math.Round(Convert.ToDouble(rd["Tong_Diem"]), 2),
                            NgayThi = Convert.ToDateTime(rd["Ngay_Thi"])
                        });
                    }
                }
            }
            return list;
        }

        // Mục 10 - Bảng 1: Thống kê theo Môn học & Quý (Q1, Q2, Q3, Q4, TC)
        public List<ThongKeTheoQuyDTO> ThongKeTheoMonVaQuy(int nam, bool tinhDiemTrungBinh)
        {
            string bieuThucGiaTri = tinhDiemTrungBinh
                ? @"ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 1 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q1,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 2 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q2,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 3 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q3,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 4 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q4,
                    ROUND(ISNULL(AVG(CAST(bt.Tong_Diem AS FLOAT)), 0), 2) AS TC"
                : @"SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 1 THEN 1 ELSE 0 END) AS Q1,
                    SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 2 THEN 1 ELSE 0 END) AS Q2,
                    SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 3 THEN 1 ELSE 0 END) AS Q3,
                    SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 4 THEN 1 ELSE 0 END) AS Q4,
                    COUNT(*) AS TC";

            string sql = $@"
                SELECT ISNULL(m.Ten_Mon, dt.Ma_Mon) AS TenDoiTuong,
                       {bieuThucGiaTri}
                FROM Bai_Thi bt
                JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De
                LEFT JOIN Mon m ON dt.Ma_Mon = m.Ma_Mon
                WHERE YEAR(ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = @Nam
                GROUP BY ISNULL(m.Ten_Mon, dt.Ma_Mon)
                ORDER BY TenDoiTuong";

            return DocBangThongKeQuy(sql, nam);
        }

        // Mục 10 - Bảng 2: Thống kê theo Lớp sinh viên & Quý (Q1, Q2, Q3, Q4, TC)
        public List<ThongKeTheoQuyDTO> ThongKeTheoLopVaQuy(int nam, bool tinhDiemTrungBinh)
        {
            string bieuThucGiaTri = tinhDiemTrungBinh
                ? @"ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 1 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q1,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 2 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q2,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 3 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q3,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 4 THEN CAST(bt.Tong_Diem AS FLOAT) END), 0), 2) AS Q4,
                    ROUND(ISNULL(AVG(CAST(bt.Tong_Diem AS FLOAT)), 0), 2) AS TC"
                : @"SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 1 THEN 1 ELSE 0 END) AS Q1,
                    SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 2 THEN 1 ELSE 0 END) AS Q2,
                    SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 3 THEN 1 ELSE 0 END) AS Q3,
                    SUM(CASE WHEN DATEPART(QUARTER, ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = 4 THEN 1 ELSE 0 END) AS Q4,
                    COUNT(*) AS TC";

            string sql = $@"
                SELECT ISNULL(sv.Lop, N'Chưa phân lớp') AS TenDoiTuong,
                       {bieuThucGiaTri}
                FROM Bai_Thi bt
                JOIN Sinh_Vien sv ON bt.Ma_SV = sv.Ma_SV
                WHERE YEAR(ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = @Nam
                GROUP BY ISNULL(sv.Lop, N'Chưa phân lớp')
                ORDER BY TenDoiTuong";

            return DocBangThongKeQuy(sql, nam);
        }

        // Mục 10 - Bảng 3: Thống kê theo Giảng viên & Quý (Q1, Q2, Q3, Q4, TC)
        public List<ThongKeTheoQuyDTO> ThongKeTheoGiangVienVaQuy(int nam, bool tinhDiemTrungBinh)
        {
            string bieuThucGiaTri = tinhDiemTrungBinh
                ? @"ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 1 THEN CAST(t.Tong_Diem AS FLOAT) END), 0), 2) AS Q1,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 2 THEN CAST(t.Tong_Diem AS FLOAT) END), 0), 2) AS Q2,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 3 THEN CAST(t.Tong_Diem AS FLOAT) END), 0), 2) AS Q3,
                    ROUND(ISNULL(AVG(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 4 THEN CAST(t.Tong_Diem AS FLOAT) END), 0), 2) AS Q4,
                    ROUND(ISNULL(AVG(CAST(t.Tong_Diem AS FLOAT)), 0), 2) AS TC"
                : @"SUM(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 1 THEN 1 ELSE 0 END) AS Q1,
                    SUM(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 2 THEN 1 ELSE 0 END) AS Q2,
                    SUM(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 3 THEN 1 ELSE 0 END) AS Q3,
                    SUM(CASE WHEN DATEPART(QUARTER, t.NgayTinh) = 4 THEN 1 ELSE 0 END) AS Q4,
                    COUNT(*) AS TC";

            string sql = $@"
                SELECT t.TenDoiTuong,
                       {bieuThucGiaTri}
                FROM (
                    SELECT CASE
                               WHEN gv.Ma_GV IS NOT NULL THEN gv.Ma_GV + N' - ' + LTRIM(RTRIM(ISNULL(gv.Ho_lot, N'') + N' ' + ISNULL(gv.Ten, N'')))
                               WHEN NULLIF(LTRIM(RTRIM(dt.Ma_GV)), '') IS NOT NULL THEN dt.Ma_GV
                               ELSE N'Chưa phân công GV'
                           END AS TenDoiTuong,
                           bt.Tong_Diem,
                           ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE())) AS NgayTinh
                    FROM Bai_Thi bt
                    JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De
                    LEFT JOIN Giang_Vien gv ON dt.Ma_GV = gv.Ma_GV
                    WHERE YEAR(ISNULL(bt.Thoi_Gian_Ket_Thuc, ISNULL(bt.Ngay_Thi, GETDATE()))) = @Nam
                ) t
                GROUP BY t.TenDoiTuong
                ORDER BY t.TenDoiTuong";

            return DocBangThongKeQuy(sql, nam);
        }

        private List<ThongKeTheoQuyDTO> DocBangThongKeQuy(string sql, int nam)
        {
            var list = new List<ThongKeTheoQuyDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = nam;
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ThongKeTheoQuyDTO
                        {
                            TenDoiTuong = rd["TenDoiTuong"].ToString() ?? "",
                            Q1 = Convert.ToDouble(rd["Q1"]),
                            Q2 = Convert.ToDouble(rd["Q2"]),
                            Q3 = Convert.ToDouble(rd["Q3"]),
                            Q4 = Convert.ToDouble(rd["Q4"]),
                            TC = Convert.ToDouble(rd["TC"])
                        });
                    }
                }
            }
            return list;
        }
    }
}
