namespace QL_Thi_Trac_Nghiem.DTO
{
    // DTO lưu kết quả nhập từng dòng khi import câu hỏi từ Excel
    public class KetQuaImportDTO
    {
        public int Dong { get; set; }
        public string MaCH { get; set; } = string.Empty;
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; } = string.Empty;
    }
}