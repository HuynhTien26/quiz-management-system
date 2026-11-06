using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class AdminDAO
    {
        // ================= PHẦN GIÁO VIÊN =================
        public List<GiangVienDTO> DocDSGV()
        {
            List<GiangVienDTO> ds = new List<GiangVienDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT Ma_GV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Ma_Mon FROM Giang_Vien";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ma = reader["Ma_GV"].ToString() ?? "";
                        string ho = reader["Ho_lot"].ToString() ?? "";
                        string ten = reader["Ten"].ToString() ?? "";
                        DateTime ns = reader.IsDBNull(3) ? DateTime.Now : reader.GetDateTime(3);
                        string phai = reader["Gioi_tinh"].ToString() ?? "";
                        string khoa = reader["Ma_Khoa"].ToString() ?? "";
                        string mon = reader["Ma_Mon"].ToString() ?? "";

                        ds.Add(new GiangVienDTO(ma, ho, ten, ns, phai, khoa, mon));
                    }
                }
            }
            return ds;
        }

        public void ThemGiaoVien(GiangVienDTO gv)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Giang_Vien (Ma_GV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Ma_Mon) 
                                 VALUES (@MaGV, @HoLot, @Ten, @NgaySinh, @GioiTinh, @MaKhoa, @MaMon)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGV", gv.MaGV);
                cmd.Parameters.AddWithValue("@HoLot", gv.HoLot);
                cmd.Parameters.AddWithValue("@Ten", gv.Ten);
                cmd.Parameters.AddWithValue("@NgaySinh", gv.NgaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", gv.GioiTinh);
                cmd.Parameters.AddWithValue("@MaKhoa", gv.MaKhoa);
                cmd.Parameters.AddWithValue("@MaMon", gv.MaMon ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        public void CapNhatGiaoVien(string maCu, GiangVienDTO gv)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE Giang_Vien 
                                 SET Ma_GV = @MaGV, Ho_lot = @HoLot, Ten = @Ten, Ngay_sinh = @NgaySinh, 
                                     Gioi_tinh = @GioiTinh, Ma_Khoa = @MaKhoa, Ma_Mon = @MaMon 
                                 WHERE Ma_GV = @MaCu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGV", gv.MaGV);
                cmd.Parameters.AddWithValue("@HoLot", gv.HoLot);
                cmd.Parameters.AddWithValue("@Ten", gv.Ten);
                cmd.Parameters.AddWithValue("@NgaySinh", gv.NgaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", gv.GioiTinh);
                cmd.Parameters.AddWithValue("@MaKhoa", gv.MaKhoa);
                cmd.Parameters.AddWithValue("@MaMon", gv.MaMon ?? "");
                cmd.Parameters.AddWithValue("@MaCu", maCu);
                cmd.ExecuteNonQuery();
            }
        }

        public void XoaGiaoVien(string maGV)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Giang_Vien WHERE Ma_GV = @MaGV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGV", maGV);
                cmd.ExecuteNonQuery();
            }
        }

        // ================= PHẦN SINH VIÊN =================
        public List<SinhVienDTO> DocDSSV()
        {
            List<SinhVienDTO> ds = new List<SinhVienDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = "SELECT Ma_SV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Lop FROM Sinh_Vien";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ma = reader["Ma_SV"].ToString() ?? "";
                        string ho = reader["Ho_lot"].ToString() ?? "";
                        string ten = reader["Ten"].ToString() ?? "";
                        DateTime ns = reader.IsDBNull(3) ? DateTime.Now : reader.GetDateTime(3);
                        string phai = reader["Gioi_tinh"].ToString() ?? "";
                        string khoa = reader["Ma_Khoa"].ToString() ?? "";
                        string lop = reader["Lop"].ToString() ?? "";

                        ds.Add(new SinhVienDTO(ma, ho, ten, ns, phai, khoa, lop));
                    }
                }
            }
            return ds;
        }

        public void ThemSinhVien(SinhVienDTO sv)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = @"INSERT INTO Sinh_Vien (Ma_SV, Ho_lot, Ten, Ngay_sinh, Gioi_tinh, Ma_Khoa, Lop) 
                                 VALUES (@MaSV, @HoLot, @Ten, @NgaySinh, @GioiTinh, @MaKhoa, @Lop)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
                cmd.Parameters.AddWithValue("@HoLot", sv.HoLot);
                cmd.Parameters.AddWithValue("@Ten", sv.Ten);
                cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
                cmd.Parameters.AddWithValue("@MaKhoa", sv.MaKhoa);
                cmd.Parameters.AddWithValue("@Lop", sv.Lop ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        public void CapNhatSinhVien(string maCu, SinhVienDTO sv)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE Sinh_Vien 
                                 SET Ma_SV = @MaSV, Ho_lot = @HoLot, Ten = @Ten, Ngay_sinh = @NgaySinh, 
                                     Gioi_tinh = @GioiTinh, Ma_Khoa = @MaKhoa, Lop = @Lop 
                                 WHERE Ma_SV = @MaCu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
                cmd.Parameters.AddWithValue("@HoLot", sv.HoLot);
                cmd.Parameters.AddWithValue("@Ten", sv.Ten);
                cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
                cmd.Parameters.AddWithValue("@MaKhoa", sv.MaKhoa);
                cmd.Parameters.AddWithValue("@Lop", sv.Lop ?? "");
                cmd.Parameters.AddWithValue("@MaCu", maCu);
                cmd.ExecuteNonQuery();
            }
        }

        public void XoaSinhVien(string maSV)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Sinh_Vien WHERE Ma_SV = @MaSV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                cmd.ExecuteNonQuery();
            }
        }
        public bool KiemTraTrungMaTrongSQL(string ma)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    SELECT COUNT(1) FROM (
                        SELECT Ma_GV AS Ma FROM Giang_Vien WHERE Ma_GV = @Ma
                        UNION ALL
                        SELECT Ma_SV AS Ma FROM Sinh_Vien WHERE Ma_SV = @Ma
                    ) AS BangGop";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", ma.Trim());
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        // HỌC KỲ ======================
        public List<string> GetDanhSachHocKy()
        {
            List<string> list = new List<string>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT DISTINCT Ten_KT FROM Ky_Thi WHERE Ten_KT IS NOT NULL ORDER BY Ten_KT";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string hk = rd["Ten_KT"].ToString() ?? "";
                        if (!string.IsNullOrWhiteSpace(hk)) list.Add(hk);
                    }
                }
            }
            return list;
        }

        // Lấy danh sách Năm học (20xy-20yz) động từ bảng Ky_Thi trong SQL Server
        public List<string> GetDanhSachNamHoc()
        {
            List<string> list = new List<string>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT DISTINCT Nam_Hoc FROM Ky_Thi WHERE Nam_Hoc IS NOT NULL ORDER BY Nam_Hoc";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string nh = rd["Nam_Hoc"].ToString() ?? "";
                        if (!string.IsNullOrWhiteSpace(nh)) list.Add(nh);
                    }
                }
            }
            return list;
        }
        // TIÉN ĐỘ HỌC TẬP=====================================

        public List<TienDoHocTapDTO> LayTienDoSinhVien(string maSV)
        {
            List<TienDoHocTapDTO> list = new List<TienDoHocTapDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    SELECT m.Ma_Mon, m.Ten_Mon, ISNULL(m.Tin_Chi, 3) AS Tin_Chi, 
                           ISNULL(bt.Tong_Diem, 0) AS Tong_Diem,
                           ISNULL(kt.Ten_KT, N'Học kỳ 1') AS Hoc_Ky,
                           ISNULL(kt.Nam_Hoc, 
                               CAST(YEAR(ISNULL(kt.Ngay_Thi, ISNULL(bt.Ngay_Thi, GETDATE()))) - 1 AS VARCHAR(4)) + '-' + 
                               CAST(YEAR(ISNULL(kt.Ngay_Thi, ISNULL(bt.Ngay_Thi, GETDATE()))) AS VARCHAR(4))
                           ) AS Nam_Hoc
                    FROM Bai_Thi bt
                    JOIN De_Thi dt ON bt.Ma_De = dt.Ma_De
                    JOIN Mon m ON dt.Ma_Mon = m.Ma_Mon
                    LEFT JOIN Ky_Thi kt ON dt.Ma_KT = kt.Ma_KT
                    WHERE bt.Ma_SV = @MaSV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSV", maSV);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new TienDoHocTapDTO
                        {
                            MaMon = rd["Ma_Mon"].ToString() ?? "",
                            TenMon = rd["Ten_Mon"].ToString() ?? "",
                            TinChi = Convert.ToInt32(rd["Tin_Chi"]),
                            DiemThi = Convert.ToDouble(rd["Tong_Diem"]),
                            HocKy = rd["Hoc_Ky"].ToString() ?? "",
                            NamHoc = rd["Nam_Hoc"].ToString() ?? ""
                        });
                    }
                }
            }
            return list;
        }
        // THỐNG KÊ ĐỀ THI =======================================================
        public List<ThongKeDeThiDTO> LayThongKeGiaoVien(string maGV)
        {
            List<ThongKeDeThiDTO> list = new List<ThongKeDeThiDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    SELECT dt.Ma_De, ISNULL(m.Ten_Mon, dt.Ma_Mon) AS Ten_Mon, 
                           ISNULL(dt.So_Cau_Hoi, 0) AS So_Cau_Hoi, 
                           ISNULL(dt.Thoi_Gian, 0) AS Thoi_Gian,
                           ISNULL(kt.Ten_KT, N'Học kỳ 1') AS Hoc_Ky,
                           ISNULL(kt.Nam_Hoc, 
                               CAST(YEAR(ISNULL(kt.Ngay_Thi, GETDATE())) - 1 AS VARCHAR(4)) + '-' + 
                               CAST(YEAR(ISNULL(kt.Ngay_Thi, GETDATE())) AS VARCHAR(4))
                           ) AS Nam_Hoc
                    FROM De_Thi dt
                    LEFT JOIN Mon m ON dt.Ma_Mon = m.Ma_Mon
                    LEFT JOIN Ky_Thi kt ON dt.Ma_KT = kt.Ma_KT
                    WHERE dt.Ma_GV = @MaGV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaGV", maGV);
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
        // =====================================================================
        // 1. QUẢN LÝ ĐỘNG: KHOA (XEM, THÊM, SỬA, XÓA, KIỂM TRA TRÙNG MÃ)
        // =====================================================================
        public List<KhoaDTO> GetAllKhoa()
        {
            List<KhoaDTO> list = new List<KhoaDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT Ma_Khoa, Ten_Khoa FROM Khoa ORDER BY Ma_Khoa";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new KhoaDTO
                        {
                            MaKhoa = rd["Ma_Khoa"].ToString() ?? "",
                            TenKhoa = rd["Ten_Khoa"].ToString() ?? ""
                        });
                    }
                }
            }
            return list;
        }

        public bool CheckTonTaiKhoa(string maKhoa)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT COUNT(1) FROM Khoa WHERE Ma_Khoa = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maKhoa);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool InsertKhoa(KhoaDTO k)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) VALUES (@Ma, @Ten)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", k.MaKhoa);
                cmd.Parameters.AddWithValue("@Ten", k.TenKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateKhoa(KhoaDTO k)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "UPDATE Khoa SET Ten_Khoa = @Ten WHERE Ma_Khoa = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", k.MaKhoa);
                cmd.Parameters.AddWithValue("@Ten", k.TenKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteKhoa(string maKhoa)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "DELETE FROM Khoa WHERE Ma_Khoa = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // =====================================================================
        // 2. QUẢN LÝ ĐỘNG: MÔN HỌC (XEM, THÊM, SỬA, XÓA, KIỂM TRA TRÙNG MÃ)
        // =====================================================================
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

        public bool CheckTonTaiMon(string maMon)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT COUNT(1) FROM Mon WHERE Ma_Mon = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maMon);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool InsertMon(MonDTO m)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "INSERT INTO Mon (Ma_Mon, Ten_Mon, Tin_Chi, Ma_Khoa) VALUES (@Ma, @Ten, @TC, @MaKhoa)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", m.MaMon);
                cmd.Parameters.AddWithValue("@Ten", m.TenMon);
                cmd.Parameters.AddWithValue("@TC", m.TinChi);
                cmd.Parameters.AddWithValue("@MaKhoa", string.IsNullOrEmpty(m.MaKhoa) ? DBNull.Value : m.MaKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateMon(MonDTO m)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "UPDATE Mon SET Ten_Mon = @Ten, Tin_Chi = @TC, Ma_Khoa = @MaKhoa WHERE Ma_Mon = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", m.MaMon);
                cmd.Parameters.AddWithValue("@Ten", m.TenMon);
                cmd.Parameters.AddWithValue("@TC", m.TinChi);
                cmd.Parameters.AddWithValue("@MaKhoa", string.IsNullOrEmpty(m.MaKhoa) ? DBNull.Value : m.MaKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

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

        // =====================================================================
        // 3. QUẢN LÝ ĐỘNG: LỚP HỌC (XEM, THÊM, SỬA, XÓA, KIỂM TRA TRÙNG MÃ)
        // =====================================================================
        public List<LopDTO> GetAllLop()
        {
            List<LopDTO> list = new List<LopDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"SELECT l.Ma_Lop, l.Ten_Lop, ISNULL(l.Ma_Khoa, '') AS Ma_Khoa, ISNULL(k.Ten_Khoa, N'') AS Ten_Khoa
                               FROM Lop l LEFT JOIN Khoa k ON l.Ma_Khoa = k.Ma_Khoa
                               ORDER BY l.Ma_Lop";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new LopDTO
                        {
                            MaLop = rd["Ma_Lop"].ToString() ?? "",
                            TenLop = rd["Ten_Lop"].ToString() ?? "",
                            MaKhoa = rd["Ma_Khoa"].ToString() ?? "",
                            TenKhoa = rd["Ten_Khoa"].ToString() ?? ""
                        });
                    }
                }
            }
            return list;
        }

        public bool CheckTonTaiLop(string maLop)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "SELECT COUNT(1) FROM Lop WHERE Ma_Lop = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maLop);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public bool InsertLop(LopDTO l)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "INSERT INTO Lop (Ma_Lop, Ten_Lop, Ma_Khoa) VALUES (@Ma, @Ten, @MaKhoa)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", l.MaLop);
                cmd.Parameters.AddWithValue("@Ten", l.TenLop);
                cmd.Parameters.AddWithValue("@MaKhoa", string.IsNullOrEmpty(l.MaKhoa) ? DBNull.Value : l.MaKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateLop(LopDTO l)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "UPDATE Lop SET Ten_Lop = @Ten, Ma_Khoa = @MaKhoa WHERE Ma_Lop = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", l.MaLop);
                cmd.Parameters.AddWithValue("@Ten", l.TenLop);
                cmd.Parameters.AddWithValue("@MaKhoa", string.IsNullOrEmpty(l.MaKhoa) ? DBNull.Value : l.MaKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteLop(string maLop)
        {
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = "DELETE FROM Lop WHERE Ma_Lop = @Ma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ma", maLop);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // =====================================================================
        // 4. THỐNG KÊ THEO QUÝ Q1, Q2, Q3, Q4 & TỪ NGÀY ĐẾN NGÀY (Mục 10 - Tuần 7)
        // =====================================================================
        // Bảng thống kê chéo 1: Số lượt làm bài thi theo Môn học và Quý trong Năm
        public List<ThongKeTheoQuyDTO> ThongKeBaiThiTheoMonVaQuy(int nam)
        {
            List<ThongKeTheoQuyDTO> list = new List<ThongKeTheoQuyDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    SELECT m.Ma_Mon, m.Ten_Mon,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 1 THEN 1 ELSE 0 END) AS Q1,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 2 THEN 1 ELSE 0 END) AS Q2,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 3 THEN 1 ELSE 0 END) AS Q3,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 4 THEN 1 ELSE 0 END) AS Q4
                    FROM Mon m
                    LEFT JOIN De_Thi dt ON m.Ma_Mon = dt.Ma_Mon
                    LEFT JOIN Bai_Thi bt ON dt.Ma_De = bt.Ma_De AND YEAR(bt.Ngay_Thi) = @Nam
                    GROUP BY m.Ma_Mon, m.Ten_Mon";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nam", nam);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ThongKeTheoQuyDTO
                        {
                            MaDoiTuong = rd["Ma_Mon"].ToString() ?? "",
                            TenDoiTuong = rd["Ten_Mon"].ToString() ?? "",
                            Q1 = Convert.ToInt32(rd["Q1"]),
                            Q2 = Convert.ToInt32(rd["Q2"]),
                            Q3 = Convert.ToInt32(rd["Q3"]),
                            Q4 = Convert.ToInt32(rd["Q4"])
                        });
                    }
                }
            }
            return list;
        }

        // Bảng thống kê chéo 2: Số đề thi/lượt thi theo Giảng viên và Quý trong Năm
        public List<ThongKeTheoQuyDTO> ThongKeTheoGiangVienVaQuy(int nam)
        {
            List<ThongKeTheoQuyDTO> list = new List<ThongKeTheoQuyDTO>();
            using (SqlConnection conn = MyConnection.GetConnection())
            {
                string sql = @"
                    SELECT gv.Ma_GV, (gv.Ho_Lot + N' ' + gv.Ten) AS HoTenGV,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 1 THEN 1 ELSE 0 END) AS Q1,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 2 THEN 1 ELSE 0 END) AS Q2,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 3 THEN 1 ELSE 0 END) AS Q3,
                        SUM(CASE WHEN DATEPART(QUARTER, bt.Ngay_Thi) = 4 THEN 1 ELSE 0 END) AS Q4
                    FROM Giang_Vien gv
                    LEFT JOIN De_Thi dt ON gv.Ma_GV = dt.Ma_GV
                    LEFT JOIN Bai_Thi bt ON dt.Ma_De = bt.Ma_De AND YEAR(bt.Ngay_Thi) = @Nam
                    GROUP BY gv.Ma_GV, gv.Ho_Lot, gv.Ten";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nam", nam);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new ThongKeTheoQuyDTO
                        {
                            MaDoiTuong = rd["Ma_GV"].ToString() ?? "",
                            TenDoiTuong = rd["HoTenGV"].ToString() ?? "",
                            Q1 = Convert.ToInt32(rd["Q1"]),
                            Q2 = Convert.ToInt32(rd["Q2"]),
                            Q3 = Convert.ToInt32(rd["Q3"]),
                            Q4 = Convert.ToInt32(rd["Q4"])
                        });
                    }
                }
            }
            return list;
        }

    }
}