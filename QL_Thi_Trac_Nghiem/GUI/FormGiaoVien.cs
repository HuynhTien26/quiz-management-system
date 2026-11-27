using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormGiaoVien : Form
    {
        public string MaNguoiDungDangNhap = "";
        public string VaiTroDangNhap = "";

        private readonly MonBUS monbus = new MonBUS();
        private string maMonDangChon = "";
        private string maKhoaDangChon = "";
        private bool dangTaiDanhSachMon = false;

        private readonly CauHoiBUS cauhoibus = new CauHoiBUS();
        private string maCHDangChon = "";
        private bool dangTaiDanhSachCauHoi = false;
        private bool dangXemCaNhanCH = false;

        private readonly ExamConfigBUS examConfigBUS = new ExamConfigBUS();
        private readonly AdminBUS adminBUS = new AdminBUS();

        private string maMTDangChon = "";
        private bool dangTaiMaTran = false;

        private string maKTDangChon = "";
        private bool dangSuaKyThi = false;
        private bool dangTaiKyThi = false;

        private string maDeDangChon = "";
        private bool dangTaiDeThi = false;
        private ListView? lsvChiTietDeThi;

        private string maKhoaQuanLyDangChon = "";
        private string maLopDangChon = "";
        private bool dangTaiKhoaLop = false;

        private const string MucDoCoBan = "Cơ bản";
        private const string MucDoVanDung = "Vận dụng";
        private const string MucDoNangCao = "Nâng cao";

        public FormGiaoVien(string maND = "", string vaiTro = "Admin")
        {
            InitializeComponent();

            // Áp dụng giao diện đồng bộ (kích thước, font, màu nút, bố cục các tab)
            ApDungGiaoDienDongBo();

            MaNguoiDungDangNhap = maND;
            VaiTroDangNhap = vaiTro;

            DongBoKetNoiDataProvider();

            this.Load -= FormGiaoVien_Load;
            this.Load += FormGiaoVien_Load;

            // Tab 1: Môn học
            btnCapNhatMon.Click -= btnCapNhatMon_Click;
            btnCapNhatMon.Click += btnCapNhatMon_Click;

            btnXoaMon.Click -= btnXoaMon_Click;
            btnXoaMon.Click += btnXoaMon_Click;

            btnLamMoiMon.Click -= btnLamMoiMon_Click;
            btnLamMoiMon.Click += btnLamMoiMon_Click;

            btnTimMon.Click -= btnTimMon_Click;
            btnTimMon.Click += btnTimMon_Click;

            lsvMonHoc.SelectedIndexChanged -= lsvMonHoc_SelectedIndexChanged;
            lsvMonHoc.SelectedIndexChanged += lsvMonHoc_SelectedIndexChanged;

            // Tab 2: Ma trận đề thi
            txtTyLeCoBan.TextChanged -= CapNhatTongTyLeMaTran;
            txtTyLeCoBan.TextChanged += CapNhatTongTyLeMaTran;
            txtTyLeVanDung.TextChanged -= CapNhatTongTyLeMaTran;
            txtTyLeVanDung.TextChanged += CapNhatTongTyLeMaTran;
            txtTyLeNangCao.TextChanged -= CapNhatTongTyLeMaTran;
            txtTyLeNangCao.TextChanged += CapNhatTongTyLeMaTran;

            btnThemMT.Click -= btnThemMT_Click;
            btnThemMT.Click += btnThemMT_Click;
            btnCapNhatMT.Click -= btnCapNhatMT_Click;
            btnCapNhatMT.Click += btnCapNhatMT_Click;
            btnXoaMT.Click -= btnXoaMT_Click;
            btnXoaMT.Click += btnXoaMT_Click;
            lsvMaTran.SelectedIndexChanged -= lsvMaTran_SelectedIndexChanged;
            lsvMaTran.SelectedIndexChanged += lsvMaTran_SelectedIndexChanged;

            // Tab 3: Ngân hàng câu hỏi
            btnThemCH.Click -= btnThemCH_Click;
            btnThemCH.Click += btnThemCH_Click;

            btnCapNhatCH.Click -= btnCapNhatCH_Click;
            btnCapNhatCH.Click += btnCapNhatCH_Click;

            btnXoaCH.Click -= btnXoaCH_Click;
            btnXoaCH.Click += btnXoaCH_Click;

            btnLamMoiCH.Click -= btnLamMoiCH_Click;
            btnLamMoiCH.Click += btnLamMoiCH_Click;

            btnXemTatCaCH.Click -= btnXemTatCaCH_Click;
            btnXemTatCaCH.Click += btnXemTatCaCH_Click;

            btnXemCaNhanCH.Click -= btnXemCaNhanCH_Click;
            btnXemCaNhanCH.Click += btnXemCaNhanCH_Click;

            lsvCauHoi.SelectedIndexChanged -= lsvCauHoi_SelectedIndexChanged;
            lsvCauHoi.SelectedIndexChanged += lsvCauHoi_SelectedIndexChanged;

            btnNhapExcelCH.Click -= btnNhapExcelCH_Click;
            btnNhapExcelCH.Click += btnNhapExcelCH_Click;

            btnTimCH.Click -= btnTimCH_Click;
            btnTimCH.Click += btnTimCH_Click;

            // Tab 4: Quản lý Kỳ thi
            btnThemKT.Click -= btnThemKT_Click;
            btnThemKT.Click += btnThemKT_Click;
            btnCapNhatKT.Click -= btnCapNhatKT_Click;
            btnCapNhatKT.Click += btnCapNhatKT_Click;
            btnXoaKT.Click -= btnXoaKT_Click;
            btnXoaKT.Click += btnXoaKT_Click;
            lsvKyThi.SelectedIndexChanged -= lsvKyThi_SelectedIndexChanged;
            lsvKyThi.SelectedIndexChanged += lsvKyThi_SelectedIndexChanged;

            // Tab 5: Quản lý Đề thi
            btnThemDe.Click -= btnThemDe_Click;
            btnThemDe.Click += btnThemDe_Click;
            btnCapNhatDe.Click -= btnCapNhatDe_Click;
            btnCapNhatDe.Click += btnCapNhatDe_Click;
            btnXoaDe.Click -= btnXoaDe_Click;
            btnXoaDe.Click += btnXoaDe_Click;
            btnDangTaiDe.Click -= btnDangTaiDe_Click;
            btnDangTaiDe.Click += btnDangTaiDe_Click;
            lsvDeThi.SelectedIndexChanged -= lsvDeThi_SelectedIndexChanged;
            lsvDeThi.SelectedIndexChanged += lsvDeThi_SelectedIndexChanged;

            // Tab Khoa - Lớp
            btnThemKhoa.Click -= btnThemKhoa_Click;
            btnThemKhoa.Click += btnThemKhoa_Click;
            btnCapNhatKhoa.Click -= btnCapNhatKhoa_Click;
            btnCapNhatKhoa.Click += btnCapNhatKhoa_Click;
            btnXoaKhoa.Click -= btnXoaKhoa_Click;
            btnXoaKhoa.Click += btnXoaKhoa_Click;
            lsvKhoa.SelectedIndexChanged -= lsvKhoa_SelectedIndexChanged;
            lsvKhoa.SelectedIndexChanged += lsvKhoa_SelectedIndexChanged;

            btnThemLop.Click -= btnThemLop_Click;
            btnThemLop.Click += btnThemLop_Click;
            btnCapNhatLop.Click -= btnCapNhatLop_Click;
            btnCapNhatLop.Click += btnCapNhatLop_Click;
            btnXoaLop.Click -= btnXoaLop_Click;
            btnXoaLop.Click += btnXoaLop_Click;
            lsvLop.SelectedIndexChanged -= lsvLop_SelectedIndexChanged;
            lsvLop.SelectedIndexChanged += lsvLop_SelectedIndexChanged;

            tabControl1.SelectedIndexChanged -= tabControl1_SelectedIndexChanged;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }

        private static void DongBoKetNoiDataProvider()
        {
            if (!string.IsNullOrWhiteSpace(DataProvider.ConnectionString)) return;
            try
            {
                using SqlConnection conn = MyConnection.GetConnection();
                if (!string.IsNullOrWhiteSpace(conn.ConnectionString))
                    DataProvider.ConnectionString = conn.ConnectionString;
            }
            catch
            {
                // Bỏ qua nếu chưa có kết nối
            }
        }

        private void FormGiaoVien_Load(object sender, EventArgs e)
        {
            DongBoKetNoiDataProvider();
            PhanQuyenGiaoDien();
            KhoiTaoTabMonHoc();
            KhoiTaoTabCauHoi();
            KhoiTaoTabMaTran();
            KhoiTaoTabKyThi();
            KhoiTaoTabDeThi();
            if (tabControl1.TabPages.ContainsKey("tabPageKhoaLop"))
                KhoiTaoTabKhoaLop();
        }

        private void PhanQuyenGiaoDien()
        {
            bool laAdmin = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            txtMaGVDangTai.Text = laAdmin ? "ADMIN" : MaNguoiDungDangNhap;
            txtMaGVDangTai.ReadOnly = true;

            // Tab 3: Chỉ Admin được xem tất cả câu hỏi; GV chỉ xem câu hỏi của chính mình
            btnXemTatCaCH.Visible = laAdmin;
            btnXemTatCaCH.Enabled = laAdmin;

            // Tab 5: Chỉ Admin được duyệt đăng tải đề thi cho SV
            btnDangTaiDe.Enabled = laAdmin;

            if (!laAdmin)
            {
                if (tabControl1.TabPages.ContainsKey("tabPageKhoaLop"))
                {
                    tabControl1.TabPages.RemoveByKey("tabPageKhoaLop");
                }

                btnThemKT.Enabled = false;
                btnCapNhatKT.Enabled = false;
                btnXoaKT.Enabled = false;
            }

            btnThemMon.Enabled = laAdmin;
            btnCapNhatMon.Enabled = laAdmin;
            btnXoaMon.Enabled = laAdmin;
        }

        // ================= TAB 1: MÔN HỌC =================
        private void KhoiTaoTabMonHoc()
        {
            LoadDanhSachMonLenLuoi();
        }

        private void HienThiDanhSachMon(List<MonDTO> dsMon)
        {
            dangTaiDanhSachMon = true;
            try
            {
                lsvMonHoc.Items.Clear();

                foreach (MonDTO mon in dsMon)
                {
                    ListViewItem item = new ListViewItem(mon.MaMon);
                    item.SubItems.Add(mon.TenMon);
                    item.SubItems.Add(mon.TinChi.ToString());
                    item.Tag = mon;
                    lsvMonHoc.Items.Add(item);
                }
            }
            finally
            {
                dangTaiDanhSachMon = false;
            }
        }

        private void LoadDanhSachMonLenLuoi()
        {
            HienThiDanhSachMon(monbus.DocDSMon());
            LamMoiFormMon();
        }

        private void XoaTrangPhanNhapMon()
        {
            maMonDangChon = "";
            maKhoaDangChon = "";

            txtMaMon.ReadOnly = false;
            txtMaMon.Clear();
            txtTenMon.Clear();

            numTinChi.Value = 3;
            lsvMonHoc.SelectedItems.Clear();
        }

        private void LamMoiFormMon()
        {
            XoaTrangPhanNhapMon();
            txtMaMon.Focus();
        }

        private int LaySoTinChi() => (int)numTinChi.Value;

        private bool KiemTraHopLeInputMonHoc()
        {
            if (string.IsNullOrWhiteSpace(txtMaMon.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaMon.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return false;
            }

            return true;
        }

        private void lsvMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangTaiDanhSachMon || lsvMonHoc.SelectedItems.Count == 0) return;

            if (lsvMonHoc.SelectedItems[0].Tag is MonDTO mon)
            {
                maMonDangChon = mon.MaMon;
                maKhoaDangChon = mon.MaKhoa;

                txtMaMon.Text = mon.MaMon;
                txtMaMon.ReadOnly = true;
                txtTenMon.Text = mon.TenMon;
                numTinChi.Value = Math.Clamp(mon.TinChi, 1, 15);
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (!KiemTraHopLeInputMonHoc()) return;

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
                NapDanhSachMonVaoCacCombo();
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

            if (!KiemTraHopLeInputMonHoc()) return;

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
                NapDanhSachMonVaoCacCombo();
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
                NapDanhSachMonVaoCacCombo();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoiMon_Click(object sender, EventArgs e)
        {
            txtTimKiemMon.Clear();
            LoadDanhSachMonLenLuoi();
        }

        private void btnTimMon_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemMon.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                LoadDanhSachMonLenLuoi();
                return;
            }

            List<MonDTO> ketQua = monbus.TimKiemMon(tuKhoa);
            HienThiDanhSachMon(ketQua);
            XoaTrangPhanNhapMon();

            if (ketQua.Count == 0)
            {
                MessageBox.Show("Không tìm thấy môn học nào phù hợp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ================= TAB 2: MA TRẬN ĐỀ THI =================
        private void KhoiTaoTabMaTran()
        {
            if (lsvMaTran.Columns.Count == 0)
            {
                lsvMaTran.Columns.Add("Mã ma trận", 100);
                lsvMaTran.Columns.Add("Mã môn", 85);
                lsvMaTran.Columns.Add("Môn học", 160);
                lsvMaTran.Columns.Add("% Cơ bản", 80);
                lsvMaTran.Columns.Add("% Vận dụng", 90);
                lsvMaTran.Columns.Add("% VĐ Nâng cao", 105);
            }

            NapDanhSachMonVaoCombo(cmbMonMT, themMucGoiY: true);
            LamMoiFormMaTran();
            LoadDanhSachMaTran();
        }

        private void CapNhatTongTyLeMaTran(object? sender, EventArgs e)
        {
            double tong = DocSoThuc(txtTyLeCoBan.Text) + DocSoThuc(txtTyLeVanDung.Text) + DocSoThuc(txtTyLeNangCao.Text);
            txtTongTyLe.Text = tong.ToString("0.##", CultureInfo.InvariantCulture) + "%";
        }

        private static double DocSoThuc(string? raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return 0;
            string chuan = raw.Trim().Replace(',', '.');
            return double.TryParse(chuan, NumberStyles.Any, CultureInfo.InvariantCulture, out double val) ? val : 0;
        }

        private void LamMoiFormMaTran()
        {
            maMTDangChon = "";
            txtMaMT.ReadOnly = false;
            txtMaMT.Clear();
            txtTyLeCoBan.Text = "50";
            txtTyLeVanDung.Text = "30";
            txtTyLeNangCao.Text = "20";
            if (cmbMonMT.Items.Count > 0) cmbMonMT.SelectedIndex = 0;
            CapNhatTongTyLeMaTran(null, EventArgs.Empty);
            lsvMaTran.SelectedItems.Clear();
        }

        private void LoadDanhSachMaTran()
        {
            dangTaiMaTran = true;
            try
            {
                lsvMaTran.Items.Clear();
                DataTable dt = examConfigBUS.LayDanhSachMaTran();
                foreach (DataRow row in dt.Rows)
                {
                    string maMT = row["Ma_MT"]?.ToString()?.Trim() ?? "";
                    string maMon = row["Ma_Mon"]?.ToString()?.Trim() ?? "";
                    string tenMon = dt.Columns.Contains("Ten_Mon") && row["Ten_Mon"] != DBNull.Value
                        ? row["Ten_Mon"]?.ToString()?.Trim() ?? maMon
                        : maMon;
                    string cb = row["Ty_le_co_ban"]?.ToString() ?? "0";
                    string vd = row["Ty_le_van_dung"]?.ToString() ?? "0";
                    string vdnc = row["Ty_le_vd_nang_cao"]?.ToString() ?? "0";

                    ListViewItem item = new ListViewItem(maMT);
                    item.SubItems.Add(maMon);
                    item.SubItems.Add(tenMon);
                    item.SubItems.Add(cb);
                    item.SubItems.Add(vd);
                    item.SubItems.Add(vdnc);
                    item.Tag = row;
                    lsvMaTran.Items.Add(item);
                }
            }
            finally
            {
                dangTaiMaTran = false;
            }
        }

        private void lsvMaTran_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (dangTaiMaTran || lsvMaTran.SelectedItems.Count == 0) return;

            if (lsvMaTran.SelectedItems[0].Tag is DataRow row)
            {
                maMTDangChon = row["Ma_MT"]?.ToString()?.Trim() ?? "";
                txtMaMT.Text = maMTDangChon;
                txtMaMT.ReadOnly = true;

                string maMon = row["Ma_Mon"]?.ToString()?.Trim() ?? "";
                if (!string.IsNullOrEmpty(maMon))
                    cmbMonMT.SelectedValue = maMon;

                txtTyLeCoBan.Text = row["Ty_le_co_ban"]?.ToString() ?? "0";
                txtTyLeVanDung.Text = row["Ty_le_van_dung"]?.ToString() ?? "0";
                txtTyLeNangCao.Text = row["Ty_le_vd_nang_cao"]?.ToString() ?? "0";
                CapNhatTongTyLeMaTran(null, EventArgs.Empty);
            }
        }

        private void LuuMaTranTuForm()
        {
            string maMT = txtMaMT.Text.Trim();
            string maMon = cmbMonMT.SelectedValue?.ToString() ?? "";
            double cb = DocSoThuc(txtTyLeCoBan.Text);
            double vd = DocSoThuc(txtTyLeVanDung.Text);
            double vdnc = DocSoThuc(txtTyLeNangCao.Text);

            var res = examConfigBUS.LuuMaTran(maMT, maMon, cb, vd, vdnc);
            if (!res.ok)
            {
                MessageBox.Show(res.msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(res.msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachMaTran();
            NapDanhSachMaTranVaoCombo();
            LamMoiFormMaTran();
        }

        private void btnThemMT_Click(object? sender, EventArgs e) => LuuMaTranTuForm();
        private void btnCapNhatMT_Click(object? sender, EventArgs e) => LuuMaTranTuForm();

        private void btnXoaMT_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maMTDangChon))
            {
                MessageBox.Show("Vui lòng chọn ma trận cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa ma trận '{maMTDangChon}' không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (examConfigBUS.XoaMaTran(maMTDangChon))
            {
                MessageBox.Show("Đã xóa ma trận đề thi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachMaTran();
                NapDanhSachMaTranVaoCombo();
                LamMoiFormMaTran();
            }
            else
            {
                MessageBox.Show("Không thể xóa ma trận (có thể đang được đề thi sử dụng).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= TAB 3: NGÂN HÀNG CÂU HỎI =================
        private void KhoiTaoTabCauHoi()
        {
            KhoiTaoCotCauHoi();
            NapDanhSachMonVaoCombo(cmbMonCH, themMucGoiY: true);
            NapDapAnDungVaoCombo();

            bool laAdmin = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            if (laAdmin)
                LoadTatCaCauHoi();
            else
                LoadCauHoiCaNhan();
        }

        private void KhoiTaoCotCauHoi()
        {
            if (lsvCauHoi.Columns.Count > 0) return;

            lsvCauHoi.Columns.Add("Mã CH", 75);
            lsvCauHoi.Columns.Add("Môn", 120);
            lsvCauHoi.Columns.Add("Nội dung", 260);
            lsvCauHoi.Columns.Add("Đáp án A", 180);
            lsvCauHoi.Columns.Add("Đáp án B", 180);
            lsvCauHoi.Columns.Add("Đáp án C", 180);
            lsvCauHoi.Columns.Add("Đáp án D", 180);
            lsvCauHoi.Columns.Add("Đáp án đúng", 65);
            lsvCauHoi.Columns.Add("Mức độ", 90);
        }

        private void NapDanhSachMonVaoCombo(ComboBox cmb, bool themMucGoiY)
        {
            try
            {
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.DataSource = null;
                cmb.DisplayMember = "TenMon";
                cmb.ValueMember = "MaMon";

                List<MonDTO> dsMon = monbus.DocDSMon() ?? new List<MonDTO>();
                if (themMucGoiY)
                    dsMon.Insert(0, new MonDTO { MaMon = "", TenMon = "Chọn môn" });

                cmb.DataSource = dsMon;
                if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
            }
            catch (Exception)
            {
                // Bỏ qua nếu mất kết nối
            }
        }

        private void NapDanhSachMonVaoCacCombo()
        {
            NapDanhSachMonVaoCombo(cmbMonCH, themMucGoiY: true);
            NapDanhSachMonVaoCombo(cmbMonMT, themMucGoiY: true);
            NapDanhSachMonVaoCombo(cmbMonDeThi, themMucGoiY: true);
        }

        private void NapDapAnDungVaoCombo()
        {
            cmbDapAnDung.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDapAnDung.Items.Clear();
            cmbDapAnDung.Items.Add("Chọn đáp án đúng");
            cmbDapAnDung.Items.AddRange(new object[] { "A", "B", "C", "D" });
            cmbDapAnDung.SelectedIndex = 0;
        }

        private void LoadDanhSachCauHoiLenLuoi(List<CauHoiDTO> dsCauHoi)
        {
            dangTaiDanhSachCauHoi = true;
            try
            {
                lsvCauHoi.Items.Clear();

                foreach (CauHoiDTO ch in dsCauHoi)
                {
                    ListViewItem item = new ListViewItem(ch.MaCH);
                    item.SubItems.Add(ch.TenMon);
                    item.SubItems.Add(ch.NoiDung);
                    item.SubItems.Add(ch.DapAnA);
                    item.SubItems.Add(ch.DapAnB);
                    item.SubItems.Add(ch.DapAnC);
                    item.SubItems.Add(ch.DapAnD);
                    item.SubItems.Add(ch.DapAnDung);
                    item.SubItems.Add(ch.MucDo);
                    item.Tag = ch;
                    lsvCauHoi.Items.Add(item);
                }
            }
            finally
            {
                dangTaiDanhSachCauHoi = false;
            }
        }

        private void LoadTatCaCauHoi()
        {
            dangXemCaNhanCH = false;
            LoadDanhSachCauHoiLenLuoi(cauhoibus.DocDSCauHoi());
            LamMoiFormCauHoi();
        }

        private void LoadCauHoiCaNhan()
        {
            dangXemCaNhanCH = true;
            LoadDanhSachCauHoiLenLuoi(cauhoibus.LocTheoGiaoVien(LayMaGVDangNhap()));
            LamMoiFormCauHoi();
        }

        private void LamMoiFormCauHoi()
        {
            maCHDangChon = "";

            txtMaCH.ReadOnly = false;
            txtMaCH.Clear();
            txtNoiDungCH.Clear();
            txtDapAnA.Clear();
            txtDapAnB.Clear();
            txtDapAnC.Clear();
            txtDapAnD.Clear();

            if (cmbMonCH.Items.Count > 0) cmbMonCH.SelectedIndex = 0;
            if (cmbDapAnDung.Items.Count > 0) cmbDapAnDung.SelectedIndex = 0;

            rdoCoBan.Checked = true;
            rdoVanDung.Checked = false;
            rdoNangCao.Checked = false;

            lsvCauHoi.SelectedItems.Clear();
            txtMaCH.Focus();
        }

        private string LayMaGVDangNhap()
        {
            return VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                ? CauHoiBUS.MaGVAdmin
                : MaNguoiDungDangNhap;
        }

        private string LayMucDoDangChon()
        {
            if (rdoCoBan.Checked) return MucDoCoBan;
            if (rdoVanDung.Checked) return MucDoVanDung;
            if (rdoNangCao.Checked) return MucDoNangCao;
            return "";
        }

        private void ChonMucDo(string mucDo)
        {
            rdoCoBan.Checked = mucDo == MucDoCoBan;
            rdoVanDung.Checked = mucDo == MucDoVanDung;
            rdoNangCao.Checked = mucDo == MucDoNangCao;
        }

        private CauHoiDTO TaoCauHoiTuForm(string maCH)
        {
            return new CauHoiDTO
            {
                MaCH = (maCH ?? "").Trim(),
                MaMon = cmbMonCH.SelectedValue?.ToString() ?? "",
                MaGV = LayMaGVDangNhap(),
                NoiDung = txtNoiDungCH.Text,
                DapAnA = txtDapAnA.Text,
                DapAnB = txtDapAnB.Text,
                DapAnC = txtDapAnC.Text,
                DapAnD = txtDapAnD.Text,
                DapAnDung = cmbDapAnDung.SelectedItem?.ToString() ?? "",
                MucDo = LayMucDoDangChon()
            };
        }

        private void lsvCauHoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dangTaiDanhSachCauHoi || lsvCauHoi.SelectedItems.Count == 0) return;

            if (lsvCauHoi.SelectedItems[0].Tag is CauHoiDTO ch)
            {
                maCHDangChon = ch.MaCH;

                txtMaCH.Text = ch.MaCH;
                txtMaCH.ReadOnly = true;

                if (!string.IsNullOrEmpty(ch.MaMon)) cmbMonCH.SelectedValue = ch.MaMon;

                txtNoiDungCH.Text = ch.NoiDung;
                txtDapAnA.Text = ch.DapAnA;
                txtDapAnB.Text = ch.DapAnB;
                txtDapAnC.Text = ch.DapAnC;
                txtDapAnD.Text = ch.DapAnD;

                if (!string.IsNullOrEmpty(ch.DapAnDung)) cmbDapAnDung.SelectedItem = ch.DapAnDung;
                ChonMucDo(ch.MucDo);
            }
        }

        private void TaiLaiDanhSachCauHoiTheoCheDo()
        {
            if (dangXemCaNhanCH)
                LoadDanhSachCauHoiLenLuoi(cauhoibus.LocTheoGiaoVien(LayMaGVDangNhap()));
            else
                LoadDanhSachCauHoiLenLuoi(cauhoibus.DocDSCauHoi());

            LamMoiFormCauHoi();
        }

        private void btnThemCH_Click(object sender, EventArgs e)
        {
            CauHoiDTO ch = TaoCauHoiTuForm(txtMaCH.Text);

            if (cauhoibus.ThemCauHoi(ch, out string thongBao))
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiLaiDanhSachCauHoiTheoCheDo();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhatCH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maCHDangChon))
            {
                MessageBox.Show("Vui lòng chọn câu hỏi trong danh sách trước khi cập nhật!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CauHoiDTO ch = TaoCauHoiTuForm(maCHDangChon);

            if (cauhoibus.CapNhatCauHoi(ch, out string thongBao))
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiLaiDanhSachCauHoiTheoCheDo();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaCH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maCHDangChon))
            {
                MessageBox.Show("Vui lòng chọn câu hỏi trong danh sách trước khi xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult traLoi = MessageBox.Show($"Bạn có chắc muốn xóa câu hỏi '{maCHDangChon}' không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (traLoi != DialogResult.Yes) return;

            if (cauhoibus.XoaCauHoi(maCHDangChon, out string thongBao))
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiLaiDanhSachCauHoiTheoCheDo();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemTatCaCH_Click(object sender, EventArgs e)
        {
            if (!VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Giáo viên chỉ được xem danh sách câu hỏi do mình biên soạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadTatCaCauHoi();
        }

        private void btnXemCaNhanCH_Click(object sender, EventArgs e)
        {
            LoadCauHoiCaNhan();
        }

        private void btnTimCH_Click(object? sender, EventArgs e)
        {
            if (CauHoiBUS.arrCauHoi.Count == 0)
            {
                if (dangXemCaNhanCH) cauhoibus.LocTheoGiaoVien(LayMaGVDangNhap());
                else cauhoibus.DocDSCauHoi();
            }

            string tuKhoa = txtTimKiemCH.Text.Trim();
            List<CauHoiDTO> kq = cauhoibus.TimKiemCauHoi(tuKhoa);
            LoadDanhSachCauHoiLenLuoi(kq);

            if (tuKhoa.Length > 0 && kq.Count == 0)
                MessageBox.Show("Không tìm thấy câu hỏi nào phù hợp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoiCH_Click(object? sender, EventArgs e)
        {
            txtTimKiemCH.Clear();
            if (dangXemCaNhanCH) LoadDanhSachCauHoiLenLuoi(cauhoibus.LocTheoGiaoVien(LayMaGVDangNhap()));
            else LoadDanhSachCauHoiLenLuoi(cauhoibus.DocDSCauHoi());
            LamMoiFormCauHoi();
        }

        private void btnNhapExcelCH_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Chọn file Excel danh sách câu hỏi",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            string maMonMacDinh = cmbMonCH.SelectedValue?.ToString() ?? "";

            if (!cauhoibus.NhapTuExcel(ofd.FileName, LayMaGVDangNhap(), maMonMacDinh,
                                       out List<KetQuaImportDTO> ketQua, out string thongBao))
            {
                MessageBox.Show(thongBao, "Lỗi nhập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(TaoBaoCaoImport(ketQua, thongBao), "Kết quả nhập",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            TaiLaiDanhSachCauHoiTheoCheDo();
        }

        private static string TaoBaoCaoImport(List<KetQuaImportDTO> ketQua, string tomTat)
        {
            List<KetQuaImportDTO> dsLoi = ketQua.Where(k => !k.ThanhCong).ToList();
            if (dsLoi.Count == 0) return tomTat;

            StringBuilder sb = new StringBuilder(tomTat);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("Các dòng bị bỏ qua:");

            foreach (KetQuaImportDTO loi in dsLoi.Take(15))
            {
                sb.AppendLine($"- Dòng {loi.Dong}" +
                              (string.IsNullOrWhiteSpace(loi.MaCH) ? "" : $" ({loi.MaCH})") +
                              $": {loi.ThongBao}");
            }

            if (dsLoi.Count > 15)
            {
                sb.AppendLine($"... và {dsLoi.Count - 15} dòng lỗi khác");
            }

            return sb.ToString().TrimEnd();
        }

        // ================= TAB 4: QUẢN LÝ KỲ THI =================
        private void KhoiTaoTabKyThi()
        {
            if (lsvKyThi.Columns.Count == 0)
            {
                lsvKyThi.Columns.Add("Mã kỳ thi", 95);
                lsvKyThi.Columns.Add("Tên kỳ thi", 130);
                lsvKyThi.Columns.Add("Năm học", 110);
                lsvKyThi.Columns.Add("Ngày thi", 110);
            }

            if (cmbTenKT.Items.Count == 0)
                cmbTenKT.Items.AddRange(new object[] { "Học kỳ 1", "Học kỳ 2", "Học kỳ 3" });

            LamMoiFormKyThi();
            LoadDanhSachKyThi();
        }

        private void LamMoiFormKyThi()
        {
            maKTDangChon = "";
            dangSuaKyThi = false;
            txtMaKT.ReadOnly = false;
            txtMaKT.Clear();
            if (cmbTenKT.Items.Count > 0) cmbTenKT.SelectedIndex = 0;
            txtNamHocKT.Text = $"{DateTime.Now.Year}-{DateTime.Now.Year + 1}";
            dtpNgayThi.Value = DateTime.Now.Date;
            lsvKyThi.SelectedItems.Clear();
        }

        private void LoadDanhSachKyThi()
        {
            dangTaiKyThi = true;
            try
            {
                lsvKyThi.Items.Clear();
                DataTable dt = examConfigBUS.LayDanhSachKyThi();
                foreach (DataRow row in dt.Rows)
                {
                    string maKT = row["Ma_KT"]?.ToString()?.Trim() ?? "";
                    string tenKT = row["Ten_KT"]?.ToString()?.Trim() ?? "";
                    string namHoc = dt.Columns.Contains("Nam_Hoc") && row["Nam_Hoc"] != DBNull.Value
                        ? row["Nam_Hoc"]?.ToString()?.Trim() ?? ""
                        : "";
                    string ngayThi = row["Ngay_Thi"] != DBNull.Value
                        ? Convert.ToDateTime(row["Ngay_Thi"]).ToString("dd/MM/yyyy")
                        : "";

                    ListViewItem item = new ListViewItem(maKT);
                    item.SubItems.Add(tenKT);
                    item.SubItems.Add(namHoc);
                    item.SubItems.Add(ngayThi);
                    item.Tag = row;
                    lsvKyThi.Items.Add(item);
                }
            }
            finally
            {
                dangTaiKyThi = false;
            }
        }

        private void lsvKyThi_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (dangTaiKyThi || lsvKyThi.SelectedItems.Count == 0) return;

            if (lsvKyThi.SelectedItems[0].Tag is DataRow row)
            {
                maKTDangChon = row["Ma_KT"]?.ToString()?.Trim() ?? "";
                dangSuaKyThi = true;
                txtMaKT.Text = maKTDangChon;
                txtMaKT.ReadOnly = true;

                string tenKT = row["Ten_KT"]?.ToString()?.Trim() ?? "";
                if (!string.IsNullOrEmpty(tenKT))
                    cmbTenKT.Text = tenKT;

                if (row.Table.Columns.Contains("Nam_Hoc") && row["Nam_Hoc"] != DBNull.Value)
                    txtNamHocKT.Text = row["Nam_Hoc"]?.ToString()?.Trim() ?? "";

                if (row["Ngay_Thi"] != DBNull.Value)
                    dtpNgayThi.Value = Convert.ToDateTime(row["Ngay_Thi"]);
            }
        }

        private void btnThemKT_Click(object? sender, EventArgs e)
        {
            string maKT = txtMaKT.Text.Trim().ToUpperInvariant();
            string tenKT = cmbTenKT.Text.Trim();
            string namHoc = txtNamHocKT.Text.Trim();

            var res = examConfigBUS.LuuKyThi(maKT, tenKT, dtpNgayThi.Value.Date, isEdit: false, namHoc: namHoc);
            if (!res.ok)
            {
                MessageBox.Show(res.msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(res.msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachKyThi();
            NapDanhSachKyThiVaoCombo();
            LamMoiFormKyThi();
        }

        private void btnCapNhatKT_Click(object? sender, EventArgs e)
        {
            if (!dangSuaKyThi || string.IsNullOrWhiteSpace(maKTDangChon))
            {
                MessageBox.Show("Vui lòng chọn kỳ thi cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenKT = cmbTenKT.Text.Trim();
            string namHoc = txtNamHocKT.Text.Trim();

            var res = examConfigBUS.LuuKyThi(maKTDangChon, tenKT, dtpNgayThi.Value.Date, isEdit: true, namHoc: namHoc);
            if (!res.ok)
            {
                MessageBox.Show(res.msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(res.msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachKyThi();
            NapDanhSachKyThiVaoCombo();
            LamMoiFormKyThi();
        }

        private void btnXoaKT_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maKTDangChon))
            {
                MessageBox.Show("Vui lòng chọn kỳ thi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa kỳ thi '{maKTDangChon}' không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var res = examConfigBUS.XoaKyThiAnToan(maKTDangChon);
            if (!res.ok)
            {
                MessageBox.Show(res.msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(res.msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachKyThi();
            NapDanhSachKyThiVaoCombo();
            LamMoiFormKyThi();
        }

        // ================= TAB 5: QUẢN LÝ ĐỀ THI =================
        private void KhoiTaoTabDeThi()
        {
            if (lsvDeThi.Columns.Count == 0)
            {
                lsvDeThi.Columns.Add("Mã đề", 95);
                lsvDeThi.Columns.Add("Môn", 80);
                lsvDeThi.Columns.Add("Ma trận", 80);
                lsvDeThi.Columns.Add("Kỳ thi", 80);
                lsvDeThi.Columns.Add("Số câu", 65);
                lsvDeThi.Columns.Add("Thời gian", 75);
                lsvDeThi.Columns.Add("Trạng thái", 105);
            }

            NapDanhSachMonVaoCombo(cmbMonDeThi, themMucGoiY: true);
            NapDanhSachMaTranVaoCombo();
            NapDanhSachKyThiVaoCombo();
            LamMoiFormDeThi();
            LoadDanhSachDeThi();
        }

        private void NapDanhSachMaTranVaoCombo()
        {
            DataTable dtMT = examConfigBUS.LayDanhSachMaTran();
            cmbMaTranDeThi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaTranDeThi.DisplayMember = "Ma_MT";
            cmbMaTranDeThi.ValueMember = "Ma_MT";
            cmbMaTranDeThi.DataSource = dtMT;
        }

        private void NapDanhSachKyThiVaoCombo()
        {
            DataTable dtKT = examConfigBUS.LayDanhSachKyThi();
            cmbKyThiDeThi.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKyThiDeThi.DisplayMember = "Ma_KT";
            cmbKyThiDeThi.ValueMember = "Ma_KT";
            cmbKyThiDeThi.DataSource = dtKT;
        }

        private void LamMoiFormDeThi()
        {
            maDeDangChon = "";
            txtMaDe.ReadOnly = false;
            txtMaDe.Clear();
            txtSoLuongCau.Text = "10";
            txtThoiGianLamBai.Text = Math.Max(15, new BaiThiBUS().LayThoiGianMacDinh()).ToString();
            txtMaGVDangTai.Text = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                ? "ADMIN"
                : MaNguoiDungDangNhap;
            if (cmbMonDeThi.Items.Count > 0) cmbMonDeThi.SelectedIndex = 0;
            lsvDeThi.SelectedItems.Clear();
        }

        private void LoadDanhSachDeThi()
        {
            dangTaiDeThi = true;
            try
            {
                lsvDeThi.Items.Clear();
                bool laAdmin = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase);
                DataTable dt = laAdmin
                    ? examConfigBUS.LayDanhSachDeThi()
                    : examConfigBUS.LayDanhSachDeThiTheoGV(MaNguoiDungDangNhap);
                foreach (DataRow row in dt.Rows)
                {
                    string maDe = row["Ma_De"]?.ToString()?.Trim() ?? "";
                    string maMon = row["Ma_Mon"]?.ToString()?.Trim() ?? "";
                    string maMT = dt.Columns.Contains("Ma_MT") && row["Ma_MT"] != DBNull.Value ? row["Ma_MT"]?.ToString()?.Trim() ?? "" : "";
                    string maKT = dt.Columns.Contains("Ma_KT") && row["Ma_KT"] != DBNull.Value ? row["Ma_KT"]?.ToString()?.Trim() ?? "" : "";
                    string soCau = dt.Columns.Contains("So_Cau_Hoi") && row["So_Cau_Hoi"] != DBNull.Value ? row["So_Cau_Hoi"]?.ToString() ?? "0" : "0";
                    string thoiGian = dt.Columns.Contains("Thoi_Gian") && row["Thoi_Gian"] != DBNull.Value ? row["Thoi_Gian"]?.ToString() ?? "0" : "0";
                    string trangThai = dt.Columns.Contains("Trang_Thai") && row["Trang_Thai"] != DBNull.Value ? row["Trang_Thai"]?.ToString() ?? "" : "";

                    ListViewItem item = new ListViewItem(maDe);
                    item.SubItems.Add(maMon);
                    item.SubItems.Add(maMT);
                    item.SubItems.Add(maKT);
                    item.SubItems.Add(soCau);
                    item.SubItems.Add(thoiGian);
                    item.SubItems.Add(trangThai);
                    item.Tag = row;
                    lsvDeThi.Items.Add(item);
                }
            }
            finally
            {
                dangTaiDeThi = false;
            }
        }

        private void lsvDeThi_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (dangTaiDeThi || lsvDeThi.SelectedItems.Count == 0) return;

            if (lsvDeThi.SelectedItems[0].Tag is DataRow row)
            {
                maDeDangChon = row["Ma_De"]?.ToString()?.Trim() ?? "";
                txtMaDe.Text = maDeDangChon;
                txtMaDe.ReadOnly = true;

                string maMon = row["Ma_Mon"]?.ToString()?.Trim() ?? "";
                if (!string.IsNullOrEmpty(maMon)) cmbMonDeThi.SelectedValue = maMon;

                string maMT = row.Table.Columns.Contains("Ma_MT") ? row["Ma_MT"]?.ToString()?.Trim() ?? "" : "";
                if (!string.IsNullOrEmpty(maMT)) cmbMaTranDeThi.SelectedValue = maMT;

                string maKT = row.Table.Columns.Contains("Ma_KT") ? row["Ma_KT"]?.ToString()?.Trim() ?? "" : "";
                if (!string.IsNullOrEmpty(maKT)) cmbKyThiDeThi.SelectedValue = maKT;

                if (row.Table.Columns.Contains("So_Cau_Hoi"))
                    txtSoLuongCau.Text = row["So_Cau_Hoi"]?.ToString() ?? "10";
                if (row.Table.Columns.Contains("Thoi_Gian"))
                    txtThoiGianLamBai.Text = row["Thoi_Gian"]?.ToString() ?? "45";
                if (row.Table.Columns.Contains("Ma_GV") && row["Ma_GV"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["Ma_GV"]?.ToString()))
                    txtMaGVDangTai.Text = row["Ma_GV"]?.ToString()?.Trim() ?? "";
            }
        }

        private (double cb, double vd, double vdnc) LayTyLeMaTranDangChon(string maMT)
        {
            DataTable dt = examConfigBUS.LayDanhSachMaTran();
            DataRow? r = dt.AsEnumerable().FirstOrDefault(x =>
                string.Equals(x["Ma_MT"]?.ToString()?.Trim(), maMT, StringComparison.OrdinalIgnoreCase));

            if (r == null) return (50, 30, 20);

            return (
                Convert.ToDouble(r["Ty_le_co_ban"] == DBNull.Value ? 50 : r["Ty_le_co_ban"]),
                Convert.ToDouble(r["Ty_le_van_dung"] == DBNull.Value ? 30 : r["Ty_le_van_dung"]),
                Convert.ToDouble(r["Ty_le_vd_nang_cao"] == DBNull.Value ? 20 : r["Ty_le_vd_nang_cao"])
            );
        }

        private void btnThemDe_Click(object? sender, EventArgs e)
        {
            string maDe = txtMaDe.Text.Trim().ToUpperInvariant();
            string maMon = cmbMonDeThi.SelectedValue?.ToString() ?? "";
            string maMT = cmbMaTranDeThi.SelectedValue?.ToString() ?? "";
            string maKT = cmbKyThiDeThi.SelectedValue?.ToString() ?? "";
            string maGV = txtMaGVDangTai.Text.Trim();

            if (string.IsNullOrWhiteSpace(maMon))
            {
                MessageBox.Show("Vui lòng chọn Môn học cho đề thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuongCau.Text.Trim(), out int soLuongCH) ||
                !int.TryParse(txtThoiGianLamBai.Text.Trim(), out int thoiGian))
            {
                MessageBox.Show("Số lượng câu hỏi và thời gian làm bài phải là số nguyên hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (cb, vd, vdnc) = LayTyLeMaTranDangChon(maMT);
            var res = examConfigBUS.SinhVaLuuDeThi(maDe, maKT, maMT, maMon, maGV, soLuongCH, thoiGian, cb, vd, vdnc);
            if (!res.ok)
            {
                MessageBox.Show(res.msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string thongBaoThemDe = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                ? res.msg
                : "Đã lưu đề thi ở trạng thái 'Chờ duyệt'. Vui lòng chờ Admin duyệt và đăng tải cho sinh viên.";

            MessageBox.Show(thongBaoThemDe, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachDeThi();
            LamMoiFormDeThi();
        }

        private void btnCapNhatDe_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maDeDangChon))
            {
                MessageBox.Show("Vui lòng chọn đề thi cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuongCau.Text.Trim(), out int soLuongCH) ||
                !int.TryParse(txtThoiGianLamBai.Text.Trim(), out int thoiGian))
            {
                MessageBox.Show("Số lượng câu hỏi và thời gian làm bài phải là số nguyên hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMon = cmbMonDeThi.SelectedValue?.ToString() ?? "";
            string maMT = cmbMaTranDeThi.SelectedValue?.ToString() ?? "";
            string maKT = cmbKyThiDeThi.SelectedValue?.ToString() ?? "";

            var res = examConfigBUS.CapNhatDeThi(maDeDangChon, maKT, maMT, maMon, soLuongCH, thoiGian);
            if (!res.ok)
            {
                MessageBox.Show(res.msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(res.msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachDeThi();
            LamMoiFormDeThi();
        }

        private void btnXoaDe_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maDeDangChon))
            {
                MessageBox.Show("Vui lòng chọn đề thi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa đề thi '{maDeDangChon}' không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            DataProvider.Instance.ExecuteNonQuery("DELETE FROM CT_DT WHERE Ma_De = @MaDe", new[] { new SqlParameter("@MaDe", maDeDangChon) });
            DataProvider.Instance.ExecuteNonQuery("DELETE FROM De_Thi WHERE Ma_De = @MaDe", new[] { new SqlParameter("@MaDe", maDeDangChon) });

            MessageBox.Show("Đã xóa đề thi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachDeThi();
            LamMoiFormDeThi();
        }

        private void btnDangTaiDe_Click(object? sender, EventArgs e)
        {
            if (!VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ Admin mới có quyền đăng tải đề thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(maDeDangChon))
            {
                MessageBox.Show("Vui lòng chọn đề thi cần đăng tải!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataProvider.Instance.ExecuteNonQuery(
                "UPDATE De_Thi SET Trang_Thai = N'Đã đăng tải' WHERE Ma_De = @MaDe",
                new[] { new SqlParameter("@MaDe", maDeDangChon) });

            MessageBox.Show($"Đã đăng tải đề thi '{maDeDangChon}' cho sinh viên.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDanhSachDeThi();
        }

        // ================= TAB KHOA - LỚP (CHỈ ADMIN) =================
        private void KhoiTaoTabKhoaLop()
        {
            LoadDanhSachKhoa();
            LoadDanhSachLop();
        }

        private void LoadDanhSachKhoa()
        {
            dangTaiKhoaLop = true;
            try
            {
                lsvKhoa.Items.Clear();
                List<KhoaDTO> dsKhoa = adminBUS.DocDSKhoa() ?? new List<KhoaDTO>();
                foreach (var k in dsKhoa)
                {
                    ListViewItem item = new ListViewItem(k.MaKhoa);
                    item.SubItems.Add(k.TenKhoa);
                    item.Tag = k;
                    lsvKhoa.Items.Add(item);
                }

                cmbKhoaCuaLop.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbKhoaCuaLop.DataSource = null;
                cmbKhoaCuaLop.DisplayMember = "TenKhoa";
                cmbKhoaCuaLop.ValueMember = "MaKhoa";
                cmbKhoaCuaLop.DataSource = dsKhoa;
            }
            finally
            {
                dangTaiKhoaLop = false;
            }
        }

        private void LoadDanhSachLop()
        {
            dangTaiKhoaLop = true;
            try
            {
                lsvLop.Items.Clear();
                List<LopDTO> dsLop = adminBUS.DocDSLop() ?? new List<LopDTO>();
                foreach (var l in dsLop)
                {
                    ListViewItem item = new ListViewItem(l.MaLop);
                    item.SubItems.Add(l.MaKhoa);
                    item.Tag = l;
                    lsvLop.Items.Add(item);
                }
            }
            finally
            {
                dangTaiKhoaLop = false;
            }
        }

        private void lsvKhoa_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (dangTaiKhoaLop || lsvKhoa.SelectedItems.Count == 0) return;
            if (lsvKhoa.SelectedItems[0].Tag is KhoaDTO k)
            {
                maKhoaQuanLyDangChon = k.MaKhoa;
                txtMaKhoa.Text = k.MaKhoa;
                txtMaKhoa.ReadOnly = true;
                txtTenKhoa.Text = k.TenKhoa;
            }
        }

        private void btnThemKhoa_Click(object? sender, EventArgs e)
        {
            string ma = txtMaKhoa.Text.Trim().ToUpperInvariant();
            string ten = txtTenKhoa.Text.Trim();
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã khoa và Tên khoa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataProvider.Instance.ExecuteNonQuery(
                "INSERT INTO Khoa (Ma_Khoa, Ten_Khoa) VALUES (@Ma, @Ten)",
                new[] { new SqlParameter("@Ma", ma), new SqlParameter("@Ten", ten) });

            maKhoaQuanLyDangChon = "";
            txtMaKhoa.ReadOnly = false;
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            LoadDanhSachKhoa();
        }

        private void btnCapNhatKhoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maKhoaQuanLyDangChon) || string.IsNullOrWhiteSpace(txtTenKhoa.Text))
            {
                MessageBox.Show("Vui lòng chọn khoa và nhập Tên khoa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataProvider.Instance.ExecuteNonQuery(
                "UPDATE Khoa SET Ten_Khoa = @Ten WHERE Ma_Khoa = @Ma",
                new[] { new SqlParameter("@Ma", maKhoaQuanLyDangChon), new SqlParameter("@Ten", txtTenKhoa.Text.Trim()) });

            maKhoaQuanLyDangChon = "";
            txtMaKhoa.ReadOnly = false;
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            LoadDanhSachKhoa();
        }

        private void btnXoaKhoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maKhoaQuanLyDangChon))
            {
                MessageBox.Show("Vui lòng chọn khoa cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa khoa '{maKhoaQuanLyDangChon}' không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            DataProvider.Instance.ExecuteNonQuery(
                "DELETE FROM Khoa WHERE Ma_Khoa = @Ma",
                new[] { new SqlParameter("@Ma", maKhoaQuanLyDangChon) });

            maKhoaQuanLyDangChon = "";
            txtMaKhoa.ReadOnly = false;
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            LoadDanhSachKhoa();
        }

        private void lsvLop_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (dangTaiKhoaLop || lsvLop.SelectedItems.Count == 0) return;
            if (lsvLop.SelectedItems[0].Tag is LopDTO l)
            {
                maLopDangChon = l.MaLop;
                txtMaLop.Text = l.MaLop;
                txtMaLop.ReadOnly = true;
                if (!string.IsNullOrWhiteSpace(l.MaKhoa))
                    cmbKhoaCuaLop.SelectedValue = l.MaKhoa;
            }
        }

        private void btnThemLop_Click(object? sender, EventArgs e)
        {
            string maLop = txtMaLop.Text.Trim().ToUpperInvariant();
            string maKhoa = cmbKhoaCuaLop.SelectedValue?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng nhập Mã lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataProvider.Instance.ExecuteNonQuery(
                "INSERT INTO Lop (Ma_Lop, Ma_Khoa) VALUES (@MaLop, @MaKhoa)",
                new[] { new SqlParameter("@MaLop", maLop), new SqlParameter("@MaKhoa", maKhoa) });

            maLopDangChon = "";
            txtMaLop.ReadOnly = false;
            txtMaLop.Clear();
            LoadDanhSachLop();
        }

        private void btnCapNhatLop_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maLopDangChon))
            {
                MessageBox.Show("Vui lòng chọn lớp cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maKhoa = cmbKhoaCuaLop.SelectedValue?.ToString() ?? "";
            DataProvider.Instance.ExecuteNonQuery(
                "UPDATE Lop SET Ma_Khoa = @MaKhoa WHERE Ma_Lop = @MaLop",
                new[] { new SqlParameter("@MaLop", maLopDangChon), new SqlParameter("@MaKhoa", maKhoa) });

            maLopDangChon = "";
            txtMaLop.ReadOnly = false;
            txtMaLop.Clear();
            LoadDanhSachLop();
        }

        private void btnXoaLop_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maLopDangChon))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa lớp '{maLopDangChon}' không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            DataProvider.Instance.ExecuteNonQuery(
                "DELETE FROM Lop WHERE Ma_Lop = @MaLop",
                new[] { new SqlParameter("@MaLop", maLopDangChon) });

            maLopDangChon = "";
            txtMaLop.ReadOnly = false;
            txtMaLop.Clear();
            LoadDanhSachLop();
        }

        private void tabControl1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            DongBoKetNoiDataProvider();

            if (tabControl1.SelectedTab == tabPage1)
            {
                LoadDanhSachMonLenLuoi();
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                NapDanhSachMonVaoCombo(cmbMonCH, themMucGoiY: true);
                bool laAdmin = VaiTroDangNhap.Equals("Admin", StringComparison.OrdinalIgnoreCase);
                if (!laAdmin || dangXemCaNhanCH)
                    LoadDanhSachCauHoiLenLuoi(cauhoibus.LocTheoGiaoVien(LayMaGVDangNhap()));
                else
                    LoadDanhSachCauHoiLenLuoi(cauhoibus.DocDSCauHoi());
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                NapDanhSachMonVaoCombo(cmbMonCH, themMucGoiY: true);
                if (dangXemCaNhanCH) LoadDanhSachCauHoiLenLuoi(cauhoibus.LocTheoGiaoVien(LayMaGVDangNhap()));
                else LoadDanhSachCauHoiLenLuoi(cauhoibus.DocDSCauHoi());
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                LoadDanhSachKyThi();
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                NapDanhSachMonVaoCombo(cmbMonDeThi, themMucGoiY: true);
                NapDanhSachMaTranVaoCombo();
                NapDanhSachKyThiVaoCombo();
                LoadDanhSachDeThi();
            }
            else if (tabControl1.SelectedTab == tabPageKhoaLop)
            {
                KhoiTaoTabKhoaLop();
            }
        }

        private void FormGiaoVien_Load_1(object sender, EventArgs e) { }
        private void lsvMonHoc_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void txtTenMon_TextChanged(object sender, EventArgs e) { }
        private void btnCapNhatMon_Click_1(object sender, EventArgs e) { }
        private void grpThongTinMon_Enter(object sender, EventArgs e) { }
        private void txtTimKiemCH_TextChanged(object sender, EventArgs e) { }

        // ================= GIAO DIỆN ĐỒNG BỘ =================
        private void ApDungGiaoDienDongBo()
        {
            // 1. Đồng bộ Form & Font với FormBaoCaoThongKe (1080x680, Segoe UI 9.5F)
            this.Size = new System.Drawing.Size(1080, 680);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);

            tabControl1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            foreach (TabPage tp in tabControl1.TabPages)
            {
                tp.BackColor = System.Drawing.Color.White;
                tp.Padding = new Padding(12);
            }

            // 2. Hàm tiện ích định dạng Button chuẩn Flat + bo viền & màu sắc theo vai trò
            void StyleButton(Button btn, System.Drawing.Color bg, System.Drawing.Color fg, bool bold = true)
            {
                if (btn == null) return;
                btn.BackColor = bg;
                btn.ForeColor = fg;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = bg == System.Drawing.Color.WhiteSmoke ? 1 : 0;
                btn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
                btn.Font = new System.Drawing.Font("Segoe UI", 9F, bold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular);
                btn.Height = 30;
                btn.Cursor = Cursors.Hand;
            }

            System.Drawing.Color colThem = System.Drawing.Color.DodgerBlue;       // Thêm / Tìm kiếm
            System.Drawing.Color colCapNhat = System.Drawing.Color.SeaGreen;      // Cập nhật
            System.Drawing.Color colXoa = System.Drawing.Color.Firebrick;         // Xóa
            System.Drawing.Color colExcel = System.Drawing.Color.ForestGreen;     // Nhập Excel
            System.Drawing.Color colDangTai = System.Drawing.Color.DarkSlateGray; // Đăng tải
            System.Drawing.Color colNeutral = System.Drawing.Color.WhiteSmoke;    // Làm mới / Lọc xem
            System.Drawing.Color colTextDark = System.Drawing.Color.FromArgb(30, 41, 59);

            // Tab 1: Môn học
            StyleButton(btnThemMon, colThem, System.Drawing.Color.White);
            StyleButton(btnCapNhatMon, colCapNhat, System.Drawing.Color.White);
            StyleButton(btnXoaMon, colXoa, System.Drawing.Color.White);
            StyleButton(btnLamMoiMon, colNeutral, colTextDark, false);
            StyleButton(btnTimMon, colThem, System.Drawing.Color.White);

            // Tab 2: Ma trận Đề thi
            StyleButton(btnThemMT, colThem, System.Drawing.Color.White);
            StyleButton(btnCapNhatMT, colCapNhat, System.Drawing.Color.White);
            StyleButton(btnXoaMT, colXoa, System.Drawing.Color.White);
            txtTongTyLe.BackColor = System.Drawing.Color.AliceBlue;
            txtTongTyLe.ForeColor = System.Drawing.Color.DarkBlue;
            txtTongTyLe.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);

            // Tab 3: Ngân hàng Câu hỏi
            StyleButton(btnThemCH, colThem, System.Drawing.Color.White);
            StyleButton(btnCapNhatCH, colCapNhat, System.Drawing.Color.White);
            StyleButton(btnXoaCH, colXoa, System.Drawing.Color.White);
            StyleButton(btnLamMoiCH, colNeutral, colTextDark, false);
            StyleButton(btnTimCH, colThem, System.Drawing.Color.White);
            StyleButton(btnXemTatCaCH, System.Drawing.Color.AliceBlue, System.Drawing.Color.DarkBlue);
            StyleButton(btnXemCaNhanCH, System.Drawing.Color.AliceBlue, System.Drawing.Color.DarkBlue);
            StyleButton(btnNhapExcelCH, colExcel, System.Drawing.Color.White);

            // Tab 4: Quản lý Kỳ thi
            StyleButton(btnThemKT, colThem, System.Drawing.Color.White);
            StyleButton(btnCapNhatKT, colCapNhat, System.Drawing.Color.White);
            StyleButton(btnXoaKT, colXoa, System.Drawing.Color.White);

            // Tab 5: Quản lý Đề thi
            StyleButton(btnThemDe, colThem, System.Drawing.Color.White);
            StyleButton(btnCapNhatDe, colCapNhat, System.Drawing.Color.White);
            StyleButton(btnXoaDe, colXoa, System.Drawing.Color.White);
            StyleButton(btnDangTaiDe, colDangTai, System.Drawing.Color.White);

            // Tab 6: Khoa - Lớp
            StyleButton(btnThemKhoa, colThem, System.Drawing.Color.White);
            StyleButton(btnCapNhatKhoa, colCapNhat, System.Drawing.Color.White);
            StyleButton(btnXoaKhoa, colXoa, System.Drawing.Color.White);
            StyleButton(btnThemLop, colThem, System.Drawing.Color.White);
            StyleButton(btnCapNhatLop, colCapNhat, System.Drawing.Color.White);
            StyleButton(btnXoaLop, colXoa, System.Drawing.Color.White);

            // 3. Căn chỉnh tỷ lệ & giãn full khung cho toàn bộ ListView + GroupBox
            void StyleListView(ListView lv)
            {
                if (lv == null) return;
                lv.BackColor = System.Drawing.Color.White;
                lv.Font = new System.Drawing.Font("Segoe UI", 9.5F);
                lv.FullRowSelect = true;
                lv.GridLines = true;
                lv.BorderStyle = BorderStyle.FixedSingle;
            }

            foreach (var lv in new[] { lsvMonHoc, lsvMaTran, lsvCauHoi, lsvKyThi, lsvDeThi, lsvKhoa, lsvLop })
                StyleListView(lv);

            // Tỷ lệ Tab 1 (Môn học)
            grpThongTinMon.Location = new System.Drawing.Point(14, 12);
            grpThongTinMon.Size = new System.Drawing.Size(1020, 76);
            grpThongTinMon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            btnThemMon.Location = new System.Drawing.Point(14, 98);
            btnCapNhatMon.Location = new System.Drawing.Point(114, 98);
            btnXoaMon.Location = new System.Drawing.Point(214, 98);
            btnLamMoiMon.Location = new System.Drawing.Point(314, 98);
            txtTimKiemMon.Location = new System.Drawing.Point(680, 100);
            txtTimKiemMon.Width = 248;
            txtTimKiemMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTimMon.Location = new System.Drawing.Point(936, 98);
            btnTimMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lsvMonHoc.Location = new System.Drawing.Point(14, 138);
            lsvMonHoc.Size = new System.Drawing.Size(1020, 470);
            lsvMonHoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            if (lsvMonHoc.Columns.Count >= 3)
            {
                lsvMonHoc.Columns[0].Width = 180;
                lsvMonHoc.Columns[1].Width = 640;
                lsvMonHoc.Columns[2].Width = 180;
            }

            // Tỷ lệ Tab 2 (Ma trận Đề thi)
            lsvMaTran.Location = new System.Drawing.Point(330, 16);
            lsvMaTran.Size = new System.Drawing.Size(704, 545);
            lsvMaTran.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThemMT.Location = new System.Drawing.Point(330, 572);
            btnThemMT.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCapNhatMT.Location = new System.Drawing.Point(416, 572);
            btnCapNhatMT.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaMT.Location = new System.Drawing.Point(502, 572);
            btnXoaMT.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // Tỷ lệ Tab 3 (Ngân hàng Câu hỏi)
            txtNoiDungCH.Width = 905;
            txtNoiDungCH.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDapAnA.Width = 455;
            txtDapAnB.Width = 455;
            txtDapAnC.Location = new System.Drawing.Point(565, 84);
            txtDapAnC.Width = 469;
            txtDapAnC.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDapAnD.Location = new System.Drawing.Point(565, 118);
            txtDapAnD.Width = 469;
            txtDapAnD.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.Location = new System.Drawing.Point(540, 87);
            label7.Location = new System.Drawing.Point(540, 122);

            btnNhapExcelCH.Width = 122;
            btnNhapExcelCH.Location = new System.Drawing.Point(912, 282);
            btnNhapExcelCH.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXemCaNhanCH.Location = new System.Drawing.Point(790, 282);
            btnXemCaNhanCH.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXemTatCaCH.Location = new System.Drawing.Point(686, 282);
            btnXemTatCaCH.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lsvCauHoi.Location = new System.Drawing.Point(17, 320);
            lsvCauHoi.Size = new System.Drawing.Size(1017, 290);
            lsvCauHoi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Tỷ lệ Tab 4 (Quản lý Kỳ thi)
            lsvKyThi.Location = new System.Drawing.Point(240, 24);
            lsvKyThi.Size = new System.Drawing.Size(794, 535);
            lsvKyThi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThemKT.Location = new System.Drawing.Point(240, 570);
            btnThemKT.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCapNhatKT.Location = new System.Drawing.Point(326, 570);
            btnCapNhatKT.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaKT.Location = new System.Drawing.Point(412, 570);
            btnXoaKT.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // Tỷ lệ Tab 5 (Quản lý Đề thi)
            lsvDeThi.Location = new System.Drawing.Point(240, 24);
            lsvDeThi.Size = new System.Drawing.Size(794, 535);
            lsvDeThi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThemDe.Location = new System.Drawing.Point(240, 570);
            btnThemDe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCapNhatDe.Location = new System.Drawing.Point(326, 570);
            btnCapNhatDe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaDe.Location = new System.Drawing.Point(412, 570);
            btnXoaDe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDangTaiDe.Width = 96;
            btnDangTaiDe.Location = new System.Drawing.Point(498, 570);
            btnDangTaiDe.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // Tỷ lệ Tab 6 (Quản lý Khoa - Lớp)
            grpKhoa.Location = new System.Drawing.Point(14, 14);
            grpKhoa.Size = new System.Drawing.Size(495, 594);
            grpKhoa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lsvKhoa.Size = new System.Drawing.Size(471, 425);
            lsvKhoa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThemKhoa.Location = new System.Drawing.Point(14, 548);
            btnThemKhoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCapNhatKhoa.Location = new System.Drawing.Point(100, 548);
            btnCapNhatKhoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaKhoa.Location = new System.Drawing.Point(186, 548);
            btnXoaKhoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            grpLop.Location = new System.Drawing.Point(524, 14);
            grpLop.Size = new System.Drawing.Size(510, 594);
            grpLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lsvLop.Size = new System.Drawing.Size(486, 425);
            lsvLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThemLop.Location = new System.Drawing.Point(16, 548);
            btnThemLop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCapNhatLop.Location = new System.Drawing.Point(102, 548);
            btnCapNhatLop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoaLop.Location = new System.Drawing.Point(188, 548);
            btnXoaLop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        }

        private void btnXoaKhoa_Click_1(object sender, EventArgs e)
        {

        }
    }
}