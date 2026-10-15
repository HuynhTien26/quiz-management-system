namespace QL_Thi_Trac_Nghiem.GUI
{
    partial class FormDangNhap
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
            label1 = new System.Windows.Forms.Label();
            txtTaiKhoan = new System.Windows.Forms.TextBox();
            txtMatKhau = new System.Windows.Forms.TextBox();
            btnDangNhap = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(330, 67);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(76, 15);
            label1.TabIndex = 0;
            label1.Text = "ĐĂNG NHẬP";
            // 
            // txtTaiKhoan
            // 
            txtTaiKhoan.Location = new System.Drawing.Point(330, 117);
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.Size = new System.Drawing.Size(217, 23);
            txtTaiKhoan.TabIndex = 1;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new System.Drawing.Point(330, 172);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '*';
            txtMatKhau.Size = new System.Drawing.Size(217, 23);
            txtMatKhau.TabIndex = 2;
            // 
            // btnDangNhap
            // 
            btnDangNhap.Location = new System.Drawing.Point(330, 235);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new System.Drawing.Size(75, 23);
            btnDangNhap.TabIndex = 3;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = true;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(263, 120);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(61, 15);
            label2.TabIndex = 4;
            label2.Text = "Tài khoản:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(263, 175);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(60, 15);
            label3.TabIndex = 5;
            label3.Text = "Mật khẩu:";
            // 
            // FormDangNhap
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnDangNhap);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTaiKhoan);
            Controls.Add(label1);
            Name = "FormDangNhap";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}