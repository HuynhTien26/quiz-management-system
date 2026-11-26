namespace QL_Thi_Trac_Nghiem.GUI
{
    partial class FormLamBaiThi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            txtTenMonThi = new TextBox();
            label2 = new Label();
            txtMaDeThi = new TextBox();
            label3 = new Label();
            timerDemNguoc = new System.Windows.Forms.Timer(components);
            txtNoiDungCauHoi = new TextBox();
            label4 = new Label();
            groupBox1 = new GroupBox();
            rdoChonA = new RadioButton();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            rdoChonB = new RadioButton();
            rdoChonC = new RadioButton();
            rdoChonD = new RadioButton();
            btnCauTruoc = new Button();
            btnCauTiep = new Button();
            btnNopBai = new Button();
            lblCauSo = new Label();
            flpDanhSachCau = new FlowLayoutPanel();
            label9 = new Label();
            lblThoiGianConLai = new Label();
            lblTienDoLamBai = new Label();
            pnlChuThich = new Panel();
            lblChuaLam = new Label();
            lblDaLam = new Label();
            lblDangChon = new Label();

            groupBox1.SuspendLayout();
            pnlChuThich.SuspendLayout();
            SuspendLayout();

            // label1
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label1.Location = new Point(24, 20);
            label1.Name = "label1";
            label1.Size = new Size(46, 17);
            label1.TabIndex = 0;
            label1.Text = "Môn:";

            // txtTenMonThi
            txtTenMonThi.BackColor = Color.White;
            txtTenMonThi.Font = new Font("Segoe UI", 9.5F);
            txtTenMonThi.Location = new Point(76, 17);
            txtTenMonThi.Name = "txtTenMonThi";
            txtTenMonThi.ReadOnly = true;
            txtTenMonThi.Size = new Size(210, 24);
            txtTenMonThi.TabIndex = 1;

            // label2
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label2.Location = new Point(304, 20);
            label2.Name = "label2";
            label2.Size = new Size(76, 17);
            label2.TabIndex = 2;
            label2.Text = "Mã đề thi:";

            // txtMaDeThi
            txtMaDeThi.BackColor = Color.White;
            txtMaDeThi.Font = new Font("Segoe UI", 9.5F);
            txtMaDeThi.Location = new Point(386, 17);
            txtMaDeThi.Name = "txtMaDeThi";
            txtMaDeThi.ReadOnly = true;
            txtMaDeThi.Size = new Size(110, 24);
            txtMaDeThi.TabIndex = 3;

            // label3
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label3.Location = new Point(525, 20);
            label3.Name = "label3";
            label3.Size = new Size(118, 17);
            label3.TabIndex = 4;
            label3.Text = "Thời gian còn lại:";

            // lblThoiGianConLai
            lblThoiGianConLai.AutoSize = true;
            lblThoiGianConLai.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblThoiGianConLai.ForeColor = Color.DarkBlue;
            lblThoiGianConLai.Location = new Point(648, 16);
            lblThoiGianConLai.Name = "lblThoiGianConLai";
            lblThoiGianConLai.Size = new Size(62, 21);
            lblThoiGianConLai.TabIndex = 14;
            lblThoiGianConLai.Text = "45:00";

            // timerDemNguoc
            timerDemNguoc.Interval = 1000;

            // label4
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label4.Location = new Point(24, 58);
            label4.Name = "label4";
            label4.Size = new Size(84, 17);
            label4.TabIndex = 6;
            label4.Text = "Câu hỏi số:";

            // lblCauSo
            lblCauSo.AutoSize = true;
            lblCauSo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCauSo.ForeColor = Color.DodgerBlue;
            lblCauSo.Location = new Point(114, 58);
            lblCauSo.Name = "lblCauSo";
            lblCauSo.Size = new Size(52, 17);
            lblCauSo.TabIndex = 11;
            lblCauSo.Text = "1 / 1";

            // txtNoiDungCauHoi
            txtNoiDungCauHoi.BackColor = Color.White;
            txtNoiDungCauHoi.Font = new Font("Segoe UI", 10F);
            txtNoiDungCauHoi.Location = new Point(24, 82);
            txtNoiDungCauHoi.Multiline = true;
            txtNoiDungCauHoi.Name = "txtNoiDungCauHoi";
            txtNoiDungCauHoi.ReadOnly = true;
            txtNoiDungCauHoi.ScrollBars = ScrollBars.Vertical;
            txtNoiDungCauHoi.Size = new Size(472, 95);
            txtNoiDungCauHoi.TabIndex = 5;

            // groupBox1
            groupBox1.Controls.Add(rdoChonA);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(rdoChonB);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(rdoChonC);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(rdoChonD);
            groupBox1.Controls.Add(label8);
            groupBox1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            groupBox1.Location = new Point(24, 189);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(472, 225);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chọn đáp án:";

            // label5 (A)
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label5.Location = new Point(14, 34);
            label5.Name = "label5";
            label5.Size = new Size(21, 17);
            label5.TabIndex = 1;
            label5.Text = "A.";

            // rdoChonA
            rdoChonA.AutoSize = true;
            rdoChonA.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            rdoChonA.Location = new Point(40, 33);
            rdoChonA.MaximumSize = new Size(418, 0);
            rdoChonA.Name = "rdoChonA";
            rdoChonA.Size = new Size(105, 21);
            rdoChonA.TabIndex = 0;
            rdoChonA.TabStop = true;
            rdoChonA.Text = "Đáp án A";
            rdoChonA.UseVisualStyleBackColor = true;

            // label6 (B)
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label6.Location = new Point(14, 81);
            label6.Name = "label6";
            label6.Size = new Size(20, 17);
            label6.TabIndex = 2;
            label6.Text = "B.";

            // rdoChonB
            rdoChonB.AutoSize = true;
            rdoChonB.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            rdoChonB.Location = new Point(40, 80);
            rdoChonB.MaximumSize = new Size(418, 0);
            rdoChonB.Name = "rdoChonB";
            rdoChonB.Size = new Size(105, 21);
            rdoChonB.TabIndex = 5;
            rdoChonB.TabStop = true;
            rdoChonB.Text = "Đáp án B";
            rdoChonB.UseVisualStyleBackColor = true;

            // label7 (C)
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label7.Location = new Point(14, 128);
            label7.Name = "label7";
            label7.Size = new Size(21, 17);
            label7.TabIndex = 3;
            label7.Text = "C.";

            // rdoChonC
            rdoChonC.AutoSize = true;
            rdoChonC.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            rdoChonC.Location = new Point(40, 127);
            rdoChonC.MaximumSize = new Size(418, 0);
            rdoChonC.Name = "rdoChonC";
            rdoChonC.Size = new Size(105, 21);
            rdoChonC.TabIndex = 6;
            rdoChonC.TabStop = true;
            rdoChonC.Text = "Đáp án C";
            rdoChonC.UseVisualStyleBackColor = true;

            // label8 (D)
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label8.Location = new Point(14, 175);
            label8.Name = "label8";
            label8.Size = new Size(21, 17);
            label8.TabIndex = 4;
            label8.Text = "D.";

            // rdoChonD
            rdoChonD.AutoSize = true;
            rdoChonD.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            rdoChonD.Location = new Point(40, 174);
            rdoChonD.MaximumSize = new Size(418, 0);
            rdoChonD.Name = "rdoChonD";
            rdoChonD.Size = new Size(106, 21);
            rdoChonD.TabIndex = 7;
            rdoChonD.TabStop = true;
            rdoChonD.Text = "Đáp án D";
            rdoChonD.UseVisualStyleBackColor = true;

            // btnCauTruoc
            btnCauTruoc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCauTruoc.Location = new Point(24, 430);
            btnCauTruoc.Name = "btnCauTruoc";
            btnCauTruoc.Size = new Size(110, 34);
            btnCauTruoc.TabIndex = 8;
            btnCauTruoc.Text = "← Câu trước";
            btnCauTruoc.UseVisualStyleBackColor = true;

            // btnCauTiep
            btnCauTiep.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCauTiep.Location = new Point(144, 430);
            btnCauTiep.Name = "btnCauTiep";
            btnCauTiep.Size = new Size(110, 34);
            btnCauTiep.TabIndex = 9;
            btnCauTiep.Text = "Câu tiếp →";
            btnCauTiep.UseVisualStyleBackColor = true;

            // btnNopBai
            btnNopBai.BackColor = Color.IndianRed;
            btnNopBai.FlatStyle = FlatStyle.Flat;
            btnNopBai.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnNopBai.ForeColor = Color.White;
            btnNopBai.Location = new Point(376, 430);
            btnNopBai.Name = "btnNopBai";
            btnNopBai.Size = new Size(120, 34);
            btnNopBai.TabIndex = 10;
            btnNopBai.Text = "Nộp bài";
            btnNopBai.UseVisualStyleBackColor = false;

            // label9
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label9.Location = new Point(514, 58);
            label9.Name = "label9";
            label9.Size = new Size(156, 17);
            label9.TabIndex = 13;
            label9.Text = "Danh sách câu hỏi:";

            // lblTienDoLamBai
            lblTienDoLamBai.AutoSize = true;
            lblTienDoLamBai.Font = new Font("Segoe UI", 9F);
            lblTienDoLamBai.ForeColor = Color.DimGray;
            lblTienDoLamBai.Location = new Point(514, 82);
            lblTienDoLamBai.Name = "lblTienDoLamBai";
            lblTienDoLamBai.Size = new Size(112, 15);
            lblTienDoLamBai.TabIndex = 15;
            lblTienDoLamBai.Text = "Đã trả lời: 0 / 0 câu";

            // flpDanhSachCau
            flpDanhSachCau.AutoScroll = true;
            flpDanhSachCau.BackColor = Color.White;
            flpDanhSachCau.BorderStyle = BorderStyle.FixedSingle;
            flpDanhSachCau.Location = new Point(514, 105);
            flpDanhSachCau.Name = "flpDanhSachCau";
            flpDanhSachCau.Size = new Size(276, 250);
            flpDanhSachCau.TabIndex = 12;

            // pnlChuThich
            pnlChuThich.Controls.Add(lblDangChon);
            pnlChuThich.Controls.Add(lblDaLam);
            pnlChuThich.Controls.Add(lblChuaLam);
            pnlChuThich.Location = new Point(514, 365);
            pnlChuThich.Name = "pnlChuThich";
            pnlChuThich.Size = new Size(276, 50);
            pnlChuThich.TabIndex = 16;

            // lblDangChon
            lblDangChon.AutoSize = true;
            lblDangChon.BackColor = Color.DodgerBlue;
            lblDangChon.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDangChon.ForeColor = Color.White;
            lblDangChon.Location = new Point(4, 6);
            lblDangChon.Name = "lblDangChon";
            lblDangChon.Padding = new Padding(4, 2, 4, 2);
            lblDangChon.Size = new Size(80, 17);
            lblDangChon.Text = "Đang chọn";

            // lblDaLam
            lblDaLam.AutoSize = true;
            lblDaLam.BackColor = Color.LightGreen;
            lblDaLam.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblDaLam.ForeColor = Color.Black;
            lblDaLam.Location = new Point(94, 6);
            lblDaLam.Name = "lblDaLam";
            lblDaLam.Padding = new Padding(4, 2, 4, 2);
            lblDaLam.Size = new Size(61, 17);
            lblDaLam.Text = "Đã làm";

            // lblChuaLam
            lblChuaLam.AutoSize = true;
            lblChuaLam.BackColor = Color.WhiteSmoke;
            lblChuaLam.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblChuaLam.ForeColor = Color.Black;
            lblChuaLam.Location = new Point(165, 6);
            lblChuaLam.Name = "lblChuaLam";
            lblChuaLam.Padding = new Padding(4, 2, 4, 2);
            lblChuaLam.Size = new Size(71, 17);
            lblChuaLam.Text = "Chưa làm";

            // FormLamBaiThi
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 488);
            Controls.Add(pnlChuThich);
            Controls.Add(lblTienDoLamBai);
            Controls.Add(lblThoiGianConLai);
            Controls.Add(label9);
            Controls.Add(flpDanhSachCau);
            Controls.Add(lblCauSo);
            Controls.Add(btnNopBai);
            Controls.Add(btnCauTiep);
            Controls.Add(btnCauTruoc);
            Controls.Add(groupBox1);
            Controls.Add(label4);
            Controls.Add(txtNoiDungCauHoi);
            Controls.Add(label3);
            Controls.Add(txtMaDeThi);
            Controls.Add(label2);
            Controls.Add(txtTenMonThi);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormLamBaiThi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phòng Thi Trực Tuyến - Làm Bài Thi";

            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            pnlChuThich.ResumeLayout(false);
            pnlChuThich.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtTenMonThi;
        private Label label2;
        private TextBox txtMaDeThi;
        private Label label3;
        private System.Windows.Forms.Timer timerDemNguoc;
        private TextBox txtNoiDungCauHoi;
        private Label label4;
        private GroupBox groupBox1;
        private RadioButton rdoChonD;
        private RadioButton rdoChonC;
        private RadioButton rdoChonB;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private RadioButton rdoChonA;
        private Button btnCauTruoc;
        private Button btnCauTiep;
        private Button btnNopBai;
        private Label lblCauSo;
        private FlowLayoutPanel flpDanhSachCau;
        private Label label9;
        private Label lblThoiGianConLai;
        private Label lblTienDoLamBai;
        private Panel pnlChuThich;
        private Label lblChuaLam;
        private Label lblDaLam;
        private Label lblDangChon;
    }
}
