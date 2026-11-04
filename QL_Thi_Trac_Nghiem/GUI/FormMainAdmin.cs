using System;
using System.Windows.Forms;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormMainAdmin : Form
    {
        public string MaNguoiDung = "";
        public string VaiTro = "";
        private Form? activeForm = null;

        public FormMainAdmin(string maND = "ADMIN", string vaiTro = "Admin")
        {
            InitializeComponent();
            MaNguoiDung = maND;
            VaiTro = vaiTro;

            this.Load -= FormMainAdmin_Load;
            this.Load += FormMainAdmin_Load;

            // Tự động gắn sự kiện Click cho nút "Quản lý Hệ thống" dù nút đó tên là gì trong Designer
            GanSuKienMenuTrai(this);
        }

        private void GanSuKienMenuTrai(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button btn)
                {
                    if (btn.Text.Contains("Hệ thống", StringComparison.OrdinalIgnoreCase))
                    {
                        btn.Click -= btnQuanLyHeThong_Click;
                        btn.Click += btnQuanLyHeThong_Click;
                    }
                }
                if (ctrl.HasChildren)
                {
                    GanSuKienMenuTrai(ctrl);
                }
            }
        }

        private void FormMainAdmin_Load(object sender, EventArgs e)
        {
            bool laAdmin = VaiTro.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            // Nếu là Giáo viên: Khóa nút Quản lý Nhân sự và mở sẵn trang Quản lý Hệ thống
            if (btnQuanLyNhanSu != null)
            {
                btnQuanLyNhanSu.Enabled = laAdmin;
            }

            if (!laAdmin)
            {
                MoFormCon(new FormGiaoVien(MaNguoiDung, VaiTro));
            }
        }

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

        private void btnQuanLyNhanSu_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormAdmin());
        }

        private void btnQuanLyHeThong_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormGiaoVien(MaNguoiDung, VaiTro));
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}