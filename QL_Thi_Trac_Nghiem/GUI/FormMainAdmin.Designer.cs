namespace QL_Thi_Trac_Nghiem.GUI
{
    partial class FormMainAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnMenuLeft = new Panel();
            btnDangXuat = new Button();
            btnQuanLyHeThong = new Button();
            btnQuanLyNhanSu = new Button();
            pnMainDesktop = new Panel();
            pnMenuLeft.SuspendLayout();
            SuspendLayout();
            // 
            // pnMenuLeft
            // 
            pnMenuLeft.BackColor = Color.Gray;
            pnMenuLeft.Controls.Add(btnDangXuat);
            pnMenuLeft.Controls.Add(btnQuanLyHeThong);
            pnMenuLeft.Controls.Add(btnQuanLyNhanSu);
            pnMenuLeft.Dock = DockStyle.Left;
            pnMenuLeft.Location = new Point(0, 0);
            pnMenuLeft.Name = "pnMenuLeft";
            pnMenuLeft.Size = new Size(245, 450);
            pnMenuLeft.TabIndex = 0;
            // 
            // btnDangXuat
            // 
            btnDangXuat.Location = new Point(46, 116);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(137, 38);
            btnDangXuat.TabIndex = 2;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // btnQuanLyHeThong
            // 
            btnQuanLyHeThong.Location = new Point(46, 72);
            btnQuanLyHeThong.Name = "btnQuanLyHeThong";
            btnQuanLyHeThong.Size = new Size(138, 38);
            btnQuanLyHeThong.TabIndex = 1;
            btnQuanLyHeThong.Text = "Quản lý Hệ thống";
            btnQuanLyHeThong.UseVisualStyleBackColor = true;
            // 
            // btnQuanLyNhanSu
            // 
            btnQuanLyNhanSu.Location = new Point(46, 28);
            btnQuanLyNhanSu.Name = "btnQuanLyNhanSu";
            btnQuanLyNhanSu.Size = new Size(137, 38);
            btnQuanLyNhanSu.TabIndex = 0;
            btnQuanLyNhanSu.Text = "Quản lý Nhân sự";
            btnQuanLyNhanSu.UseVisualStyleBackColor = true;
            btnQuanLyNhanSu.Click += btnQuanLyNhanSu_Click;
            // 
            // pnMainDesktop
            // 
            pnMainDesktop.Dock = DockStyle.Fill;
            pnMainDesktop.Location = new Point(245, 0);
            pnMainDesktop.Name = "pnMainDesktop";
            pnMainDesktop.Size = new Size(555, 450);
            pnMainDesktop.TabIndex = 1;
            // 
            // FormMainAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnMainDesktop);
            Controls.Add(pnMenuLeft);
            Name = "FormMainAdmin";
            Text = "FormMainAdmin";
            pnMenuLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnMenuLeft;
        private Button btnDangXuat;
        private Button btnQuanLyHeThong;
        private Button btnQuanLyNhanSu;
        private Panel pnMainDesktop;
    }
}