namespace QL_Thi_Trac_Nghiem.DTO
{
    public class ThongKeDeThiDTO
    {
        public string MaDe { get; set; } = "";
        public string TenMon { get; set; } = "";
        public int SoCauHoi { get; set; }
        public int ThoiGian { get; set; }
        public string HocKy { get; set; } = "";
        public string NamHoc { get; set; } = ""; // Lưu dạng "20xy-20yz"

        public ThongKeDeThiDTO() { }

        public ThongKeDeThiDTO(string maDe, string tenMon, int soCauHoi, int thoiGian, string hocKy = "", string namHoc = "")
        {
            MaDe = maDe;
            TenMon = tenMon;
            SoCauHoi = soCauHoi;
            ThoiGian = thoiGian;
            HocKy = hocKy;
            NamHoc = namHoc;
        }
    }
}