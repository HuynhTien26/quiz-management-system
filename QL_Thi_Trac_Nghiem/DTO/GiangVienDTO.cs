using System;

// BẮT BUỘC PHẢI CÓ CHỮ .DTO Ở ĐUÔI
namespace QL_Thi_Trac_Nghiem.DTO
{
    // BẮT BUỘC PHẢI CÓ CHỮ "public"
    public class GiangVienDTO
    {
        public string MaGV { get; set; }
        public string HoLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string MaKhoa { get; set; }
        public string MaMon { get; set; }

        public GiangVienDTO(string maGV, string hoLot, string ten, DateTime ngaySinh, string gioiTinh, string maKhoa, string maMon)
        {
            MaGV = maGV;
            HoLot = hoLot;
            Ten = ten;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            MaKhoa = maKhoa;
            MaMon = maMon;
        }
    }
}