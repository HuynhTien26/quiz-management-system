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

        // Bộ lọc hàng 1 (Kỳ thi, Môn học, Lớp)
        private ComboBox cmbKyThi = null!, cmbMonHoc = null!, cmbLop = null!;
        private Button btnLoc = null!, btnXuatExcel = null!, btnXuatPDF = null!, btnLuuPDF = null!;

        // Bộ lọc hàng 2 (Mục 10a: Thời gian + Tiêu chí 3 bảng Quý)
        private ComboBox cmbCheDoThoiGian = null!, cmbGiaTriThoiGian = null!, cmbTieuChiQuy = null!;
        private Label lblTuNgay = null!, lblDenNgay = null!, lblChonKy = null!;
        private DateTimePicker dtpTuNgay = null!, dtpDenNgay = null!;
        private NumericUpDown numNam = null!;

        // 4 Tab: Chi tiết + 3 bảng theo Quý (Q1, Q2, Q3, Q4, TC)
        private TabControl tabThongKe = null!;
        private DataGridView dgvBaoCao = null!, dgvTheoMon = null!, dgvTheoLop = null!, dgvTheoGV = null!;

        // Thanh tổng hợp phía dưới (Mục 10a)
        private Label lblTongSoBai = null!, lblSoBaiDat = null!, lblSoBaiChuaDat = null!,
                      lblTyLeDat = null!, lblDiemTrungBinh = null!, lblDiemCaoNhat = null!;

        private List<BaoCaoDiemThiDTO> dsHienTai = new List<BaoCaoDiemThiDTO>();

        public FormBaoCaoThongKe()
        {
            KhoiTaoGiaoDien();
            this.Load += FormBaoCaoThongKe_Load;
        }

        private void KhoiTaoGiaoDien()
        {
            this.Text = "Báo cáo & Thống kê Điểm thi (Tổng hợp Thời gian & 3 Bảng theo Quý)";
            this.Size = new Size(1060, 640);
            this.StartPosition = FormStartPosition.CenterParent;

            // ==================== KHUNG BỘ LỌC (2 HÀNG) ====================
            GroupBox grpFilter = new GroupBox
            {
                Text = "Bộ lọc Thống kê & Xuất báo cáo",
                Dock = DockStyle.Top,
                Height = 118
            };

            // Hàng 1: Kỳ thi, Môn học, Lớp + Nút bấm
            Label lblKT = new Label { Text = "Kỳ thi:", Location = new Point(15, 30), AutoSize = true };
            cmbKyThi = new ComboBox { Location = new Point(65, 26), Width = 125, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblMon = new Label { Text = "Môn học:", Location = new Point(202, 30), AutoSize = true };
            cmbMonHoc = new ComboBox { Location = new Point(265, 26), Width = 160, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblLop = new Label { Text = "Lớp SV:", Location = new Point(438, 30), AutoSize = true };
            cmbLop = new ComboBox { Location = new Point(490, 26), Width = 115, DropDownStyle = ComboBoxStyle.DropDownList };

            btnLoc = new Button { Text = "Thống kê", Location = new Point(620, 24), Size = new Size(88, 28), BackColor = Color.DodgerBlue, ForeColor = Color.White };
            btnXuatExcel = new Button { Text = "Xuất Excel", Location = new Point(715, 24), Size = new Size(85, 28) };
            btnXuatPDF = new Button { Text = "Xuất PDF", Location = new Point(807, 24), Size = new Size(80, 28) };
            btnLuuPDF = new Button { Text = "Lưu PDF", Location = new Point(894, 24), Size = new Size(80, 28) };

            // Hàng 2: Lọc Từ ngày -> Đến ngày / Tháng / Quý / Năm & Tiêu chí 3 bảng Quý
            Label lblThoiGian = new Label { Text = "Lọc TG:", Location = new Point(15, 74), AutoSize = true };
            cmbCheDoThoiGian = new ComboBox { Location = new Point(65, 70), Width = 145, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbCheDoThoiGian.Items.AddRange(new object[] { "Tất cả", "Từ ngày - Đến ngày", "Theo Tháng", "Theo Quý", "Theo Năm" });
            cmbCheDoThoiGian.SelectedIndex = 0;

            lblTuNgay = new Label { Text = "Từ:", Location = new Point(220, 74), AutoSize = true };
            dtpTuNgay = new DateTimePicker { Location = new Point(246, 70), Width = 105, Format = DateTimePickerFormat.Short, Value = new DateTime(DateTime.Now.Year, 1, 1) };

            lblDenNgay = new Label { Text = "Đến:", Location = new Point(360, 74), AutoSize = true };
            dtpDenNgay = new DateTimePicker { Location = new Point(394, 70), Width = 105, Format = DateTimePickerFormat.Short, Value = DateTime.Now };

            lblChonKy = new Label { Text = "Tháng:", Location = new Point(220, 74), AutoSize = true, Visible = false };
            cmbGiaTriThoiGian = new ComboBox { Location = new Point(272, 70), Width = 95, DropDownStyle = ComboBoxStyle.DropDownList, Visible = false };

            Label lblNam = new Label { Text = "Năm:", Location = new Point(515, 74), AutoSize = true };
            numNam = new NumericUpDown { Location = new Point(553, 70), Width = 68, Minimum = 2020, Maximum = 2100, Value = DateTime.Now.Year };

            Label lblTieuChi = new Label { Text = "3 bảng Quý:", Location = new Point(638, 74), AutoSize = true };
            cmbTieuChiQuy = new ComboBox { Location = new Point(715, 70), Width = 165, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTieuChiQuy.Items.AddRange(new object[] { "Số lượng bài thi", "Điểm trung bình" });
            cmbTieuChiQuy.SelectedIndex = 0;

            // Gắn sự kiện
            btnLoc.Click += (s, e) => ThucHienThongKe();
            btnXuatExcel.Click += (s, e) => XuatFileExcel(dsHienTai);
            btnXuatPDF.Click += BtnXuatPDF_Click;
            btnLuuPDF.Click += (s, e) => baiThiBUS.XuatFilePdf(dsHienTai);
            cmbCheDoThoiGian.SelectedIndexChanged += (s, e) => CapNhatTrangThaiBoLocThoiGian();
            numNam.ValueChanged += (s, e) => TaiBaBangTheoQuy();
            cmbTieuChiQuy.SelectedIndexChanged += (s, e) => TaiBaBangTheoQuy();

            grpFilter.Controls.AddRange(new Control[]
            {
                lblKT, cmbKyThi, lblMon, cmbMonHoc, lblLop, cmbLop,
                btnLoc, btnXuatExcel, btnXuatPDF, btnLuuPDF,
                lblThoiGian, cmbCheDoThoiGian, lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay,
                lblChonKy, cmbGiaTriThoiGian, lblNam, numNam, lblTieuChi, cmbTieuChiQuy
            });

            // ==================== THANH TỔNG HỢP PHÍA DƯỚI ====================
            Panel pnlSummary = new Panel { Dock = DockStyle.Bottom, Height = 50, BackColor = Color.WhiteSmoke };
            lblTongSoBai = new Label { Text = "Tổng số bài: 0", Location = new Point(15, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            lblSoBaiDat = new Label { Text = "Đạt (>=5): 0", Location = new Point(170, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.DarkGreen };
            lblSoBaiChuaDat = new Label { Text = "Chưa đạt (<5): 0", Location = new Point(320, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.Firebrick };
            lblTyLeDat = new Label { Text = "Tỷ lệ đạt: 0%", Location = new Point(495, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.Purple };
            lblDiemTrungBinh = new Label { Text = "Điểm TB: 0.00", Location = new Point(660, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.DarkBlue };
            lblDiemCaoNhat = new Label { Text = "Cao nhất: 0.00", Location = new Point(825, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), ForeColor = Color.Teal };
            pnlSummary.Controls.AddRange(new Control[] { lblTongSoBai, lblSoBaiDat, lblSoBaiChuaDat, lblTyLeDat, lblDiemTrungBinh, lblDiemCaoNhat });

            // ==================== 4 TAB DỮ LIỆU THỐNG KÊ ====================
            tabThongKe = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F) };

            dgvBaoCao = TaoBangDuLieu();
            dgvTheoMon = TaoBangDuLieu();
            dgvTheoLop = TaoBangDuLieu();
            dgvTheoGV = TaoBangDuLieu();

            dgvTheoMon.DataBindingComplete += (s, e) => DinhDangBangQuy(dgvTheoMon, "Môn học");
            dgvTheoLop.DataBindingComplete += (s, e) => DinhDangBangQuy(dgvTheoLop, "Lớp sinh viên");
            dgvTheoGV.DataBindingComplete += (s, e) => DinhDangBangQuy(dgvTheoGV, "Giảng viên");

            TabPage tabChiTiet = new TabPage("1. Chi tiết Điểm thi");
            tabChiTiet.Controls.Add(dgvBaoCao);

            TabPage tabMon = new TabPage("2. Theo Môn học & Quý (Q1-Q4)");
            tabMon.Controls.Add(dgvTheoMon);

            TabPage tabLop = new TabPage("3. Theo Lớp SV & Quý (Q1-Q4)");
            tabLop.Controls.Add(dgvTheoLop);

            TabPage tabGV = new TabPage("4. Theo Giảng viên & Quý (Q1-Q4)");
            tabGV.Controls.Add(dgvTheoGV);

            tabThongKe.TabPages.AddRange(new TabPage[] { tabChiTiet, tabMon, tabLop, tabGV });

            this.Controls.Add(tabThongKe);
            this.Controls.Add(pnlSummary);
            this.Controls.Add(grpFilter);
        }

        private static DataGridView TaoBangDuLieu()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                RowHeadersVisible = false
            };
        }

        private void CapNhatTrangThaiBoLocThoiGian()
        {
            string cheDo = cmbCheDoThoiGian.SelectedItem?.ToString() ?? "Tất cả";

            bool laKhoangNgay = cheDo == "Từ ngày - Đến ngày";
            bool laThang = cheDo == "Theo Tháng";
            bool laQuy = cheDo == "Theo Quý";

            lblTuNgay.Visible = laKhoangNgay;
            dtpTuNgay.Visible = laKhoangNgay;
            lblDenNgay.Visible = laKhoangNgay;
            dtpDenNgay.Visible = laKhoangNgay;

            lblChonKy.Visible = laThang || laQuy;
            cmbGiaTriThoiGian.Visible = laThang || laQuy;

            if (laThang)
            {
                lblChonKy.Text = "Tháng:";
                cmbGiaTriThoiGian.Items.Clear();
                for (int i = 1; i <= 12; i++) cmbGiaTriThoiGian.Items.Add($"Tháng {i}");
                cmbGiaTriThoiGian.SelectedIndex = DateTime.Now.Month - 1;
            }
            else if (laQuy)
            {
                lblChonKy.Text = "Quý:";
                cmbGiaTriThoiGian.Items.Clear();
                for (int i = 1; i <= 4; i++) cmbGiaTriThoiGian.Items.Add($"Quý {i}");
                cmbGiaTriThoiGian.SelectedIndex = (DateTime.Now.Month - 1) / 3;
            }
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

            CapNhatTrangThaiBoLocThoiGian();
            ThucHienThongKe();
        }

        private void ThucHienThongKe()
        {
            try
            {
                string kyThi = cmbKyThi.SelectedItem?.ToString() ?? "Tất cả";
                string monHoc = cmbMonHoc.SelectedItem?.ToString() ?? "Tất cả";
                string maLop = cmbLop.SelectedItem?.ToString() ?? "Tất cả";
                string cheDo = cmbCheDoThoiGian.SelectedItem?.ToString() ?? "Tất cả";

                DateTime? tuNgay = null;
                DateTime? denNgay = null;
                int? thang = null;
                int? quy = null;
                int? nam = null;

                if (cheDo == "Từ ngày - Đến ngày")
                {
                    tuNgay = dtpTuNgay.Value.Date;
                    denNgay = dtpDenNgay.Value.Date;
                }
                else if (cheDo == "Theo Tháng")
                {
                    thang = cmbGiaTriThoiGian.SelectedIndex + 1;
                    nam = (int)numNam.Value;
                }
                else if (cheDo == "Theo Quý")
                {
                    quy = cmbGiaTriThoiGian.SelectedIndex + 1;
                    nam = (int)numNam.Value;
                }
                else if (cheDo == "Theo Năm")
                {
                    nam = (int)numNam.Value;
                }

                dsHienTai = baiThiBUS.ThongKeDiemThiNangCao(kyThi, monHoc, maLop, tuNgay, denNgay, thang, quy, nam);
                dgvBaoCao.DataSource = null;
                dgvBaoCao.DataSource = dsHienTai;

                TongHopThongKeDTO tongHop = baiThiBUS.TinhTongHopThongKe(dsHienTai);
                lblTongSoBai.Text = $"Tổng số bài: {tongHop.TongSoBai}";
                lblSoBaiDat.Text = $"Đạt (>=5): {tongHop.SoBaiDat}";
                lblSoBaiChuaDat.Text = $"Chưa đạt (<5): {tongHop.SoBaiChuaDat}";
                lblTyLeDat.Text = $"Tỷ lệ đạt: {tongHop.TyLeDat:0.#}%";
                lblDiemTrungBinh.Text = $"Điểm TB: {tongHop.DiemTrungBinh:0.00}";
                lblDiemCaoNhat.Text = $"Cao nhất: {tongHop.DiemCaoNhat:0.00}";

                TaiBaBangTheoQuy();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thống kê dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TaiBaBangTheoQuy()
        {
            if (dgvTheoMon == null || dgvTheoLop == null || dgvTheoGV == null) return;

            int nam = (int)numNam.Value;
            bool tinhDiemTB = cmbTieuChiQuy.SelectedIndex == 1;

            dgvTheoMon.DataSource = null;
            dgvTheoMon.DataSource = baiThiBUS.ThongKeTheoMonVaQuy(nam, tinhDiemTB);

            dgvTheoLop.DataSource = null;
            dgvTheoLop.DataSource = baiThiBUS.ThongKeTheoLopVaQuy(nam, tinhDiemTB);

            dgvTheoGV.DataSource = null;
            dgvTheoGV.DataSource = baiThiBUS.ThongKeTheoGiangVienVaQuy(nam, tinhDiemTB);
        }

        private void DinhDangBangQuy(DataGridView dgv, string tenCotDau)
        {
            if (dgv.Columns.Count == 0) return;

            dgv.Columns[0].HeaderText = tenCotDau;
            dgv.Columns[0].FillWeight = 220;

            bool tinhDiemTB = cmbTieuChiQuy.SelectedIndex == 1;
            string dinhDangSo = tinhDiemTB ? "0.00" : "N0";

            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[i].DefaultCellStyle.Format = dinhDangSo;
            }

            if (dgv.Rows.Count > 0)
            {
                var lastRow = dgv.Rows[dgv.Rows.Count - 1];
                lastRow.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                lastRow.DefaultCellStyle.BackColor = Color.AliceBlue;
                lastRow.DefaultCellStyle.ForeColor = Color.DarkBlue;
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

                ev.Graphics!.DrawString("BÁO CÁO THỐNG KÊ ĐIỂM THI TRẮC NGHIỆM", fTitle, Brushes.DarkBlue, 280, y);
                y += 35;
                ev.Graphics.DrawString($"Kỳ thi: {cmbKyThi.Text} | Môn: {cmbMonHoc.Text} | Lớp: {cmbLop.Text} | {lblTongSoBai.Text} | {lblSoBaiDat.Text} | {lblDiemTrungBinh.Text}", fHeader, Brushes.Black, 50, y);
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
