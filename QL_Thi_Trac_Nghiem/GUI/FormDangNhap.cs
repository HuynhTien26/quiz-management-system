using System;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.BUS; // Bắt buộc phải có dòng này để gọi tầng BUS

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormDangNhap : Form
    {
        // Khởi tạo đối tượng BUS để sử dụng xử lý logic
        TaiKhoanBUS taiKhoanBUS = new TaiKhoanBUS();

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra quyền từ SQL
            string loaiTaiKhoan = taiKhoanBUS.KiemTraDangNhap(taiKhoan, matKhau);

            if (loaiTaiKhoan == "AD")
            {
                MessageBox.Show("Đăng nhập thành công! Chào mừng Quản trị viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormAdmin frmAdmin = new FormAdmin();
                this.Hide();
                frmAdmin.ShowDialog(); // Dùng ShowDialog để khi đóng FormAdmin thì hiện lại FormDangNhap
                this.Show();
            }
            else if (loaiTaiKhoan == "GV")
            {
                MessageBox.Show($"Đăng nhập thành công! Mã giảng viên: {taiKhoan}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormGiaoVien frmGV = new FormGiaoVien();
                this.Hide();
                frmGV.ShowDialog();
                this.Show();
            }
            else if (loaiTaiKhoan == "TS" || loaiTaiKhoan == "SV")
            {
                MessageBox.Show($"Đăng nhập thành công! Mã thí sinh: {taiKhoan}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormSinhVien frmSV = new FormSinhVien();
                this.Hide();
                frmSV.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}