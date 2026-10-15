using System;
using System.Collections.Generic;
using System.Data;
using QL_Thi_Trac_Nghiem.DAO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    public class AdminBUS
    {
        public DataTable LayDanhSachGiaoVien() => AdminDAO.Instance.LayDanhSachGiaoVien();
        public DataTable LayDanhSachSinhVien() => AdminDAO.Instance.LayDanhSachSinhVien();

        public bool KiemTraTrungMaGV(string ma) => AdminDAO.Instance.KiemTraTrungMaGV(ma);
        public bool KiemTraTrungMaSV(string ma) => AdminDAO.Instance.KiemTraTrungMaSV(ma);

        public bool ThemGiaoVien(string ma, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string mon)
            => AdminDAO.Instance.ThemGiaoVien(ma, ho, ten, ngaySinh, gioiTinh, khoa, mon);

        public bool ThemSinhVien(string ma, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string lop)
            => AdminDAO.Instance.ThemSinhVien(ma, ho, ten, ngaySinh, gioiTinh, khoa, lop);

        public bool CapNhatGiaoVien(string maCu, string maMoi, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string mon)
            => AdminDAO.Instance.CapNhatGiaoVien(maCu, maMoi, ho, ten, ngaySinh, gioiTinh, khoa, mon);

        public bool CapNhatSinhVien(string maCu, string maMoi, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa, string lop)
            => AdminDAO.Instance.CapNhatSinhVien(maCu, maMoi, ho, ten, ngaySinh, gioiTinh, khoa, lop);

        public bool XoaGiaoVien(string ma) => AdminDAO.Instance.XoaGiaoVien(ma);
        public bool XoaSinhVien(string ma) => AdminDAO.Instance.XoaSinhVien(ma);

        public DataTable TimGiaoVien(string ma, string ho, string ten, string gioiTinh, string khoa, List<string> dsMon)
            => AdminDAO.Instance.TimGiaoVien(ma, ho, ten, gioiTinh, khoa, dsMon);

        public DataTable TimSinhVien(string ma, string ho, string ten, string gioiTinh, string khoa, string lop)
            => AdminDAO.Instance.TimSinhVien(ma, ho, ten, gioiTinh, khoa, lop);
    }
}