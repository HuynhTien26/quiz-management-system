using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormTraCuu : Form
    {
        // Chỉ giữ lại 2 biến DTO này để truyền dữ liệu qua lại
        public static GiangVienDTO GVDuocChon = null;
        public static SinhVienDTO SVDuocChon = null;

        private AdminBUS adminBUS = new AdminBUS();
        private TextBox txtSearch;
        private List<GiangVienDTO> dsGVGoc = new List<GiangVienDTO>();
        private List<SinhVienDTO> dsSVGoc = new List<SinhVienDTO>();
        
        public FormTraCuu()
        {
            InitializeComponent();
            KhoiTaoGiaoDienTimKiem();
            this.Load += FormTraCuu_Load;
        }

        private void KhoiTaoGiaoDienTimKiem()
        {
            this.Text = "Tra cứu & Chọn Nhân sự (Nhấp đúp chuột để chọn)";
            this.Size = new Size(650, 420);
            this.StartPosition = FormStartPosition.CenterParent;

            // 1. Tạo thanh Search phía trên
            Panel pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                Padding = new Padding(10)
            };

            Label lblSearch = new Label
            {
                Text = "Search:",
                AutoSize = true,
                Location = new Point(12, 14),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };

            txtSearch = new TextBox
            {
                Location = new Point(75, 11),
                Width = 530,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                PlaceholderText = "Nhập mã, họ tên, khoa hoặc lớp để lọc nhanh..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;
            txtSearch.KeyDown += TxtSearch_KeyDown;

            pnlTop.Controls.Add(lblSearch);
            pnlTop.Controls.Add(txtSearch);

            // 2. Cấu hình lại lsvDuLieu
            lsvDuLieu.Dock = DockStyle.Fill;
            lsvDuLieu.View = View.Details;
            lsvDuLieu.FullRowSelect = true;
            lsvDuLieu.GridLines = true;
            lsvDuLieu.Columns.Clear();
            lsvDuLieu.Columns.Add("Mã", 85);
            lsvDuLieu.Columns.Add("Họ và tên", 180);
            lsvDuLieu.Columns.Add("Vai trò", 90);
            lsvDuLieu.Columns.Add("Khoa", 110);
            lsvDuLieu.Columns.Add("Lớp / Môn", 140);

            // Đăng ký sự kiện DoubleClick an toàn
            lsvDuLieu.DoubleClick -= LsvDuLieu_DoubleClick;
            lsvDuLieu.DoubleClick += LsvDuLieu_DoubleClick;
            lsvDuLieu.KeyDown += LsvDuLieu_KeyDown;

            this.Controls.Add(pnlTop);
            lsvDuLieu.BringToFront();
        }

        private void FormTraCuu_Load(object sender, EventArgs e)
        {
            dsGVGoc = adminBUS.LocGiaoVien("", "", "", "", "") ?? new List<GiangVienDTO>();
            dsSVGoc = adminBUS.LocSinhVien("", "", "", "", "", "") ?? new List<SinhVienDTO>();

            HienThiDanhSachLoc("");
            txtSearch.Focus();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            HienThiDanhSachLoc(txtSearch.Text.Trim());
        }

        private void HienThiDanhSachLoc(string tuKhoa)
        {
            lsvDuLieu.Items.Clear();
            string kw = tuKhoa.ToLower();

            foreach (var gv in dsGVGoc)
            {
                string hoTen = $"{gv.HoLot} {gv.Ten}".Trim();
                if (string.IsNullOrEmpty(kw) ||
                    gv.MaGV.ToLower().Contains(kw) ||
                    hoTen.ToLower().Contains(kw) ||
                    (gv.MaKhoa != null && gv.MaKhoa.ToLower().Contains(kw)) ||
                    (gv.MaMon != null && gv.MaMon.ToLower().Contains(kw)))
                {
                    ListViewItem item = new ListViewItem(gv.MaGV);
                    item.SubItems.Add(hoTen);
                    item.SubItems.Add("Giáo viên");
                    item.SubItems.Add(gv.MaKhoa);
                    item.SubItems.Add(gv.MaMon);
                    item.Tag = gv;
                    lsvDuLieu.Items.Add(item);
                }
            }

            foreach (var sv in dsSVGoc)
            {
                string hoTen = $"{sv.HoLot} {sv.Ten}".Trim();
                if (string.IsNullOrEmpty(kw) ||
                    sv.MaSV.ToLower().Contains(kw) ||
                    hoTen.ToLower().Contains(kw) ||
                    (sv.MaKhoa != null && sv.MaKhoa.ToLower().Contains(kw)) ||
                    (sv.Lop != null && sv.Lop.ToLower().Contains(kw)))
                {
                    ListViewItem item = new ListViewItem(sv.MaSV);
                    item.SubItems.Add(hoTen);
                    item.SubItems.Add("Sinh viên");
                    item.SubItems.Add(sv.MaKhoa);
                    item.SubItems.Add(sv.Lop);
                    item.Tag = sv;
                    lsvDuLieu.Items.Add(item);
                }
            }
        }

        private void LsvDuLieu_DoubleClick(object sender, EventArgs e)
        {
            ChonDongHienTaiVaDong();
        }

        // Alias tương thích ngược nếu FormTraCuu.Designer.cs gọi chữ thường
        private void lsvDuLieu_DoubleClick(object sender, EventArgs e)
        {
            LsvDuLieu_DoubleClick(sender, e);
        }

        private void LsvDuLieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChonDongHienTaiVaDong();
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lsvDuLieu.Items.Count > 0)
            {
                lsvDuLieu.Items[0].Selected = true;
                ChonDongHienTaiVaDong();
            }
        }

        private void ChonDongHienTaiVaDong()
        {
            if (lsvDuLieu.SelectedItems.Count == 0) return;

            object duLieu = lsvDuLieu.SelectedItems[0].Tag;
            if (duLieu is GiangVienDTO gv)
            {
                GVDuocChon = gv;
                SVDuocChon = null;
                MaNhanSuDuocChon = gv.MaGV;
            }
            else if (duLieu is SinhVienDTO sv)
            {
                SVDuocChon = sv;
                GVDuocChon = null;
                MaNhanSuDuocChon = sv.MaSV;
            }

            this.Close();
        }
    }
}