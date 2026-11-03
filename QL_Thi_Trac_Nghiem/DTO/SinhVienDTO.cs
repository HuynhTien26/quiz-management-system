using System;

namespace QL_Thi_Trac_Nghiem.DTO
{
    public class SinhVienDTO
    {
        public string MaSV { get; set; }
        public string HoLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }

        // Đây chính là dòng bạn đang thiếu khiến code báo lỗi đỏ:
        public string MaKhoa { get; set; }

        public string Lop { get; set; }

        // Constructor phải nhận đủ 7 tham số
        public SinhVienDTO(string maSV, string hoLot, string ten, DateTime ngaySinh, string gioiTinh, string maKhoa, string lop)
        {
            MaSV = maSV;
            HoLot = hoLot;
            Ten = ten;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            MaKhoa = maKhoa; // Và gán giá trị ở đây
            Lop = lop;
        }
    }
}