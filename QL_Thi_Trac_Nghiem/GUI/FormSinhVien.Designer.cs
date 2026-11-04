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
            btnBatDauLamBai = new Button();
            lsvDanhSachBaiThi = new ListView();
            txtNgayThi = new TextBox();
            label3 = new Label();
            label2 = new Label();
            cmbChonKyThi = new ComboBox();
            txtThongTinSV = new TextBox();
            label1 = new Label();
            tabPage2 = new TabPage();
            label9 = new Label();
            rtxtXemChiTietCau = new RichTextBox();
            lsvChiTietBaiLam = new ListView();
            label8 = new Label();
            txtSoCauSai = new TextBox();
            label7 = new Label();
            txtSoCauDung = new TextBox();
            label6 = new Label();
            txtTongDiem = new TextBox();
            label5 = new Label();
            label4 = new Label();
            lsvLichSuBaiThi = new ListView();
            btnDangXuatSV = new Button();
            colMaDe = new ColumnHeader();
            colMonHoc = new ColumnHeader();
            colKyThi = new ColumnHeader();
            colNamHoc = new ColumnHeader();
            colSoCauHoi = new ColumnHeader();
            colThoiGian = new ColumnHeader();
            cmbNamHocSV = new ComboBox();
            label10 = new Label();
            cmbMonThiSV = new ComboBox();
            label11 = new Label();
            btnTaiLaiDeThi = new Button();
            txtHoTenSV = new TextBox();
            label12 = new Label();
            colMaBaiThi = new ColumnHeader();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            btnXuatPDFBangDiem = new Button();
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
            tabControl1.Size = new Size(775, 447);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label12);
            tabPage1.Controls.Add(txtHoTenSV);
            tabPage1.Controls.Add(btnTaiLaiDeThi);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(cmbMonThiSV);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(cmbNamHocSV);
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
            tabPage1.Size = new Size(767, 419);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "1. Danh sách Đề thi";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnBatDauLamBai
            // 
            btnBatDauLamBai.Location = new Point(35, 390);
            btnBatDauLamBai.Name = "btnBatDauLamBai";
            btnBatDauLamBai.Size = new Size(117, 23);
            btnBatDauLamBai.TabIndex = 7;
            btnBatDauLamBai.Text = "Bắt đầu làm bài";
            btnBatDauLamBai.UseVisualStyleBackColor = true;
            // 
            // lsvDanhSachBaiThi
            // 
            lsvDanhSachBaiThi.Columns.AddRange(new ColumnHeader[] { colMaDe, colMonHoc, colKyThi, colNamHoc, colSoCauHoi, colThoiGian });
            lsvDanhSachBaiThi.FullRowSelect = true;
            lsvDanhSachBaiThi.GridLines = true;
            lsvDanhSachBaiThi.Location = new Point(35, 191);
            lsvDanhSachBaiThi.Name = "lsvDanhSachBaiThi";
            lsvDanhSachBaiThi.Size = new Size(673, 193);
            lsvDanhSachBaiThi.TabIndex = 6;
            lsvDanhSachBaiThi.UseCompatibleStateImageBehavior = false;
            lsvDanhSachBaiThi.View = View.Details;
            // 
            // txtNgayThi
            // 
            txtNgayThi.Location = new Point(340, 62);
            txtNgayThi.Name = "txtNgayThi";
            txtNgayThi.ReadOnly = true;
            txtNgayThi.Size = new Size(100, 23);
            txtNgayThi.TabIndex = 5;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 65);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 3;
            label2.Text = "Chọn Kỳ thi:";
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
            // txtThongTinSV
            // 
            txtThongTinSV.Location = new Point(119, 24);
            txtThongTinSV.Name = "txtThongTinSV";
            txtThongTinSV.ReadOnly = true;
            txtThongTinSV.Size = new Size(100, 23);
            txtThongTinSV.TabIndex = 1;
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
            // tabPage2
            // 
            tabPage2.Controls.Add(btnXuatPDFBangDiem);
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
            tabPage2.Size = new Size(767, 419);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "2. Lịch sử & Kết quả Bài thi";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
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
            // rtxtXemChiTietCau
            // 
            rtxtXemChiTietCau.Location = new Point(23, 309);
            rtxtXemChiTietCau.Name = "rtxtXemChiTietCau";
            rtxtXemChiTietCau.Size = new Size(711, 85);
            rtxtXemChiTietCau.TabIndex = 10;
            rtxtXemChiTietCau.Text = "";
            // 
            // lsvChiTietBaiLam
            // 
            lsvChiTietBaiLam.Columns.AddRange(new ColumnHeader[] { columnHeader8, columnHeader9, columnHeader10, columnHeader11 });
            lsvChiTietBaiLam.FullRowSelect = true;
            lsvChiTietBaiLam.GridLines = true;
            lsvChiTietBaiLam.Location = new Point(23, 198);
            lsvChiTietBaiLam.Name = "lsvChiTietBaiLam";
            lsvChiTietBaiLam.Size = new Size(711, 90);
            lsvChiTietBaiLam.TabIndex = 9;
            lsvChiTietBaiLam.UseCompatibleStateImageBehavior = false;
            lsvChiTietBaiLam.View = View.Details;
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
            // txtSoCauSai
            // 
            txtSoCauSai.Location = new Point(470, 129);
            txtSoCauSai.Name = "txtSoCauSai";
            txtSoCauSai.ReadOnly = true;
            txtSoCauSai.Size = new Size(100, 23);
            txtSoCauSai.TabIndex = 7;
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
            // txtSoCauDung
            // 
            txtSoCauDung.Location = new Point(296, 129);
            txtSoCauDung.Name = "txtSoCauDung";
            txtSoCauDung.ReadOnly = true;
            txtSoCauDung.Size = new Size(100, 23);
            txtSoCauDung.TabIndex = 5;
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
            // txtTongDiem
            // 
            txtTongDiem.Location = new Point(94, 129);
            txtTongDiem.Name = "txtTongDiem";
            txtTongDiem.ReadOnly = true;
            txtTongDiem.Size = new Size(86, 23);
            txtTongDiem.TabIndex = 3;
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 8);
            label4.Name = "label4";
            label4.Size = new Size(168, 15);
            label4.TabIndex = 1;
            label4.Text = "Bảng lịch sử các Bài thi đã nộp";
            // 
            // lsvLichSuBaiThi
            // 
            lsvLichSuBaiThi.Columns.AddRange(new ColumnHeader[] { colMaBaiThi, columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7 });
            lsvLichSuBaiThi.FullRowSelect = true;
            lsvLichSuBaiThi.GridLines = true;
            lsvLichSuBaiThi.Location = new Point(26, 26);
            lsvLichSuBaiThi.Name = "lsvLichSuBaiThi";
            lsvLichSuBaiThi.Size = new Size(708, 97);
            lsvLichSuBaiThi.TabIndex = 0;
            lsvLichSuBaiThi.UseCompatibleStateImageBehavior = false;
            lsvLichSuBaiThi.View = View.Details;
            // 
            // btnDangXuatSV
            // 
            btnDangXuatSV.Location = new Point(706, 12);
            btnDangXuatSV.Name = "btnDangXuatSV";
            btnDangXuatSV.Size = new Size(75, 23);
            btnDangXuatSV.TabIndex = 1;
            btnDangXuatSV.Text = "Đăng xuất";
            btnDangXuatSV.UseVisualStyleBackColor = true;
            // 
            // colMaDe
            // 
            colMaDe.Text = "Mã đề";
            // 
            // colMonHoc
            // 
            colMonHoc.Text = "Môn học";
            colMonHoc.Width = 140;
            // 
            // colKyThi
            // 
            colKyThi.Text = "Kỳ thi";
            // 
            // colNamHoc
            // 
            colNamHoc.Text = "Năm học";
            colNamHoc.Width = 100;
            // 
            // colSoCauHoi
            // 
            colSoCauHoi.Text = "Số câu hỏi";
            // 
            // colThoiGian
            // 
            colThoiGian.Text = "Thời gian (phút)";
            colThoiGian.Width = 80;
            // 
            // cmbNamHocSV
            // 
            cmbNamHocSV.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNamHocSV.FormattingEnabled = true;
            cmbNamHocSV.Location = new Point(119, 106);
            cmbNamHocSV.Name = "cmbNamHocSV";
            cmbNamHocSV.Size = new Size(100, 23);
            cmbNamHocSV.TabIndex = 8;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(35, 109);
            label10.Name = "label10";
            label10.Size = new Size(59, 15);
            label10.TabIndex = 9;
            label10.Text = "Năm học:";
            // 
            // cmbMonThiSV
            // 
            cmbMonThiSV.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMonThiSV.FormattingEnabled = true;
            cmbMonThiSV.Location = new Point(119, 145);
            cmbMonThiSV.Name = "cmbMonThiSV";
            cmbMonThiSV.Size = new Size(100, 23);
            cmbMonThiSV.TabIndex = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(35, 148);
            label11.Name = "label11";
            label11.Size = new Size(35, 15);
            label11.TabIndex = 11;
            label11.Text = "Môn:";
            // 
            // btnTaiLaiDeThi
            // 
            btnTaiLaiDeThi.Location = new Point(586, 162);
            btnTaiLaiDeThi.Name = "btnTaiLaiDeThi";
            btnTaiLaiDeThi.Size = new Size(122, 23);
            btnTaiLaiDeThi.TabIndex = 12;
            btnTaiLaiDeThi.Text = "Làm mới danh sách";
            btnTaiLaiDeThi.UseVisualStyleBackColor = true;
            // 
            // txtHoTenSV
            // 
            txtHoTenSV.Location = new Point(340, 24);
            txtHoTenSV.Name = "txtHoTenSV";
            txtHoTenSV.ReadOnly = true;
            txtHoTenSV.Size = new Size(223, 23);
            txtHoTenSV.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(273, 27);
            label12.Name = "label12";
            label12.Size = new Size(61, 15);
            label12.TabIndex = 14;
            label12.Text = "Họ và tên:";
            // 
            // colMaBaiThi
            // 
            colMaBaiThi.Text = "Mã bài thi";
            colMaBaiThi.Width = 75;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Mã đề";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Môn học";
            columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Thời gian bắt đầu";
            columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Thời gian kết thúc";
            columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Số câu đúng";
            columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Số câu sai";
            columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Tổng điểm";
            columnHeader7.Width = 80;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Mã đề";
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "Mã câu hỏi";
            columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "Đáp án đã chọn";
            columnHeader10.Width = 100;
            // 
            // columnHeader11
            // 
            columnHeader11.Text = "Kết quả";
            // 
            // btnXuatPDFBangDiem
            // 
            btnXuatPDFBangDiem.Location = new Point(590, 128);
            btnXuatPDFBangDiem.Name = "btnXuatPDFBangDiem";
            btnXuatPDFBangDiem.Size = new Size(144, 23);
            btnXuatPDFBangDiem.TabIndex = 12;
            btnXuatPDFBangDiem.Text = "In Bảng Điểm (PDF)";
            btnXuatPDFBangDiem.UseVisualStyleBackColor = true;
            // 
            // FormSinhVien
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 487);
            Controls.Add(btnDangXuatSV);
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
        private ColumnHeader colMaDe;
        private ColumnHeader colMonHoc;
        private ColumnHeader colKyThi;
        private ColumnHeader colNamHoc;
        private ColumnHeader colSoCauHoi;
        private ColumnHeader colThoiGian;
        private Button btnDangXuatSV;
        private TextBox txtHoTenSV;
        private Button btnTaiLaiDeThi;
        private Label label11;
        private ComboBox cmbMonThiSV;
        private Label label10;
        private ComboBox cmbNamHocSV;
        private Label label12;
        private ColumnHeader colMaBaiThi;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Button btnXuatPDFBangDiem;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
    }
}