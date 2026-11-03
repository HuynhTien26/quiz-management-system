namespace QL_Thi_Trac_Nghiem.DTO
{
    public class TienDoHocTapDTO
    {
        public string MaMon { get; set; } = "";
        public string TenMon { get; set; } = "";
        public int TinChi { get; set; }
        public double DiemThi { get; set; }
        public string HocKy { get; set; } = "";
        public string NamHoc { get; set; } = ""; // Lưu dạng "20xy-20yz"

        public TienDoHocTapDTO() { }

        public TienDoHocTapDTO(string maMon, string tenMon, int tinChi, double diemThi, string hocKy = "", string namHoc = "")
        {
            MaMon = maMon;
            TenMon = tenMon;
            TinChi = tinChi;
            DiemThi = diemThi;
            HocKy = hocKy;
            NamHoc = namHoc;
        }
    }
}