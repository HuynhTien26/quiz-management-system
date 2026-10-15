namespace QL_Thi_Trac_Nghiem.GUI
{
    partial class FormLamBaiThi
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
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 27);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 0;
            label1.Text = "Môn:";
            // 
            // txtTenMonThi
            // 
            txtTenMonThi.Location = new Point(70, 24);
            txtTenMonThi.Name = "txtTenMonThi";
            txtTenMonThi.ReadOnly = true;
            txtTenMonThi.Size = new Size(100, 23);
            txtTenMonThi.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 63);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 2;
            label2.Text = "Mã đề thi:";
            // 
            // txtMaDeThi
            // 
            txtMaDeThi.Location = new Point(95, 60);
            txtMaDeThi.Name = "txtMaDeThi";
            txtMaDeThi.ReadOnly = true;
            txtMaDeThi.Size = new Size(75, 23);
            txtMaDeThi.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(201, 63);
            label3.Name = "label3";
            label3.Size = new Size(98, 15);
            label3.TabIndex = 4;
            label3.Text = "Thời gian còn lại:";
            // 
            // timerDemNguoc
            // 
            timerDemNguoc.Interval = 1000;
            // 
            // txtNoiDungCauHoi
            // 
            txtNoiDungCauHoi.Location = new Point(33, 122);
            txtNoiDungCauHoi.Multiline = true;
            txtNoiDungCauHoi.Name = "txtNoiDungCauHoi";
            txtNoiDungCauHoi.ReadOnly = true;
            txtNoiDungCauHoi.Size = new Size(351, 37);
            txtNoiDungCauHoi.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 102);
            label4.Name = "label4";
            label4.Size = new Size(66, 15);
            label4.TabIndex = 6;
            label4.Text = "Câu hỏi số:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rdoChonD);
            groupBox1.Controls.Add(rdoChonC);
            groupBox1.Controls.Add(rdoChonB);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(rdoChonA);
            groupBox1.Location = new Point(33, 165);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(351, 127);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chọn đáp án:";
            // 
            // rdoChonA
            // 
            rdoChonA.AutoSize = true;
            rdoChonA.Location = new Point(32, 32);
            rdoChonA.Name = "rdoChonA";
            rdoChonA.Size = new Size(94, 19);
            rdoChonA.TabIndex = 0;
            rdoChonA.TabStop = true;
            rdoChonA.Text = "radioButton1";
            rdoChonA.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 34);
            label5.Name = "label5";
            label5.Size = new Size(18, 15);
            label5.TabIndex = 1;
            label5.Text = "A:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 55);
            label6.Name = "label6";
            label6.Size = new Size(17, 15);
            label6.TabIndex = 2;
            label6.Text = "B:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 77);
            label7.Name = "label7";
            label7.Size = new Size(18, 15);
            label7.TabIndex = 3;
            label7.Text = "C:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 99);
            label8.Name = "label8";
            label8.Size = new Size(18, 15);
            label8.TabIndex = 4;
            label8.Text = "D:";
            // 
            // rdoChonB
            // 
            rdoChonB.AutoSize = true;
            rdoChonB.Location = new Point(32, 53);
            rdoChonB.Name = "rdoChonB";
            rdoChonB.Size = new Size(94, 19);
            rdoChonB.TabIndex = 5;
            rdoChonB.TabStop = true;
            rdoChonB.Text = "radioButton1";
            rdoChonB.UseVisualStyleBackColor = true;
            // 
            // rdoChonC
            // 
            rdoChonC.AutoSize = true;
            rdoChonC.Location = new Point(32, 75);
            rdoChonC.Name = "rdoChonC";
            rdoChonC.Size = new Size(94, 19);
            rdoChonC.TabIndex = 6;
            rdoChonC.TabStop = true;
            rdoChonC.Text = "radioButton1";
            rdoChonC.UseVisualStyleBackColor = true;
            // 
            // rdoChonD
            // 
            rdoChonD.AutoSize = true;
            rdoChonD.Location = new Point(32, 97);
            rdoChonD.Name = "rdoChonD";
            rdoChonD.Size = new Size(94, 19);
            rdoChonD.TabIndex = 7;
            rdoChonD.TabStop = true;
            rdoChonD.Text = "radioButton1";
            rdoChonD.UseVisualStyleBackColor = true;
            // 
            // btnCauTruoc
            // 
            btnCauTruoc.Location = new Point(33, 308);
            btnCauTruoc.Name = "btnCauTruoc";
            btnCauTruoc.Size = new Size(75, 23);
            btnCauTruoc.TabIndex = 8;
            btnCauTruoc.Text = "Câu trước";
            btnCauTruoc.UseVisualStyleBackColor = true;
            // 
            // btnCauTiep
            // 
            btnCauTiep.Location = new Point(114, 308);
            btnCauTiep.Name = "btnCauTiep";
            btnCauTiep.Size = new Size(75, 23);
            btnCauTiep.TabIndex = 9;
            btnCauTiep.Text = "Câu tiếp";
            btnCauTiep.UseVisualStyleBackColor = true;
            // 
            // btnNopBai
            // 
            btnNopBai.Location = new Point(309, 308);
            btnNopBai.Name = "btnNopBai";
            btnNopBai.Size = new Size(75, 23);
            btnNopBai.TabIndex = 10;
            btnNopBai.Text = "Nộp bài";
            btnNopBai.UseVisualStyleBackColor = true;
            // 
            // lblCauSo
            // 
            lblCauSo.AutoSize = true;
            lblCauSo.Location = new Point(95, 102);
            lblCauSo.Name = "lblCauSo";
            lblCauSo.Size = new Size(38, 15);
            lblCauSo.TabIndex = 11;
            lblCauSo.Text = "label9";
            // 
            // flpDanhSachCau
            // 
            flpDanhSachCau.Location = new Point(400, 102);
            flpDanhSachCau.Name = "flpDanhSachCau";
            flpDanhSachCau.Size = new Size(284, 190);
            flpDanhSachCau.TabIndex = 12;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(400, 84);
            label9.Name = "label9";
            label9.Size = new Size(255, 15);
            label9.TabIndex = 13;
            label9.Text = "Khung danh sách số thứ tự câu hỏi (Tùy chọn):";
            // 
            // lblThoiGianConLai
            // 
            lblThoiGianConLai.AutoSize = true;
            lblThoiGianConLai.Location = new Point(305, 63);
            lblThoiGianConLai.Name = "lblThoiGianConLai";
            lblThoiGianConLai.Size = new Size(44, 15);
            lblThoiGianConLai.TabIndex = 14;
            lblThoiGianConLai.Text = "label10";
            // 
            // FormLamBaiThi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(696, 450);
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
            Name = "FormLamBaiThi";
            Text = "FormLamBaiThi";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
    }
}