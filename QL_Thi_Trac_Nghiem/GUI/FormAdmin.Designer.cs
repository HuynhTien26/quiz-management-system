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
            colKhoa = new ColumnHeader();
            colMon = new ColumnHeader();
            colNgaySinh = new ColumnHeader();
            colLop = new ColumnHeader();
            colVaiTro = new ColumnHeader();
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
            btnCapNhat = new Button();
            panel1 = new Panel();
            btnChonNhanSu = new Button();
            tabMain = new TabControl();
            tabDanhSach = new TabPage();
            tabTienDo = new TabPage();
            lsvKetQuaMonHoc = new ListView();
            colMaMon = new ColumnHeader();
            colTenMon = new ColumnHeader();
            colTinChi = new ColumnHeader();
            colDiemThi = new ColumnHeader();
            colDiemChu = new ColumnHeader();
            colKetQua = new ColumnHeader();
            label11 = new Label();
            label10 = new Label();
            txtDiemTBXepLoai = new TextBox();
            txtTongTinChi = new TextBox();
            tabThongKeGV = new TabPage();
            lsvThongKeDeThi = new ListView();
            colMaDe = new ColumnHeader();
            colMa_Mon = new ColumnHeader();
            colSoLuongCau = new ColumnHeader();
            colThoiGianLamBai = new ColumnHeader();
            label13 = new Label();
            label12 = new Label();
            txtTongSoCauHoi = new TextBox();
            txtTongSoDe = new TextBox();
            cboHocKySV = new ComboBox();
            cboNamHocSV = new ComboBox();
            label14 = new Label();
            label15 = new Label();
            cboHocKyGV = new ComboBox();
            cboNamHocGV = new ComboBox();
            label16 = new Label();
            label17 = new Label();
            panel1.SuspendLayout();
            tabMain.SuspendLayout();
            tabDanhSach.SuspendLayout();
            tabTienDo.SuspendLayout();
            tabThongKeGV.SuspendLayout();
            SuspendLayout();
            // 
            // lsvDanhSach
            // 
            lsvDanhSach.Columns.AddRange(new ColumnHeader[] { colMa, colHo, colTen, colGioi, colKhoa, colMon, colNgaySinh, colLop, colVaiTro });
            lsvDanhSach.FullRowSelect = true;
            lsvDanhSach.GridLines = true;
            lsvDanhSach.Location = new Point(6, 6);
            lsvDanhSach.Name = "lsvDanhSach";
            lsvDanhSach.Size = new Size(645, 330);
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
            // colKhoa
            // 
            colKhoa.Text = "Khoa";
            colKhoa.Width = 90;
            // 
            // colMon
            // 
            colMon.Text = "Môn";
            colMon.Width = 100;
            // 
            // colNgaySinh
            // 
            colNgaySinh.Text = "Ngày sinh";
            colNgaySinh.Width = 80;
            // 
            // colLop
            // 
            colLop.Text = "Lớp";
            // 
            // colVaiTro
            // 
            colVaiTro.Text = "Vai trò";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 19);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 1;
            label1.Text = "Mã:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 55);
            label2.Name = "label2";
            label2.Size = new Size(26, 15);
            label2.TabIndex = 2;
            label2.Text = "Họ:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(179, 55);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 3;
            label3.Text = "Tên:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 121);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 4;
            label4.Text = "Giới:";
            // 
            // rdoNam
            // 
            rdoNam.AutoSize = true;
            rdoNam.Location = new Point(16, 139);
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
            rdoNu.Location = new Point(106, 139);
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
            label5.Location = new Point(364, 76);
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
            cmbKhoa.Location = new Point(365, 94);
            cmbKhoa.Name = "cmbKhoa";
            cmbKhoa.Size = new Size(121, 23);
            cmbKhoa.TabIndex = 11;
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(81, 88);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(92, 23);
            dtpNgaySinh.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 94);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 13;
            label7.Text = "Ngày sinh:";
            // 
            // clbMon
            // 
            clbMon.FormattingEnabled = true;
            clbMon.Location = new Point(580, 23);
            clbMon.Name = "clbMon";
            clbMon.Size = new Size(181, 148);
            clbMon.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(580, 5);
            label8.Name = "label8";
            label8.Size = new Size(35, 15);
            label8.TabIndex = 16;
            label8.Text = "Môn:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(365, 127);
            label9.Name = "label9";
            label9.Size = new Size(30, 15);
            label9.TabIndex = 17;
            label9.Text = "Lớp:";
            // 
            // cmbLop
            // 
            cmbLop.FormattingEnabled = true;
            cmbLop.Location = new Point(365, 145);
            cmbLop.Name = "cmbLop";
            cmbLop.Size = new Size(121, 23);
            cmbLop.TabIndex = 18;
            // 
            // txtMa
            // 
            txtMa.Location = new Point(47, 16);
            txtMa.Name = "txtMa";
            txtMa.Size = new Size(126, 23);
            txtMa.TabIndex = 19;
            // 
            // txtHo
            // 
            txtHo.Location = new Point(47, 52);
            txtHo.Name = "txtHo";
            txtHo.Size = new Size(126, 23);
            txtHo.TabIndex = 20;
            // 
            // txtTen
            // 
            txtTen.Location = new Point(214, 52);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(121, 23);
            txtTen.TabIndex = 21;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(670, 11);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(75, 23);
            btnThem.TabIndex = 22;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(671, 69);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 23);
            btnXoa.TabIndex = 23;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnCapNhat
            // 
            btnCapNhat.Location = new Point(670, 40);
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
            panel1.Location = new Point(365, 16);
            panel1.Name = "panel1";
            panel1.Size = new Size(209, 54);
            panel1.TabIndex = 26;
            // 
            // btnChonNhanSu
            // 
            btnChonNhanSu.Location = new Point(179, 16);
            btnChonNhanSu.Name = "btnChonNhanSu";
            btnChonNhanSu.Size = new Size(42, 23);
            btnChonNhanSu.TabIndex = 28;
            btnChonNhanSu.Text = "...";
            btnChonNhanSu.UseVisualStyleBackColor = true;
            btnChonNhanSu.Click += btnChonNhanSu_Click;
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabDanhSach);
            tabMain.Controls.Add(tabTienDo);
            tabMain.Controls.Add(tabThongKeGV);
            tabMain.Location = new Point(12, 178);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(785, 434);
            tabMain.TabIndex = 29;
            // 
            // tabDanhSach
            // 
            tabDanhSach.Controls.Add(lsvDanhSach);
            tabDanhSach.Controls.Add(btnThem);
            tabDanhSach.Controls.Add(btnCapNhat);
            tabDanhSach.Controls.Add(btnXoa);
            tabDanhSach.Location = new Point(4, 24);
            tabDanhSach.Name = "tabDanhSach";
            tabDanhSach.Padding = new Padding(3);
            tabDanhSach.Size = new Size(777, 406);
            tabDanhSach.TabIndex = 2;
            tabDanhSach.Text = "Hồ sơ ";
            tabDanhSach.UseVisualStyleBackColor = true;
            // 
            // tabTienDo
            // 
            tabTienDo.Controls.Add(label15);
            tabTienDo.Controls.Add(label14);
            tabTienDo.Controls.Add(cboNamHocSV);
            tabTienDo.Controls.Add(cboHocKySV);
            tabTienDo.Controls.Add(lsvKetQuaMonHoc);
            tabTienDo.Controls.Add(label11);
            tabTienDo.Controls.Add(label10);
            tabTienDo.Controls.Add(txtDiemTBXepLoai);
            tabTienDo.Controls.Add(txtTongTinChi);
            tabTienDo.Location = new Point(4, 24);
            tabTienDo.Name = "tabTienDo";
            tabTienDo.Padding = new Padding(3);
            tabTienDo.Size = new Size(777, 406);
            tabTienDo.TabIndex = 1;
            tabTienDo.Text = "Tiến độ Sinh viên";
            tabTienDo.UseVisualStyleBackColor = true;
            // 
            // lsvKetQuaMonHoc
            // 
            lsvKetQuaMonHoc.Columns.AddRange(new ColumnHeader[] { colMaMon, colTenMon, colTinChi, colDiemThi, colDiemChu, colKetQua });
            lsvKetQuaMonHoc.FullRowSelect = true;
            lsvKetQuaMonHoc.GridLines = true;
            lsvKetQuaMonHoc.Location = new Point(25, 129);
            lsvKetQuaMonHoc.Name = "lsvKetQuaMonHoc";
            lsvKetQuaMonHoc.Size = new Size(540, 255);
            lsvKetQuaMonHoc.TabIndex = 4;
            lsvKetQuaMonHoc.UseCompatibleStateImageBehavior = false;
            lsvKetQuaMonHoc.View = View.Details;
            // 
            // colMaMon
            // 
            colMaMon.Text = "Mã môn";
            // 
            // colTenMon
            // 
            colTenMon.Text = "Tên môn";
            // 
            // colTinChi
            // 
            colTinChi.Text = "Tín chỉ";
            // 
            // colDiemThi
            // 
            colDiemThi.Text = "Điểm thi";
            // 
            // colDiemChu
            // 
            colDiemChu.Text = "Điểm chữ";
            colDiemChu.Width = 70;
            // 
            // colKetQua
            // 
            colKetQua.Text = "Kết quả";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(25, 31);
            label11.Name = "label11";
            label11.Size = new Size(94, 15);
            label11.TabIndex = 3;
            label11.Text = "Tổng tín chỉ đạt:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(220, 26);
            label10.Name = "label10";
            label10.Size = new Size(52, 15);
            label10.TabIndex = 2;
            label10.Text = "Xếp loại:";
            // 
            // txtDiemTBXepLoai
            // 
            txtDiemTBXepLoai.Location = new Point(278, 20);
            txtDiemTBXepLoai.Name = "txtDiemTBXepLoai";
            txtDiemTBXepLoai.ReadOnly = true;
            txtDiemTBXepLoai.Size = new Size(84, 23);
            txtDiemTBXepLoai.TabIndex = 1;
            // 
            // txtTongTinChi
            // 
            txtTongTinChi.Location = new Point(125, 23);
            txtTongTinChi.Name = "txtTongTinChi";
            txtTongTinChi.ReadOnly = true;
            txtTongTinChi.Size = new Size(89, 23);
            txtTongTinChi.TabIndex = 0;
            // 
            // tabThongKeGV
            // 
            tabThongKeGV.Controls.Add(label17);
            tabThongKeGV.Controls.Add(label16);
            tabThongKeGV.Controls.Add(cboNamHocGV);
            tabThongKeGV.Controls.Add(cboHocKyGV);
            tabThongKeGV.Controls.Add(lsvThongKeDeThi);
            tabThongKeGV.Controls.Add(label13);
            tabThongKeGV.Controls.Add(label12);
            tabThongKeGV.Controls.Add(txtTongSoCauHoi);
            tabThongKeGV.Controls.Add(txtTongSoDe);
            tabThongKeGV.Location = new Point(4, 24);
            tabThongKeGV.Name = "tabThongKeGV";
            tabThongKeGV.Padding = new Padding(3);
            tabThongKeGV.Size = new Size(777, 406);
            tabThongKeGV.TabIndex = 3;
            tabThongKeGV.Text = "Thống kê Giáo viên";
            tabThongKeGV.UseVisualStyleBackColor = true;
            // 
            // lsvThongKeDeThi
            // 
            lsvThongKeDeThi.Columns.AddRange(new ColumnHeader[] { colMaDe, colMa_Mon, colSoLuongCau, colThoiGianLamBai });
            lsvThongKeDeThi.FullRowSelect = true;
            lsvThongKeDeThi.GridLines = true;
            lsvThongKeDeThi.Location = new Point(15, 122);
            lsvThongKeDeThi.Name = "lsvThongKeDeThi";
            lsvThongKeDeThi.Size = new Size(711, 267);
            lsvThongKeDeThi.TabIndex = 4;
            lsvThongKeDeThi.UseCompatibleStateImageBehavior = false;
            lsvThongKeDeThi.View = View.Details;
            // 
            // colMaDe
            // 
            colMaDe.Text = "Mã đề";
            // 
            // colMa_Mon
            // 
            colMa_Mon.Text = "Mã môn";
            // 
            // colSoLuongCau
            // 
            colSoLuongCau.Text = "Số lượng câu";
            colSoLuongCau.Width = 90;
            // 
            // colThoiGianLamBai
            // 
            colThoiGianLamBai.Text = "Thời gian làm bài";
            colThoiGianLamBai.Width = 130;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(277, 29);
            label13.Name = "label13";
            label13.Size = new Size(150, 15);
            label13.TabIndex = 3;
            label13.Text = "Tổng số câu hỏi đóng góp:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(15, 29);
            label12.Name = "label12";
            label12.Size = new Size(105, 15);
            label12.TabIndex = 2;
            label12.Text = "Tổng số đề đã tạo:";
            // 
            // txtTongSoCauHoi
            // 
            txtTongSoCauHoi.Location = new Point(433, 26);
            txtTongSoCauHoi.Name = "txtTongSoCauHoi";
            txtTongSoCauHoi.ReadOnly = true;
            txtTongSoCauHoi.Size = new Size(104, 23);
            txtTongSoCauHoi.TabIndex = 1;
            // 
            // txtTongSoDe
            // 
            txtTongSoDe.Location = new Point(126, 26);
            txtTongSoDe.Name = "txtTongSoDe";
            txtTongSoDe.ReadOnly = true;
            txtTongSoDe.Size = new Size(96, 23);
            txtTongSoDe.TabIndex = 0;
            // 
            // cboHocKySV
            // 
            cboHocKySV.FormattingEnabled = true;
            cboHocKySV.Location = new Point(126, 66);
            cboHocKySV.Name = "cboHocKySV";
            cboHocKySV.Size = new Size(88, 23);
            cboHocKySV.TabIndex = 5;
            // 
            // cboNamHocSV
            // 
            cboNamHocSV.FormattingEnabled = true;
            cboNamHocSV.Location = new Point(280, 67);
            cboNamHocSV.Name = "cboNamHocSV";
            cboNamHocSV.Size = new Size(81, 23);
            cboNamHocSV.TabIndex = 6;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(31, 70);
            label14.Name = "label14";
            label14.Size = new Size(47, 15);
            label14.TabIndex = 7;
            label14.Text = "Học kỳ:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(220, 70);
            label15.Name = "label15";
            label15.Size = new Size(59, 15);
            label15.TabIndex = 8;
            label15.Text = "Năm học:";
            // 
            // cboHocKyGV
            // 
            cboHocKyGV.FormattingEnabled = true;
            cboHocKyGV.Location = new Point(126, 66);
            cboHocKyGV.Name = "cboHocKyGV";
            cboHocKyGV.Size = new Size(96, 23);
            cboHocKyGV.TabIndex = 5;
            // 
            // cboNamHocGV
            // 
            cboNamHocGV.FormattingEnabled = true;
            cboNamHocGV.Location = new Point(433, 66);
            cboNamHocGV.Name = "cboNamHocGV";
            cboNamHocGV.Size = new Size(104, 23);
            cboNamHocGV.TabIndex = 6;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(15, 69);
            label16.Name = "label16";
            label16.Size = new Size(47, 15);
            label16.TabIndex = 7;
            label16.Text = "Học kỳ:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(277, 69);
            label17.Name = "label17";
            label17.Size = new Size(59, 15);
            label17.TabIndex = 8;
            label17.Text = "Năm học:";
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1543, 719);
            Controls.Add(tabMain);
            Controls.Add(btnChonNhanSu);
            Controls.Add(clbMon);
            Controls.Add(cmbKhoa);
            Controls.Add(label5);
            Controls.Add(dtpNgaySinh);
            Controls.Add(panel1);
            Controls.Add(rdoNu);
            Controls.Add(label7);
            Controls.Add(txtTen);
            Controls.Add(rdoNam);
            Controls.Add(label4);
            Controls.Add(txtHo);
            Controls.Add(label8);
            Controls.Add(txtMa);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label9);
            Controls.Add(cmbLop);
            Controls.Add(label2);
            Name = "FormAdmin";
            Text = "FormAdmin";
            Load += FormAdmin_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabMain.ResumeLayout(false);
            tabDanhSach.ResumeLayout(false);
            tabTienDo.ResumeLayout(false);
            tabTienDo.PerformLayout();
            tabThongKeGV.ResumeLayout(false);
            tabThongKeGV.PerformLayout();
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
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoGiaoVien;
        private Button btnChonNhanSu;
        private TabControl tabMain;

       
        private TabPage tabTienDo;
        private TabPage tabDanhSach;
        private TabPage tabThongKeGV;
        private TextBox txtDiemTBXepLoai;
        private TextBox txtTongTinChi;
        private Label label10;
        private Label label11;
        private ColumnHeader colVaiTro;
        private ListView lsvKetQuaMonHoc;
        private ColumnHeader colMaMon;
        private ColumnHeader colTenMon;
        private ColumnHeader colTinChi;
        private ColumnHeader colDiemThi;
        private ColumnHeader colKetQua;
        private ColumnHeader colDiemChu;
        private Label label13;
        private Label label12;
        private TextBox txtTongSoCauHoi;
        private TextBox txtTongSoDe;
        private ListView lsvThongKeDeThi;
        private ColumnHeader colMaDe;
        private ColumnHeader colMa_Mon;
        private ColumnHeader colSoLuongCau;
        private ColumnHeader colThoiGianLamBai;
        private Label label15;
        private Label label14;
        private ComboBox cboNamHocSV;
        private ComboBox cboHocKySV;
        private ComboBox cboHocKyGV;
        private Label label17;
        private Label label16;
        private ComboBox cboNamHocGV;
    }
}