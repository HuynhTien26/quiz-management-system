using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DTO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormGiaoVien : Form
    {
        // Khai báo dạng biến thường (không dùng { get; set; }) để không bị lỗi WFO1000
        public string MaNguoiDungDangNhap = "";
        public string VaiTroDangNhap = "";

        private readonly MonBUS monbus = new MonBUS();
        private string maMonDangChon = "";
        private string maKhoaDangChon = "";
        private bool dangTaiDanhSachMon = false;

        public FormGiaoVien(string maND = "", string vaiTro = "Admin")
        {
            InitializeComponent();
            MaNguoiDungDangNhap = maND;
            VaiTroDangNhap = vaiTro;

            this.Load -= FormGiaoVien_Load;
            this.Load += FormGiaoVien_Load;

            btnCapNhatMon.Click -= btnCapNhatMon_Click;
            btnCapNhatMon.Click += btnCapNhatMon_Click;

            btnXoaMon.Click -= btnXoaMon_Click;
            btnXoaMon.Click += btnXoaMon_Click;

            lsvMonHoc.SelectedIndexChanged -= lsvMonHoc_SelectedIndexChanged;
            lsvMonHoc.SelectedIndexChanged += lsvMonHoc_SelectedIndexChanged;
        }

        private void FormGiaoVien_Load(object sender, EventArgs e)
        {
            PhanQuyenGiaoDien();
            KhoiTaoTabMonHoc();
        }

        private void PhanQuyenGiaoDien()
        {
            bool laAdmin = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            // 1. Tự động điền Mã GV vào ô Mã Giáo Viên ở Tab Đề thi và khóa lại
            txtMaGVDangTai.Text = laAdmin ? "ADMIN" : MaNguoiDungDangNhap;
            txtMaGVDangTai.ReadOnly = true;

            // 2. Nút "Đăng tải" đề thi: Chỉ Admin mới có quyền bấm duyệt đăng tải cho SV
            btnDangTaiDe.Enabled = laAdmin;

            // 3. Nếu là Giáo viên: Ẩn Tab Quản lý Khoa - Lớp và khóa quyền sửa Môn học / Kỳ thi
            if (!laAdmin)
            {
                if (tabControl1.TabPages.ContainsKey("tabPageKhoaLop"))
                {
                    tabControl1.TabPages.RemoveByKey("tabPageKhoaLop");
                }

                btnThemMon.Enabled = false;
                btnCapNhatMon.Enabled = false;
                btnXoaMon.Enabled = false;

                btnThemKT.Enabled = false;
                btnCapNhatKT.Enabled = false;
                btnXoaKT.Enabled = false;
            }
        }

        // ================= TAB MÔN HỌC =================
        private void KhoiTaoTabMonHoc()
        {
            if (lsvMonHoc.Columns.Count == 0)
            {
                lsvMonHoc.Columns.Add("Mã môn", 90);
                lsvMonHoc.Columns.Add("Tên môn", 220);
                lsvMonHoc.Columns.Add("Tín chỉ", 70);
                lsvMonHoc.Columns.Add("Khoa", 120);
            }

            LoadDanhSachMonLenLuoi();
        }

        private void LoadDanhSachMonLenLuoi()
        {
            dangTaiDanhSachMon = true;
            try
            {
                List<MonDTO> dsMon = monbus.DocDSMon();
                lsvMonHoc.Items.Clear();

                foreach (MonDTO mon in dsMon)
                {
                    ListViewItem item = new ListViewItem(mon.MaMon);
                    item.SubItems.Add(mon.TenMon);
                    item.SubItems.Add(mon.TinChi.ToString());
                    item.SubItems.Add(mon.MaKhoa);
                    item.Tag = mon;
                    lsvMonHoc.Items.Add(item);
                }
            }
            finally
            {
                dangTaiDanhSachMon = false;
            }

            LamMoiFormMon();
        }

        private void LamMoiFormMon()
        {
            maMonDangChon = "";
            maKhoaDangChon = "";

            txtMaMon.ReadOnly = false;
            txtMaMon.Clear();
            txtTenMon.Clear();

            numTinChi.Value = 3;
            lsvMonHoc.SelectedItems.Clear();

            txtMaMon.Focus();
        }

        private int LaySoTinChi() => (int)numTinChi.Value;

        private void lsvMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangTaiDanhSachMon || lsvMonHoc.SelectedItems.Count == 0) return;

            if (lsvMonHoc.SelectedItems[0].Tag is MonDTO mon)
            {
                maMonDangChon = mon.MaMon;
                maKhoaDangChon = mon.MaKhoa;

                txtMaMon.Text = mon.MaMon;
                txtMaMon.ReadOnly = true; // Không cho sửa khóa chính
                txtTenMon.Text = mon.TenMon;
                numTinChi.Value = Math.Clamp(mon.TinChi, 1, 15);
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            MonDTO mon = new MonDTO
            {
                MaMon = txtMaMon.Text,
                TenMon = txtTenMon.Text,
                TinChi = LaySoTinChi(),
                MaKhoa = ""
            };

            if (monbus.ThemMon(mon, out string thongBao))
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachMonLenLuoi();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhatMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maMonDangChon))
            {
                MessageBox.Show("Vui lòng chọn môn học trong danh sách trước khi cập nhật!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MonDTO mon = new MonDTO
            {
                MaMon = maMonDangChon,
                TenMon = txtTenMon.Text,
                TinChi = LaySoTinChi(),
                MaKhoa = maKhoaDangChon
            };

            if (monbus.CapNhatMon(mon, out string thongBao))
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachMonLenLuoi();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maMonDangChon))
            {
                MessageBox.Show("Vui lòng chọn môn học trong danh sách trước khi xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult traLoi = MessageBox.Show($"Bạn có chắc muốn xóa môn '{maMonDangChon}' không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traLoi != DialogResult.Yes) return;

            if (monbus.XoaMon(maMonDangChon, out string thongBao))
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachMonLenLuoi();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaCH_Click(object sender, EventArgs e)
        {
        }
    }
}
