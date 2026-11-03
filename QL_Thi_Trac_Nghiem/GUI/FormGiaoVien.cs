using System;
using System.Windows.Forms;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormGiaoVien : Form
    {
        // Khai báo dạng biến thường (không dùng { get; set; }) để không bị lỗi WFO1000
        public string MaNguoiDungDangNhap = "";
        public string VaiTroDangNhap = "";

        public FormGiaoVien(string maND = "", string vaiTro = "Admin")
        {
            InitializeComponent();
            MaNguoiDungDangNhap = maND;
            VaiTroDangNhap = vaiTro;

            this.Load -= FormGiaoVien_Load;
            this.Load += FormGiaoVien_Load;
        }

        private void FormGiaoVien_Load(object sender, EventArgs e)
        {
            PhanQuyenGiaoDien();
        }

        private void PhanQuyenGiaoDien()
        {
            bool laAdmin = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            // 1. Tự động điền Mã GV vào ô Mã Giáo Viên ở Tab Đề thi và khóa lại
            txtMaGVDangTai.Text = laAdmin ? "ADMIN" : MaNguoiDungDangNhap;
            txtMaGVDangTai.ReadOnly = true;

            // 2. Nút "Đăng tải" đề thi: Chỉ Admin mới có quyền bấm duyệt đăng tải cho SV
            btnDangTaiDe.Enabled = laAdmin;

            // 3. Nếu là Giáo viên: Ẩn Tab Quản lý Khoa - Lớp và khóa quyền sửa Môn học / Kỳ thi
            if (!laAdmin)
            {
                if (tabControl1.TabPages.ContainsKey("tabPageKhoaLop"))
                {
                    tabControl1.TabPages.RemoveByKey("tabPageKhoaLop");
                }

                btnThemMon.Enabled = false;
                btnCapNhatMon.Enabled = false;
                btnXoaMon.Enabled = false;

                btnThemKT.Enabled = false;
                btnCapNhatKT.Enabled = false;
                btnXoaKT.Enabled = false;
            }
        }

        private void btnXoaCH_Click(object sender, EventArgs e)
        {
        }
    }
}