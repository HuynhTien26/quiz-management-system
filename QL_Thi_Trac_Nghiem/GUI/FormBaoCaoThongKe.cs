using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public class FormBaoCaoThongKe : Form
    {
        private readonly BaiThiBUS baiThiBUS = new BaiThiBUS();
        private readonly AdminDAO adminDAO = new AdminDAO();

        private ComboBox cmbKyThi, cmbMonHoc, cmbLop;
        private Button btnLoc, btnXuatExcel, btnXuatPDF, btnLuuPDF;
        private DataGridView dgvBaoCao;
        private Label lblTongSoBai, lblDiemTrungBinh, lblDiemCaoNhat;
        private List<BaoCaoDiemThiDTO> dsHienTai = new List<BaoCaoDiemThiDTO>();

        public FormBaoCaoThongKe()
        {
            KhoiTaoGiaoDien();
            this.Load += FormBaoCaoThongKe_Load;
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Báo cáo & Thống kê Điểm thi theo Kỳ thi, Môn học, Lớp";
            this.Size = new Size(1000, 560);
            this.StartPosition = FormStartPosition.CenterParent;

            GroupBox grpFilter = new GroupBox
            {
                Text = "Bộ lọc Thống kê",
                Dock = DockStyle.Top,
                Height = 80
            };

            Label lblKT = new Label { Text = "Kỳ thi:", Location = new Point(20, 32), AutoSize = true };
            cmbKyThi = new ComboBox { Location = new Point(70, 28), Width = 130, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblMon = new Label { Text = "Môn học:", Location = new Point(220, 32), AutoSize = true };
            cmbMonHoc = new ComboBox { Location = new Point(285, 28), Width = 170, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblLop = new Label { Text = "Lớp SV:", Location = new Point(475, 32), AutoSize = true };
            cmbLop = new ComboBox { Location = new Point(530, 28), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };

            btnLoc = new Button { Text = "Thống kê", Location = new Point(665, 26), Size = new Size(80, 28), BackColor = Color.DodgerBlue, ForeColor = Color.White };
            btnXuatExcel = new Button { Text = "Xuất Excel", Location = new Point(750, 26), Size = new Size(75, 28) };
            btnXuatPDF = new Button { Text = "Xuất PDF", Location = new Point(830, 26), Size = new Size(70, 28) };
            btnLuuPDF = new Button { Text = "Lưu PDF", Location = new Point(905, 26), Size = new Size(70, 28) };
            
            btnLoc.Click += (s, e) => ThucHienThongKe();
            btnXuatExcel.Click += (s, e) => XuatFileExcel(dsHienTai);
            btnXuatPDF.Click += BtnXuatPDF_Click;
            btnLuuPDF.Click += (s, e) => baiThiBUS.XuatFilePdf(dsHienTai);

            grpFilter.Controls.AddRange(new Control[] { lblKT, cmbKyThi, lblMon, cmbMonHoc, lblLop, cmbLop, btnLoc, btnXuatExcel, btnXuatPDF, btnLuuPDF });

            Panel pnlSummary = new Panel { Dock = DockStyle.Bottom, Height = 45, Padding = new Padding(15, 10, 15, 10) };
            lblTongSoBai = new Label { Text = "Tổng số bài thi: 0", Location = new Point(20, 12), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            lblDiemTrungBinh = new Label { Text = "Điểm trung bình: 0.00", Location = new Point(260, 12), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.DarkBlue };
            lblDiemCaoNhat = new Label { Text = "Điểm cao nhất: 0.00", Location = new Point(520, 12), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.DarkGreen };
            pnlSummary.Controls.AddRange(new Control[] { lblTongSoBai, lblDiemTrungBinh, lblDiemCaoNhat });

            dgvBaoCao = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            this.Controls.Add(dgvBaoCao);
            this.Controls.Add(pnlSummary);
            this.Controls.Add(grpFilter);
        }

        private void FormBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            cmbKyThi.Items.Clear();
            cmbKyThi.Items.Add("Tất cả");
            foreach (var hk in adminDAO.GetDanhSachHocKy()) cmbKyThi.Items.Add(hk);
            cmbKyThi.SelectedIndex = 0;

            cmbMonHoc.Items.Clear();
            cmbMonHoc.Items.Add("Tất cả");
            foreach (var m in adminDAO.GetAllMon()) cmbMonHoc.Items.Add(m.TenMon);
            cmbMonHoc.SelectedIndex = 0;

            cmbLop.Items.Clear();
            cmbLop.Items.Add("Tất cả");
            foreach (var l in adminDAO.GetAllLop()) cmbLop.Items.Add(l.MaLop);
            cmbLop.SelectedIndex = 0;

            ThucHienThongKe();
        }

        private void ThucHienThongKe()
        {
            string kyThi = cmbKyThi.SelectedItem?.ToString() ?? "Tất cả";
            string monHoc = cmbMonHoc.SelectedItem?.ToString() ?? "Tất cả";
            string maLop = cmbLop.SelectedItem?.ToString() ?? "Tất cả";

            dsHienTai = baiThiBUS.ThongKeDiemThi(kyThi, monHoc, maLop);
            dgvBaoCao.DataSource = null;
            dgvBaoCao.DataSource = dsHienTai;

            lblTongSoBai.Text = $"Tổng số bài thi: {dsHienTai.Count}";
            if (dsHienTai.Count > 0)
            {
                double tong = 0, max = 0;
                foreach (var item in dsHienTai)
                {
                    tong += item.TongDiem;
                    if (item.TongDiem > max) max = item.TongDiem;
                }
                lblDiemTrungBinh.Text = $"Điểm trung bình: {(tong / dsHienTai.Count):0.00}";
                lblDiemCaoNhat.Text = $"Điểm cao nhất: {max:0.00}";
            }
            else
            {
                lblDiemTrungBinh.Text = "Điểm trung bình: 0.00";
                lblDiemCaoNhat.Text = "Điểm cao nhất: 0.00";
            }
        }

        private void XuatFileExcel(List<BaoCaoDiemThiDTO>? dsBaoCao)
        {
            if (dsBaoCao == null || dsBaoCao.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu điểm thi để xuất Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel CSV (*.csv)|*.csv";
                sfd.FileName = $"BaoCaoDiemThi_{DateTime.Now:yyyyMMdd_HHmm}.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Mã Bài Thi,Mã SV,Họ và Tên,Lớp,Mã Đề,Môn Học,Kỳ Thi,Năm Học,Số Câu Đúng,Số Câu Sai,Tổng Điểm,Ngày Thi");
                    foreach (var item in dsBaoCao)
                    {
                        sb.AppendLine($"\"{item.MaBT}\",\"{item.MaSV}\",\"{item.HoTenSV}\",\"{item.Lop}\",\"{item.MaDe}\",\"{item.TenMon}\",\"{item.KyThi}\",\"{item.NamHoc}\",{item.SoCauDung},{item.SoCauSai},{item.TongDiem},\"{item.NgayThi:dd/MM/yyyy HH:mm}\"");
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnXuatPDF_Click(object sender, EventArgs e)
        {
            if (dsHienTai.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu điểm thi để xuất PDF!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += (s, ev) =>
            {
                Font fTitle = new Font("Segoe UI", 15, FontStyle.Bold);
                Font fHeader = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                Font fRow = new Font("Segoe UI", 9.5F, FontStyle.Regular);
                float y = 40;

                ev.Graphics.DrawString("BÁO CÁO THỐNG KÊ ĐIỂM THI TRẮC NGHIỆM", fTitle, Brushes.DarkBlue, 280, y);
                y += 35;
                ev.Graphics.DrawString($"Kỳ thi: {cmbKyThi.Text}   |   Môn học: {cmbMonHoc.Text}   |   Lớp: {cmbLop.Text}   |   {lblTongSoBai.Text}   |   {lblDiemTrungBinh.Text}", fHeader, Brushes.Black, 50, y);
                y += 30;

                ev.Graphics.DrawString("Mã SV", fHeader, Brushes.Black, 50, y);
                ev.Graphics.DrawString("Họ và Tên", fHeader, Brushes.Black, 130, y);
                ev.Graphics.DrawString("Lớp", fHeader, Brushes.Black, 310, y);
                ev.Graphics.DrawString("Mã Đề", fHeader, Brushes.Black, 390, y);
                ev.Graphics.DrawString("Môn Học", fHeader, Brushes.Black, 470, y);
                ev.Graphics.DrawString("Đúng/Sai", fHeader, Brushes.Black, 660, y);
                ev.Graphics.DrawString("Tổng Điểm", fHeader, Brushes.Black, 760, y);
                ev.Graphics.DrawString("Ngày Thi", fHeader, Brushes.Black, 860, y);
                y += 22;
                ev.Graphics.DrawLine(Pens.Black, 50, y, 1000, y);
                y += 8;

                foreach (var item in dsHienTai)
                {
                    ev.Graphics.DrawString(item.MaSV, fRow, Brushes.Black, 50, y);
                    ev.Graphics.DrawString(item.HoTenSV, fRow, Brushes.Black, 130, y);
                    ev.Graphics.DrawString(item.Lop, fRow, Brushes.Black, 310, y);
                    ev.Graphics.DrawString(item.MaDe, fRow, Brushes.Black, 390, y);
                    ev.Graphics.DrawString(item.TenMon, fRow, Brushes.Black, 470, y);
                    ev.Graphics.DrawString($"{item.SoCauDung}/{item.SoCauSai}", fRow, Brushes.Black, 670, y);
                    ev.Graphics.DrawString(item.TongDiem.ToString("0.00"), fHeader, Brushes.Red, 775, y);
                    ev.Graphics.DrawString(item.NgayThi.ToString("dd/MM/yyyy"), fRow, Brushes.Black, 860, y);
                    y += 24;
                    if (y > 720) break;
                }
            };

            PrintDialog dlg = new PrintDialog { Document = pd };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
    }
}
