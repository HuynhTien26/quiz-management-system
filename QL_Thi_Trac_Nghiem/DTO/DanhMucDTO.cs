using System;

namespace QL_Thi_Trac_Nghiem.DTO
{
    // 1. DTO Khoa
    public class KhoaDTO
    {
        public string MaKhoa { get; set; } = string.Empty;
        public string TenKhoa { get; set; } = string.Empty;
    }

    // 2. DTO Lớp học
    public class LopDTO
    {
        public string MaLop { get; set; } = string.Empty;
        public string TenLop { get; set; } = string.Empty;
        public string MaKhoa { get; set; } = string.Empty;
        public string TenKhoa { get; set; } = string.Empty;
    }

    // 3. DTO Môn học
    public class MonDTO
    {
        public string MaMon { get; set; } = string.Empty;
        public string TenMon { get; set; } = string.Empty;
        public int TinChi { get; set; }
        public string MaKhoa { get; set; } = string.Empty;
    }

    // 4. DTO Thống kê theo Quý Q1, Q2, Q3, Q4, Tổng cộng (Đáp ứng Mục 10 - Tuần 7 của cô)
    public class ThongKeTheoQuyDTO
    {
        public string MaDoiTuong { get; set; } = string.Empty;
        public string TenDoiTuong { get; set; } = string.Empty;
        public int Q1 { get; set; }
        public int Q2 { get; set; }
        public int Q3 { get; set; }
        public int Q4 { get; set; }
        public int TC => Q1 + Q2 + Q3 + Q4; // Cột Tổng cộng tự động tính (Field không nhập)
    }
}