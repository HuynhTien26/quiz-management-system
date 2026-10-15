using System;

namespace QL_Thi_Trac_Nghiem.GUI
{
    partial class FormAdmin
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
            lsvDanhSach = new ListView();
            colMa = new ColumnHeader();
            colHo = new ColumnHeader();
            colTen = new ColumnHeader();
            colGioi = new ColumnHeader();
            colNgaySinh = new ColumnHeader();
            colKhoa = new ColumnHeader();
            colMon = new ColumnHeader();
            colLop = new ColumnHeader();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            rdoNam = new RadioButton();
            rdoNu = new RadioButton();
            label5 = new Label();
            label6 = new Label();
            rdoGiaoVien = new RadioButton();
            rdoSinhVien = new RadioButton();
            cmbKhoa = new ComboBox();
            dtpNgaySinh = new DateTimePicker();
            label7 = new Label();
            clbMon = new CheckedListBox();
            label8 = new Label();
            label9 = new Label();
            cmbLop = new ComboBox();
            txtMa = new TextBox();
            txtHo = new TextBox();
            txtTen = new TextBox();
            btnThem = new Button();
            btnXoa = new Button();
            btnTim = new Button();
            btnCapNhat = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lsvDanhSach
            // 
            lsvDanhSach.Columns.AddRange(new ColumnHeader[] { colMa, colHo, colTen, colGioi, colNgaySinh, colKhoa, colMon, colLop });
            lsvDanhSach.FullRowSelect = true;
            lsvDanhSach.GridLines = true;
            lsvDanhSach.Location = new Point(473, 36);
            lsvDanhSach.Name = "lsvDanhSach";
            lsvDanhSach.Size = new Size(495, 330);
            lsvDanhSach.TabIndex = 0;
            lsvDanhSach.UseCompatibleStateImageBehavior = false;
            lsvDanhSach.View = View.Details;
            lsvDanhSach.SelectedIndexChanged += lsvDanhSach_SelectedIndexChanged;
            // 
            // colMa
            // 
            colMa.Text = "Mã";
            // 
            // colHo
            // 
            colHo.Text = "Họ lót";
            // 
            // colTen
            // 
            colTen.Text = "Tên";
            // 
            // colGioi
            // 
            colGioi.Text = "Giới tính";
            // 
            // colNgaySinh
            // 
            colNgaySinh.DisplayIndex = 6;
            colNgaySinh.Text = "Ngày sinh";
            // 
            // colKhoa
            // 
            colKhoa.DisplayIndex = 4;
            colKhoa.Text = "Khoa";
            // 
            // colMon
            // 
            colMon.DisplayIndex = 5;
            colMon.Text = "Môn";
            // 
            // colLop
            // 
            colLop.Text = "Lớp";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 39);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 1;
            label1.Text = "Mã:";
            
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 75);
            label2.Name = "label2";
            label2.Size = new Size(26, 15);
            label2.TabIndex = 2;
            label2.Text = "Họ:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(199, 75);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 3;
            label3.Text = "Tên:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(35, 141);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 4;
            label4.Text = "Giới:";
            // 
            // rdoNam
            // 
            rdoNam.AutoSize = true;
            rdoNam.Location = new Point(36, 159);
            rdoNam.Name = "rdoNam";
            rdoNam.Size = new Size(51, 19);
            rdoNam.TabIndex = 5;
            rdoNam.TabStop = true;
            rdoNam.Text = "Nam";
            rdoNam.UseVisualStyleBackColor = true;
            
            // 
            // rdoNu
            // 
            rdoNu.AutoSize = true;
            rdoNu.Location = new Point(126, 159);
            rdoNu.Name = "rdoNu";
            rdoNu.Size = new Size(41, 19);
            rdoNu.TabIndex = 6;
            rdoNu.TabStop = true;
            rdoNu.Text = "Nữ";
            rdoNu.UseVisualStyleBackColor = true;
            
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 254);
            label5.Name = "label5";
            label5.Size = new Size(37, 15);
            label5.TabIndex = 7;
            label5.Text = "Khoa:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 6);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 8;
            label6.Text = "Vai trò:";
            
            // 
            // rdoGiaoVien
            // 
            rdoGiaoVien.AutoSize = true;
            rdoGiaoVien.Location = new Point(4, 24);
            rdoGiaoVien.Name = "rdoGiaoVien";
            rdoGiaoVien.Size = new Size(74, 19);
            rdoGiaoVien.TabIndex = 9;
            rdoGiaoVien.TabStop = true;
            rdoGiaoVien.Text = "Giáo viên";
            rdoGiaoVien.UseVisualStyleBackColor = true;
            rdoGiaoVien.CheckedChanged += rdoGiaoVien_CheckedChanged;
            // 
            // rdoSinhVien
            // 
            rdoSinhVien.AutoSize = true;
            rdoSinhVien.Location = new Point(94, 24);
            rdoSinhVien.Name = "rdoSinhVien";
            rdoSinhVien.Size = new Size(67, 19);
            rdoSinhVien.TabIndex = 10;
            rdoSinhVien.TabStop = true;
            rdoSinhVien.Text = "Thí sinh";
            rdoSinhVien.UseVisualStyleBackColor = true;
            rdoSinhVien.CheckedChanged += rdoSinhVien_CheckedChanged;
            // 
            // cmbKhoa
            // 
            cmbKhoa.FormattingEnabled = true;
            cmbKhoa.Location = new Point(36, 272);
            cmbKhoa.Name = "cmbKhoa";
            cmbKhoa.Size = new Size(121, 23);
            cmbKhoa.TabIndex = 11;
            
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(101, 108);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(92, 23);
            dtpNgaySinh.TabIndex = 12;
            
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(32, 114);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 13;
            label7.Text = "Ngày sinh:";
            
            // 
            // clbMon
            // 
            clbMon.FormattingEnabled = true;
            clbMon.Location = new Point(235, 272);
            clbMon.Name = "clbMon";
            clbMon.Size = new Size(120, 94);
            clbMon.TabIndex = 15;
            
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(235, 254);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 16;
            label8.Text = "Môn:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(36, 325);
            label9.Name = "label9";
            label9.Size = new Size(30, 15);
            label9.TabIndex = 17;
            label9.Text = "Lớp:";
            // 
            // cmbLop
            // 
            cmbLop.FormattingEnabled = true;
            cmbLop.Location = new Point(36, 343);
            cmbLop.Name = "cmbLop";
            cmbLop.Size = new Size(121, 23);
            cmbLop.TabIndex = 18;
            
            // 
            // txtMa
            // 
            txtMa.Location = new Point(67, 36);
            txtMa.Name = "txtMa";
            txtMa.Size = new Size(126, 23);
            txtMa.TabIndex = 19;
            
            // 
            // txtHo
            // 
            txtHo.Location = new Point(67, 72);
            txtHo.Name = "txtHo";
            txtHo.Size = new Size(126, 23);
            txtHo.TabIndex = 20;
            
            // 
            // txtTen
            // 
            txtTen.Location = new Point(234, 72);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(121, 23);
            txtTen.TabIndex = 21;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(557, 383);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(75, 23);
            btnThem.TabIndex = 22;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(719, 383);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 23);
            btnXoa.TabIndex = 23;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnTim
            // 
            btnTim.Location = new Point(800, 383);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(75, 23);
            btnTim.TabIndex = 24;
            btnTim.Text = "Tìm";
            btnTim.UseVisualStyleBackColor = true;
            btnTim.Click += btnTim_Click;
            // 
            // btnCapNhat
            // 
            btnCapNhat.Location = new Point(638, 383);
            btnCapNhat.Name = "btnCapNhat";
            btnCapNhat.Size = new Size(75, 23);
            btnCapNhat.TabIndex = 25;
            btnCapNhat.Text = "Cập nhật";
            btnCapNhat.UseVisualStyleBackColor = true;
            btnCapNhat.Click += btnCapNhat_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoSinhVien);
            panel1.Controls.Add(rdoGiaoVien);
            panel1.Controls.Add(label6);
            panel1.Location = new Point(32, 188);
            panel1.Name = "panel1";
            panel1.Size = new Size(209, 54);
            panel1.TabIndex = 26;
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 450);
            Controls.Add(panel1);
            Controls.Add(btnCapNhat);
            Controls.Add(btnTim);
            Controls.Add(btnXoa);
            Controls.Add(btnThem);
            Controls.Add(txtTen);
            Controls.Add(txtHo);
            Controls.Add(txtMa);
            Controls.Add(cmbLop);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(clbMon);
            Controls.Add(label7);
            Controls.Add(dtpNgaySinh);
            Controls.Add(cmbKhoa);
            Controls.Add(label5);
            Controls.Add(rdoNu);
            Controls.Add(rdoNam);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lsvDanhSach);
            Name = "FormAdmin";
            Text = "FormAdmin";
            Load += FormAdmin_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private System.Windows.Forms.ListView lsvDanhSach;
        private System.Windows.Forms.ColumnHeader colMa;
        private System.Windows.Forms.ColumnHeader colHo;
        private System.Windows.Forms.ColumnHeader colTen;
        private System.Windows.Forms.ColumnHeader colGioi;
        private System.Windows.Forms.ColumnHeader colKhoa;
        private System.Windows.Forms.ColumnHeader colMon;
        private System.Windows.Forms.ColumnHeader colNgaySinh;
        private System.Windows.Forms.ColumnHeader colLop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoNam;
        private System.Windows.Forms.RadioButton rdoNu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rabtnGV;
        private System.Windows.Forms.RadioButton rdoSinhVien;
        private System.Windows.Forms.ComboBox cmbKhoa;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox clbMon;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbLop;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.TextBox txtHo;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoGiaoVien;
    }
}