using System;
using System.ComponentModel;

namespace QL_Thi_Trac_Nghiem.DTO
{
    public class CauHoiThiDTO
    {
        public string MaCH { get; set; } = "";
        public string MaDe { get; set; } = "";
        public string NoiDung { get; set; } = "";
        public string DapAnA { get; set; } = "";
        public string DapAnB { get; set; } = "";
        public string DapAnC { get; set; } = "";
        public string DapAnD { get; set; } = "";
        public string DapAnDung { get; set; } = "";
        public string DapAnDaChon { get; set; } = "";
    }

    public class BaiThiDTO
    {
        public string MaBT { get; set; } = "";
        public string MaSV { get; set; } = "";
        public string MaDe { get; set; } = "";
        public string TenMon { get; set; } = "";
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public int SoCauDung { get; set; }
        public int SoCauSai { get; set; }
        public double TongDiem { get; set; }
        public string TrangThai { get; set; } = "";
    }

    public class ChiTietBaiLamDTO
    {
        public string MaBT { get; set; } = "";
        public string MaDe { get; set; } = "";
        public string MaCH { get; set; } = "";
        public string NoiDungCauHoi { get; set; } = "";
        public string DapAnA { get; set; } = "";
        public string DapAnB { get; set; } = "";
        public string DapAnC { get; set; } = "";
        public string DapAnD { get; set; } = "";
        public string DapAnChon { get; set; } = "";
        public string DapAnDung { get; set; } = "";
        public string KetQua { get; set; } = "";
    }

    public class BaoCaoDiemThiDTO
    {
        public string MaBT { get; set; } = "";
        public string MaSV { get; set; } = "";
        public string HoTenSV { get; set; } = "";
        public string Lop { get; set; } = "";
        public string MaDe { get; set; } = "";
        public string TenMon { get; set; } = "";
        public string KyThi { get; set; } = "";
        public string NamHoc { get; set; } = "";
        public int SoCauDung { get; set; }
        public int SoCauSai { get; set; }
        public double TongDiem { get; set; }
        public DateTime NgayThi { get; set; }
    }

    public class TongHopThongKeDTO
    {
        public int TongSoBai { get; set; }
        public int SoBaiDat { get; set; }
        public int SoBaiChuaDat { get; set; }
        public double TyLeDat { get; set; }
        public double DiemTrungBinh { get; set; }
        public double DiemCaoNhat { get; set; }
    }

    public class BaoCaoTheoQuyDTO
    {
        [DisplayName("Đối tượng thống kê")]
        public string TenDoiTuong { get; set; } = "";

        [DisplayName("Q1")]
        public double Q1 { get; set; }

        [DisplayName("Q2")]
        public double Q2 { get; set; }

        [DisplayName("Q3")]
        public double Q3 { get; set; }

        [DisplayName("Q4")]
        public double Q4 { get; set; }

        [DisplayName("TC")]
        public double TC { get; set; }
    }
}