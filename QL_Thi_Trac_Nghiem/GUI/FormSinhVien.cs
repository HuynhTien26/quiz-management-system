using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormSinhVien : Form
    {
        private readonly BaiThiBUS baiThiBUS = new BaiThiBUS();
        private readonly AdminDAO adminDAO = new AdminDAO();
        private string maSVHienTai = "SV01";
        private string hoTenSVHienTai = "";

        public FormSinhVien()
        {
            InitializeComponent();
            GanSuKien();
            this.Load += FormSinhVien_Load;
        }

        // Constructor nhận Mã SV từ FormDangNhap (nếu có)
        public FormSinhVien(string maSV, string hoTen = "") : this()
        {
            if (!string.IsNullOrWhiteSpace(maSV))
                this.maSVHienTai = maSV.Trim();
            this.hoTenSVHienTai = (hoTen ?? "").Trim();
        }

        private void GanSuKien()
        {
            cmbChonKyThi.SelectedIndexChanged += LocDanhSachDeThi_Changed;
            cmbNamHocSV.SelectedIndexChanged += LocDanhSachDeThi_Changed;
            cmbMonThiSV.SelectedIndexChanged += LocDanhSachDeThi_Changed;
            btnTaiLaiDeThi.Click += (s, e) => TaiDanhSachDeThi();
            btnBatDauLamBai.Click += BtnBatDauLamBai_Click;
            btnDangXuatSV.Click += (s, e) => this.Close();

            // Sự kiện Tab 2: RESULT-FE (Xem kết quả & chi tiết bài làm)
            lsvLichSuBaiThi.SelectedIndexChanged += LsvLichSuBaiThi_SelectedIndexChanged;
            lsvChiTietBaiLam.SelectedIndexChanged += LsvChiTietBaiLam_SelectedIndexChanged;
            btnXuatPDFBangDiem.Click += BtnXuatPDFBangDiem_Click;
        }

        private void FormSinhVien_Load(object sender, EventArgs e)
        {
            NapThongTinSinhVien();
            NapBoLocComboBox();
            TaiDanhSachDeThi();
            TaiLichSuBaiThi();
        }

        private void NapThongTinSinhVien()
        {
            var dsSV = adminDAO.DocDSSV();
            var sv = dsSV.Find(x => string.Equals(x.MaSV, maSVHienTai, StringComparison.OrdinalIgnoreCase));
            if (sv != null)
            {
                txtThongTinSV.Text = sv.MaSV;
                txtHoTenSV.Text = $"{sv.HoLot} {sv.Ten}".Trim();
            }
            else
            {
                txtThongTinSV.Text = maSVHienTai;
                txtHoTenSV.Text = string.IsNullOrEmpty(hoTenSVHienTai) ? "Sinh viên dự thi" : hoTenSVHienTai;
            }
            txtNgayThi.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void NapBoLocComboBox()
        {
            cmbChonKyThi.Items.Clear();
            cmbChonKyThi.Items.Add("Tất cả");
            foreach (var hk in adminDAO.GetDanhSachHocKy()) cmbChonKyThi.Items.Add(hk);
            cmbChonKyThi.SelectedIndex = 0;

            cmbNamHocSV.Items.Clear();
            cmbNamHocSV.Items.Add("Tất cả");
            foreach (var nh in adminDAO.GetDanhSachNamHoc()) cmbNamHocSV.Items.Add(nh);
            cmbNamHocSV.SelectedIndex = 0;

            cmbMonThiSV.Items.Clear();
            cmbMonThiSV.Items.Add("Tất cả");
            foreach (var m in adminDAO.GetAllMon()) cmbMonThiSV.Items.Add(m.TenMon);
            cmbMonThiSV.SelectedIndex = 0;
        }

        private void LocDanhSachDeThi_Changed(object sender, EventArgs e)
        {
            TaiDanhSachDeThi();
        }

        private void TaiDanhSachDeThi()
        {
            lsvDanhSachBaiThi.Items.Clear();
            string kyThi = cmbChonKyThi.SelectedItem?.ToString() ?? "Tất cả";
            string namHoc = cmbNamHocSV.SelectedItem?.ToString() ?? "Tất cả";
            string monHoc = cmbMonThiSV.SelectedItem?.ToString() ?? "Tất cả";

            var dsDe = baiThiBUS.LayDanhSachDeThiChoSV(kyThi, namHoc, monHoc);
            foreach (var de in dsDe)
            {
                ListViewItem item = new ListViewItem(de.MaDe);
                item.SubItems.Add(de.TenMon);
                item.SubItems.Add(de.HocKy);
                item.SubItems.Add(de.NamHoc);
                item.SubItems.Add(de.SoCauHoi.ToString());
                item.SubItems.Add(de.ThoiGian.ToString());
                item.Tag = de;
                lsvDanhSachBaiThi.Items.Add(item);
            }
        }

        private void BtnBatDauLamBai_Click(object sender, EventArgs e)
        {
            if (lsvDanhSachBaiThi.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đề thi trong danh sách để bắt đầu làm bài!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ThongKeDeThiDTO deChon = (ThongKeDeThiDTO)lsvDanhSachBaiThi.SelectedItems[0].Tag;

            using (FormLamBaiThi frmThi = new FormLamBaiThi(txtThongTinSV.Text.Trim(), deChon.MaDe, deChon.TenMon, deChon.ThoiGian))
            {
                this.Hide();
                var res = frmThi.ShowDialog();
                this.Show();

                // Sau khi nộp bài xong, tự động chuyển sang Tab 2 (RESULT-FE) hiển thị kết quả vừa thi
                if (res == DialogResult.OK && frmThi.KetQuaSauKhiNop != null)
                {
                    TaiLichSuBaiThi();
                    tabControl1.SelectedTab = tabPage2;

                    foreach (ListViewItem item in lsvLichSuBaiThi.Items)
                    {
                        if (item.Text == frmThi.KetQuaSauKhiNop.MaBT)
                        {
                            item.Selected = true;
                            item.EnsureVisible();
                            break;
                        }
                    }
                }
            }
        }

        // ================= PHẦN RESULT-FE: XEM KẾT QUẢ & CHI TIẾT BÀI LÀM =================
        private void TaiLichSuBaiThi()
        {
            lsvLichSuBaiThi.Items.Clear();
            var dsLichSu = baiThiBUS.LayLichSuBaiThiSV(txtThongTinSV.Text.Trim());
            foreach (var bt in dsLichSu)
            {
                ListViewItem item = new ListViewItem(bt.MaBT);
                item.SubItems.Add(bt.MaDe);
                item.SubItems.Add(bt.TenMon);
                item.SubItems.Add(bt.ThoiGianBatDau.ToString("dd/MM/yyyy HH:mm:ss"));
                item.SubItems.Add(bt.ThoiGianKetThuc.ToString("dd/MM/yyyy HH:mm:ss"));
                item.SubItems.Add(bt.SoCauDung.ToString());
                item.SubItems.Add(bt.SoCauSai.ToString());
                item.SubItems.Add(bt.TongDiem.ToString("0.00"));
                item.Tag = bt;
                lsvLichSuBaiThi.Items.Add(item);
            }
        }

        private void LsvLichSuBaiThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvLichSuBaiThi.SelectedItems.Count == 0) return;

            BaiThiDTO bt = (BaiThiDTO)lsvLichSuBaiThi.SelectedItems[0].Tag;
            txtTongDiem.Text = bt.TongDiem.ToString("0.00");
            txtSoCauDung.Text = bt.SoCauDung.ToString();
            txtSoCauSai.Text = bt.SoCauSai.ToString();

            // Hiển thị chi tiết từng câu của bài thi đã chọn
            lsvChiTietBaiLam.Items.Clear();
            rtxtXemChiTietCau.Clear();

            var dsChiTiet = baiThiBUS.LayChiTietBaiLam(bt.MaBT);
            foreach (var ct in dsChiTiet)
            {
                ListViewItem item = new ListViewItem(ct.MaDe);
                item.SubItems.Add(ct.MaCH);
                item.SubItems.Add(string.IsNullOrEmpty(ct.DapAnChon) ? "(Bỏ trống)" : ct.DapAnChon);
                item.SubItems.Add(ct.KetQua);
                item.ForeColor = ct.KetQua == "Đúng" ? Color.DarkGreen : Color.Crimson;
                item.Tag = ct;
                lsvChiTietBaiLam.Items.Add(item);
            }
        }

        private void LsvChiTietBaiLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvChiTietBaiLam.SelectedItems.Count == 0) return;

            ChiTietBaiLamDTO ct = (ChiTietBaiLamDTO)lsvChiTietBaiLam.SelectedItems[0].Tag;
            rtxtXemChiTietCau.Text =
                $"Câu hỏi ({ct.MaCH}): {ct.NoiDungCauHoi}\n" +
                $"A. {ct.DapAnA}    |    B. {ct.DapAnB}    |    C. {ct.DapAnC}    |    D. {ct.DapAnD}\n" +
                $"👉 Đáp án bạn chọn: {(string.IsNullOrEmpty(ct.DapAnChon) ? "Không chọn" : ct.DapAnChon)}   —   " +
                $"Đáp án đúng: {ct.DapAnDung}   ({ct.KetQua})";
        }

        // Xuất PDF Bảng điểm cá nhân bằng Microsoft Print to PDF có sẵn trên Windows
        private void BtnXuatPDFBangDiem_Click(object sender, EventArgs e)
        {
            if (lsvLichSuBaiThi.Items.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu lịch sử bài thi để in bảng điểm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = false;
            pd.PrintPage += (s, ev) =>
            {
                Font fTitle = new Font("Segoe UI", 16, FontStyle.Bold);
                Font fHeader = new Font("Segoe UI", 10, FontStyle.Bold);
                Font fBody = new Font("Segoe UI", 10, FontStyle.Regular);
                float y = 50;

                ev.Graphics.DrawString("BẢNG ĐIỂM KẾT QUẢ THI TRẮC NGHIỆM", fTitle, Brushes.DarkBlue, 160, y);
                y += 45;
                ev.Graphics.DrawString($"Mã sinh viên: {txtThongTinSV.Text}          Họ và tên: {txtHoTenSV.Text}", fHeader, Brushes.Black, 60, y);
                y += 25;
                ev.Graphics.DrawString($"Ngày xuất phiếu: {DateTime.Now:dd/MM/yyyy HH:mm}", fBody, Brushes.Black, 60, y);
                y += 35;

                ev.Graphics.DrawString("Mã BT", fHeader, Brushes.Black, 60, y);
                ev.Graphics.DrawString("Mã Đề", fHeader, Brushes.Black, 150, y);
                ev.Graphics.DrawString("Môn Học", fHeader, Brushes.Black, 230, y);
                ev.Graphics.DrawString("Đúng/Sai", fHeader, Brushes.Black, 430, y);
                ev.Graphics.DrawString("Tổng Điểm", fHeader, Brushes.Black, 530, y);
                ev.Graphics.DrawString("Ngày Thi", fHeader, Brushes.Black, 630, y);
                y += 25;
                ev.Graphics.DrawLine(Pens.Black, 60, y, 750, y);
                y += 10;

                foreach (ListViewItem item in lsvLichSuBaiThi.Items)
                {
                    BaiThiDTO bt = (BaiThiDTO)item.Tag;
                    ev.Graphics.DrawString(bt.MaBT, fBody, Brushes.Black, 60, y);
                    ev.Graphics.DrawString(bt.MaDe, fBody, Brushes.Black, 150, y);
                    ev.Graphics.DrawString(bt.TenMon, fBody, Brushes.Black, 230, y);
                    ev.Graphics.DrawString($"{bt.SoCauDung} / {bt.SoCauSai}", fBody, Brushes.Black, 430, y);
                    ev.Graphics.DrawString(bt.TongDiem.ToString("0.00"), fHeader, Brushes.Red, 540, y);
                    ev.Graphics.DrawString(bt.ThoiGianKetThuc.ToString("dd/MM/yyyy"), fBody, Brushes.Black, 630, y);
                    y += 28;
                }
            };

            PrintDialog dlg = new PrintDialog { Document = pd };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            TaiLichSuBaiThi();
        }
    }
}
