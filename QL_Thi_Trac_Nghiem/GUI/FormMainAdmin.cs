using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormMainAdmin : Form
    {
        public string MaNguoiDung = "";
        public string VaiTro = "";

        public FormMainAdmin(string maND = "ADMIN", string vaiTro = "Admin")
        {
            InitializeComponent();
            MaNguoiDung = maND;
            VaiTro = vaiTro;
            this.Load += FormMainAdmin_Load;
        }

        private void FormMainAdmin_Load(object sender, EventArgs e)
        {
            // Kiểm tra phân quyền: Nếu không phải Admin thì vô hiệu hóa nút Quản lý Nhân sự
            bool laAdmin = VaiTro.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            if (btnQuanLyNhanSu != null)
            {
                btnQuanLyNhanSu.Enabled = laAdmin;
                btnQuanLyNhanSu.Visible = laAdmin;
            }
        }

        private Form activeForm = null;

        private void MoFormCon(Form formCon)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = formCon;
            formCon.TopLevel = false;
            formCon.FormBorderStyle = FormBorderStyle.None;
            formCon.Dock = DockStyle.Fill;

            pnMainDesktop.Controls.Add(formCon);
            pnMainDesktop.Tag = formCon;
            formCon.BringToFront();
            formCon.Show();
        }

        // 1. Nút "Quản lý Nhân sự" (Chỉ Admin mới bấm được)
        private void btnQuanLyNhanSu_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormAdmin());
        }

        // 2. Thêm sự kiện cho nút "Quản lý Hệ thống" trên menu trái để mở FormGiaoVien
        private void btnQuanLyHeThong_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormGiaoVien(MaNguoiDung, VaiTro));
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Đóng FormMainAdmin hiện tại, luồng sẽ tự động trả về FormDangNhap
            this.Close();
        }
    }
}