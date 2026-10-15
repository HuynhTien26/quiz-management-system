using QL_Thi_Trac_Nghiem.DAO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    public class TaiKhoanBUS
    {
        public string KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            // Ở tầng GUI ta đã kiểm tra rỗng rồi, 
            // nên tầng BUS chỉ việc gọi thẳng xuống DAO để lấy kết quả
            return TaiKhoanDAO.Instance.KiemTraDangNhap(taiKhoan, matKhau);
        }
    }
}