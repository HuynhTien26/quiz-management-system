namespace QL_Thi_Trac_Nghiem.GUI
{
    partial class FormSinhVien
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            label1 = new Label();
            txtThongTinSV = new TextBox();
            cmbChonKyThi = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            txtNgayThi = new TextBox();
            lsvDanhSachBaiThi = new ListView();
            btnBatDauLamBai = new Button();
            lsvLichSuBaiThi = new ListView();
            label4 = new Label();
            label5 = new Label();
            txtTongDiem = new TextBox();
            label6 = new Label();
            txtSoCauDung = new TextBox();
            label7 = new Label();
            txtSoCauSai = new TextBox();
            label8 = new Label();
            lsvChiTietBaiLam = new ListView();
            rtxtXemChiTietCau = new RichTextBox();
            label9 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(13, 28);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(775, 398);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnBatDauLamBai);
            tabPage1.Controls.Add(lsvDanhSachBaiThi);
            tabPage1.Controls.Add(txtNgayThi);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(cmbChonKyThi);
            tabPage1.Controls.Add(txtThongTinSV);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(767, 370);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(rtxtXemChiTietCau);
            tabPage2.Controls.Add(lsvChiTietBaiLam);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(txtSoCauSai);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(txtSoCauDung);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(txtTongDiem);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(lsvLichSuBaiThi);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(767, 370);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 27);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 0;
            label1.Text = "Mã Sinh viên:";
            // 
            // txtThongTinSV
            // 
            txtThongTinSV.Location = new Point(119, 24);
            txtThongTinSV.Name = "txtThongTinSV";
            txtThongTinSV.ReadOnly = true;
            txtThongTinSV.Size = new Size(100, 23);
            txtThongTinSV.TabIndex = 1;
            // 
            // cmbChonKyThi
            // 
            cmbChonKyThi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChonKyThi.FormattingEnabled = true;
            cmbChonKyThi.Location = new Point(119, 62);
            cmbChonKyThi.Name = "cmbChonKyThi";
            cmbChonKyThi.Size = new Size(100, 23);
            cmbChonKyThi.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 65);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 3;
            label2.Text = "Chọn Kỳ thi:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(279, 65);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 4;
            label3.Text = "Ngày thi:";
            // 
            // txtNgayThi
            // 
            txtNgayThi.Location = new Point(340, 62);
            txtNgayThi.Name = "txtNgayThi";
            txtNgayThi.ReadOnly = true;
            txtNgayThi.Size = new Size(100, 23);
            txtNgayThi.TabIndex = 5;
            // 
            // lsvDanhSachBaiThi
            // 
            lsvDanhSachBaiThi.Location = new Point(35, 91);
            lsvDanhSachBaiThi.Name = "lsvDanhSachBaiThi";
            lsvDanhSachBaiThi.Size = new Size(673, 193);
            lsvDanhSachBaiThi.TabIndex = 6;
            lsvDanhSachBaiThi.UseCompatibleStateImageBehavior = false;
            // 
            // btnBatDauLamBai
            // 
            btnBatDauLamBai.Location = new Point(35, 290);
            btnBatDauLamBai.Name = "btnBatDauLamBai";
            btnBatDauLamBai.Size = new Size(117, 23);
            btnBatDauLamBai.TabIndex = 7;
            btnBatDauLamBai.Text = "Bắt đầu làm bài";
            btnBatDauLamBai.UseVisualStyleBackColor = true;
            // 
            // lsvLichSuBaiThi
            // 
            lsvLichSuBaiThi.Location = new Point(26, 26);
            lsvLichSuBaiThi.Name = "lsvLichSuBaiThi";
            lsvLichSuBaiThi.Size = new Size(708, 97);
            lsvLichSuBaiThi.TabIndex = 0;
            lsvLichSuBaiThi.UseCompatibleStateImageBehavior = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 8);
            label4.Name = "label4";
            label4.Size = new Size(168, 15);
            label4.TabIndex = 1;
            label4.Text = "Bảng lịch sử các Bài thi đã nộp";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 132);
            label5.Name = "label5";
            label5.Size = new Size(68, 15);
            label5.TabIndex = 2;
            label5.Text = "Tổng điểm:";
            // 
            // txtTongDiem
            // 
            txtTongDiem.Location = new Point(94, 129);
            txtTongDiem.Name = "txtTongDiem";
            txtTongDiem.ReadOnly = true;
            txtTongDiem.Size = new Size(86, 23);
            txtTongDiem.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(214, 132);
            label6.Name = "label6";
            label6.Size = new Size(76, 15);
            label6.TabIndex = 4;
            label6.Text = "Số câu đúng:";
            // 
            // txtSoCauDung
            // 
            txtSoCauDung.Location = new Point(296, 129);
            txtSoCauDung.Name = "txtSoCauDung";
            txtSoCauDung.ReadOnly = true;
            txtSoCauDung.Size = new Size(100, 23);
            txtSoCauDung.TabIndex = 5;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(402, 132);
            label7.Name = "label7";
            label7.Size = new Size(62, 15);
            label7.TabIndex = 6;
            label7.Text = "Số câu sai:";
            // 
            // txtSoCauSai
            // 
            txtSoCauSai.Location = new Point(470, 129);
            txtSoCauSai.Name = "txtSoCauSai";
            txtSoCauSai.ReadOnly = true;
            txtSoCauSai.Size = new Size(100, 23);
            txtSoCauSai.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(26, 177);
            label8.Name = "label8";
            label8.Size = new Size(199, 15);
            label8.TabIndex = 8;
            label8.Text = "Bảng Chi tiết bài làm và Gợi ý sửa lỗi";
            // 
            // lsvChiTietBaiLam
            // 
            lsvChiTietBaiLam.Location = new Point(23, 198);
            lsvChiTietBaiLam.Name = "lsvChiTietBaiLam";
            lsvChiTietBaiLam.Size = new Size(711, 90);
            lsvChiTietBaiLam.TabIndex = 9;
            lsvChiTietBaiLam.UseCompatibleStateImageBehavior = false;
            // 
            // rtxtXemChiTietCau
            // 
            rtxtXemChiTietCau.Location = new Point(23, 309);
            rtxtXemChiTietCau.Name = "rtxtXemChiTietCau";
            rtxtXemChiTietCau.Size = new Size(711, 48);
            rtxtXemChiTietCau.TabIndex = 10;
            rtxtXemChiTietCau.Text = "";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(23, 291);
            label9.Name = "label9";
            label9.Size = new Size(154, 15);
            label9.TabIndex = 11;
            label9.Text = "Xem chi tiết câu hỏi & đáp án";
            // 
            // FormSinhVien
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 487);
            Controls.Add(tabControl1);
            Name = "FormSinhVien";
            Text = "FormSinhVien";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private ListView lsvDanhSachBaiThi;
        private TextBox txtNgayThi;
        private Label label3;
        private Label label2;
        private ComboBox cmbChonKyThi;
        private TextBox txtThongTinSV;
        private Label label1;
        private TabPage tabPage2;
        private Button btnBatDauLamBai;
        private Label label6;
        private TextBox txtTongDiem;
        private Label label5;
        private Label label4;
        private ListView lsvLichSuBaiThi;
        private RichTextBox rtxtXemChiTietCau;
        private ListView lsvChiTietBaiLam;
        private Label label8;
        private TextBox txtSoCauSai;
        private Label label7;
        private TextBox txtSoCauDung;
        private Label label9;
    }
}