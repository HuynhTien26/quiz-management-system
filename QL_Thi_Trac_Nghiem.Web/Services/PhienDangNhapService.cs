namespace QL_Thi_Trac_Nghiem.Web.Services
{
    public class PhienDangNhapService
    {
        public event Action? OnChange;

        public string MaNguoiDung { get; private set; } = string.Empty;
        public string LoaiTaiKhoan { get; private set; } = string.Empty;
        public string VaiTroHienThi { get; private set; } = string.Empty;

        public bool DaDangNhap => !string.IsNullOrWhiteSpace(MaNguoiDung);
        public bool LaAdmin => LoaiTaiKhoan.Equals("AD", StringComparison.OrdinalIgnoreCase);
        public bool LaGiaoVien => LoaiTaiKhoan.Equals("GV", StringComparison.OrdinalIgnoreCase);
        public bool LaSinhVien => LoaiTaiKhoan.Equals("SV", StringComparison.OrdinalIgnoreCase) ||
                                LoaiTaiKhoan.Equals("TS", StringComparison.OrdinalIgnoreCase);

        public void DangNhapThanhCong(string maNguoiDung, string loaiTaiKhoan)
        {
            MaNguoiDung = maNguoiDung.Trim();
            LoaiTaiKhoan = loaiTaiKhoan.Trim().ToUpper();
            VaiTroHienThi = LoaiTaiKhoan switch
            {
                "AD" => "Quản trị viên",
                "GV" => "Giáo viên",
                "SV" or "TS" => "Sinh viên",
                _ => "Người dùng"
            };
            OnChange?.Invoke();
        }

        public void DangXuat()
        {
            MaNguoiDung = string.Empty;
            LoaiTaiKhoan = string.Empty;
            VaiTroHienThi = string.Empty;
            OnChange?.Invoke();
        }
    }
}
