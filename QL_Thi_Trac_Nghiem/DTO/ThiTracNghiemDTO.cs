using System;

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
        // Lưu tạm đáp án thí sinh đang chọn trên giao diện ('A', 'B', 'C', 'D' hoặc "")
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
        public string KetQua { get; set; } = ""; // "Đúng" / "Sai"
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
}
