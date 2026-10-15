using System.Data;

namespace QL_Thi_Trac_Nghiem.DAO
{
    public class TaiKhoanDAO
    {
        private static TaiKhoanDAO instance;
        public static TaiKhoanDAO Instance
        {
            get { if (instance == null) instance = new TaiKhoanDAO(); return instance; }
            private set { instance = value; }
        }

        private TaiKhoanDAO() { }

        public string KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            // Viết câu lệnh SQL lấy LoaiTaiKhoan dựa vào user/pass
            string query = $"SELECT LoaiTaiKhoan FROM TaiKhoan WHERE TenDangNhap = '{tenDangNhap}' AND MatKhau = '{matKhau}'";

            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                // Nếu có kết quả, trả về loại tài khoản ('GV' hoặc 'TS')
                return result.Rows[0]["LoaiTaiKhoan"].ToString();
            }

            // Trả về chuỗi rỗng nếu không tìm thấy
            return "";
        }
    }
}