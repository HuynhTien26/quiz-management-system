using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    public class AdminBUS
    {
        public static List<GiangVienDTO>? dsgv;
        public static List<SinhVienDTO>? dssv;

        public static ArrayList arrKhoa = new ArrayList();
        public static ArrayList arrLop = new ArrayList();
        public static ArrayList arrMon = new ArrayList();

        private readonly AdminDAO dao = new AdminDAO();

        // ================= PHẦN QUẢN LÝ KHOA, MÔN, LỚP, KỲ THI ĐỘNG TỪ SQL =================
        public List<KhoaDTO> DocDSKhoa()
        {
            List<KhoaDTO> list = dao.GetAllKhoa();
            arrKhoa = new ArrayList(list);
            return list;
        }

        public List<LopDTO> DocDSLop()
        {
            List<LopDTO> list = dao.GetAllLop();
            arrLop = new ArrayList(list);
            return list;
        }

        public List<MonDTO> DocDSMon()
        {
            List<MonDTO> list = dao.GetAllMon();
            arrMon = new ArrayList(list);
            return list;
        }

        public List<string> DocDSHocKy() => dao.GetDanhSachHocKy();

        public List<string> DocDSNamHoc() => dao.GetDanhSachNamHoc();

        public List<LopDTO> TimKiemLopTrenArrayList(string tuKhoa, string maKhoaFilter = "")
        {
            if (arrLop == null || arrLop.Count == 0) DocDSLop();
            List<LopDTO> ketQua = new List<LopDTO>();
            tuKhoa = (tuKhoa ?? "").Trim().ToLower();

            foreach (object item in arrLop!)
            {
                if (item is LopDTO l)
                {
                    bool khopTuKhoa = string.IsNullOrEmpty(tuKhoa) ||
                                      l.MaLop.ToLower().Contains(tuKhoa) ||
                                      l.TenLop.ToLower().Contains(tuKhoa);
                    bool khopKhoa = string.IsNullOrEmpty(maKhoaFilter) ||
                                    l.MaKhoa.Equals(maKhoaFilter, StringComparison.OrdinalIgnoreCase);

                    if (khopTuKhoa && khopKhoa)
                        ketQua.Add(l);
                }
            }
            return ketQua;
        }

        public List<KhoaDTO> LayDanhSachKhoa() => dao.GetAllKhoa();
        public List<MonDTO> LayDanhSachMon() => dao.GetAllMon();
        public List<LopDTO> LayDanhSachLop() => dao.GetAllLop();

        

        // ================= PHẦN GIÁO VIÊN =================
        public List<GiangVienDTO> DocDSGV()
        {
            if (dsgv == null || dsgv.Count == 0) dsgv = dao.DocDSGV();
            return dsgv;
        }

        public void ThemGiaoVien(GiangVienDTO gv)
        {
            dao.ThemGiaoVien(gv);
            dsgv = dao.DocDSGV();
        }

        public void CapNhatGiaoVien(string maCu, GiangVienDTO gvMoi)
        {
            dao.CapNhatGiaoVien(maCu, gvMoi);
            dsgv = dao.DocDSGV();
        }

        public void XoaGiaoVien(string maGV)
        {
            dao.XoaGiaoVien(maGV);
            dsgv = dao.DocDSGV();
        }

        public bool KiemTraTrungMaGV(string maGV)
        {
            if (string.IsNullOrWhiteSpace(maGV)) return false;
            string ma = maGV.Trim();
            var listGV = DocDSGV();
            var listSV = DocDSSV();
            return listGV.Exists(x => x.MaGV.Trim().Equals(ma, StringComparison.OrdinalIgnoreCase)) ||
                   listSV.Exists(x => x.MaSV.Trim().Equals(ma, StringComparison.OrdinalIgnoreCase)) ||
                   dao.KiemTraTrungMaTrongSQL(ma);
        }

        public List<GiangVienDTO> TimKiemGV(string tuKhoa)
        {
            var listGV = DocDSGV();
            string key = (tuKhoa ?? "").Trim();
            return listGV.FindAll(gv =>
                gv.MaGV.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                gv.Ten.Contains(key, StringComparison.OrdinalIgnoreCase));
        }

        public List<GiangVienDTO> LocGiaoVien(string ma, string ho, string ten, string gioiTinh, string khoa)
        {
            var listGV = DocDSGV();
            return listGV.FindAll(gv =>
                (string.IsNullOrEmpty(ma) || gv.MaGV.ToLower().Contains(ma.ToLower())) &&
                (string.IsNullOrEmpty(ho) || gv.HoLot.ToLower().Contains(ho.ToLower())) &&
                (string.IsNullOrEmpty(ten) || gv.Ten.ToLower().Contains(ten.ToLower())) &&
                (string.IsNullOrEmpty(gioiTinh) || gv.GioiTinh == gioiTinh) &&
                (string.IsNullOrEmpty(khoa) || gv.MaKhoa == khoa)
            );
        }

        public List<ThongKeDeThiDTO> LayThongKeGiaoVien(string maGV, string hocKy = "Tất cả", string namHoc = "Tất cả")
        {
            List<ThongKeDeThiDTO> ds = dao.LayThongKeGiaoVien(maGV);
            return ds.FindAll(x =>
                (string.IsNullOrEmpty(hocKy) || hocKy == "Tất cả" || x.HocKy.Equals(hocKy, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(namHoc) || namHoc == "Tất cả" || x.NamHoc.Equals(namHoc, StringComparison.OrdinalIgnoreCase))
            );
        }
        public int DemSoCauHoiDongGopCuaGV(string maGV, string hocKy = "Tất cả", string namHoc = "Tất cả")
        {
            if (string.IsNullOrWhiteSpace(maGV)) return 0;
            return dao.DemSoCauHoiDongGopCuaGV(maGV.Trim(), hocKy ?? "Tất cả", namHoc ?? "Tất cả");
        }
        // ================= PHẦN SINH VIÊN =================
        public List<SinhVienDTO> DocDSSV()
        {
            if (dssv == null || dssv.Count == 0) dssv = dao.DocDSSV();
            return dssv;
        }

        public void ThemSinhVien(SinhVienDTO sv)
        {
            dao.ThemSinhVien(sv);
            dssv = dao.DocDSSV();
        }

        public void CapNhatSinhVien(string maCu, SinhVienDTO svMoi)
        {
            dao.CapNhatSinhVien(maCu, svMoi);
            dssv = dao.DocDSSV();
        }

        public void XoaSinhVien(string maSV)
        {
            dao.XoaSinhVien(maSV);
            dssv = dao.DocDSSV();
        }

        public bool KiemTraTrungMaSV(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return false;
            string ma = maSV.Trim();
            var listSV = DocDSSV();
            var listGV = DocDSGV();
            return listSV.Exists(x => x.MaSV.Trim().Equals(ma, StringComparison.OrdinalIgnoreCase)) ||
                   listGV.Exists(x => x.MaGV.Trim().Equals(ma, StringComparison.OrdinalIgnoreCase)) ||
                   dao.KiemTraTrungMaTrongSQL(ma);
        }

        public List<SinhVienDTO> TimKiemSV(string tuKhoa)
        {
            var listSV = DocDSSV();
            string key = (tuKhoa ?? "").Trim();
            return listSV.FindAll(sv =>
                sv.MaSV.Contains(key, StringComparison.OrdinalIgnoreCase) ||
                sv.Ten.Contains(key, StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVienDTO> LocSinhVien(string ma, string ho, string ten, string gioiTinh, string khoa, string lop)
        {
            var listSV = DocDSSV();
            return listSV.FindAll(sv =>
                (string.IsNullOrEmpty(ma) || sv.MaSV.ToLower().Contains(ma.ToLower())) &&
                (string.IsNullOrEmpty(ho) || sv.HoLot.ToLower().Contains(ho.ToLower())) &&
                (string.IsNullOrEmpty(ten) || sv.Ten.ToLower().Contains(ten.ToLower())) &&
                (string.IsNullOrEmpty(gioiTinh) || sv.GioiTinh == gioiTinh) &&
                (string.IsNullOrEmpty(khoa) || sv.MaKhoa == khoa) &&
                (string.IsNullOrEmpty(lop) || sv.Lop == lop)
            );
        }

        public List<TienDoHocTapDTO> LayKetQuaHocTap(string maSV, string hocKy = "Tất cả", string namHoc = "Tất cả")
        {
            List<TienDoHocTapDTO> ds = dao.LayTienDoSinhVien(maSV);
            return ds.FindAll(x =>
                (string.IsNullOrEmpty(hocKy) || hocKy == "Tất cả" || x.HocKy.Equals(hocKy, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(namHoc) || namHoc == "Tất cả" || x.NamHoc.Equals(namHoc, StringComparison.OrdinalIgnoreCase))
            );
        }


        
    }
}