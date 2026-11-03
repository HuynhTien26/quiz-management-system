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
        public FormMainAdmin()
        {
            InitializeComponent();
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

        // Bấm đúp vào nút "Quản lý Nhân sự" trên giao diện để tạo sự kiện Click:
        private void btnQuanLyNhanSu_Click(object sender, EventArgs e)
        {
            MoFormCon(new FormAdmin());
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Đóng FormMainAdmin hiện tại, luồng sẽ tự động trả về FormDangNhap
            this.Close();
        }
    }
}
