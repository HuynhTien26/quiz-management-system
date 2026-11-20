using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DTO;
using System;
using System.Collections.Generic;
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

        private const string MucDoCoBan = "Cơ bản";
        private const string MucDoVanDung = "Vận dụng";
        private const string MucDoNangCao = "Nâng cao";

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

            btnLamMoiMon.Click -= btnLamMoiMon_Click;
            btnLamMoiMon.Click += btnLamMoiMon_Click;

            btnTimMon.Click -= btnTimMon_Click;
            btnTimMon.Click += btnTimMon_Click;

            lsvMonHoc.SelectedIndexChanged -= lsvMonHoc_SelectedIndexChanged;
            lsvMonHoc.SelectedIndexChanged += lsvMonHoc_SelectedIndexChanged;

            btnThemCH.Click -= btnThemCH_Click;
            btnThemCH.Click += btnThemCH_Click;

            btnCapNhatCH.Click -= btnCapNhatCH_Click;
            btnCapNhatCH.Click += btnCapNhatCH_Click;

            btnXoaCH.Click -= btnXoaCH_Click;
            btnXoaCH.Click += btnXoaCH_Click;

            btnXemTatCaCH.Click -= btnXemTatCaCH_Click;
            btnXemTatCaCH.Click += btnXemTatCaCH_Click;

            btnXemCaNhanCH.Click -= btnXemCaNhanCH_Click;
            btnXemCaNhanCH.Click += btnXemCaNhanCH_Click;

            lsvCauHoi.SelectedIndexChanged -= lsvCauHoi_SelectedIndexChanged;
            lsvCauHoi.SelectedIndexChanged += lsvCauHoi_SelectedIndexChanged;
        }

        private void FormGiaoVien_Load(object sender, EventArgs e)
        {
            PhanQuyenGiaoDien();
            KhoiTaoTabMonHoc();
            KhoiTaoTabCauHoi();
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

                btnThemKT.Enabled = false;
                btnCapNhatKT.Enabled = false;
                btnXoaKT.Enabled = false;
            }

            // 4. Môn học: Admin được Thêm/Cập nhật/Xóa, Giáo viên chỉ được xem và tìm kiếm
            btnThemMon.Enabled = laAdmin;
            btnCapNhatMon.Enabled = laAdmin;
            btnXoaMon.Enabled = laAdmin;
        }

        // ================= TAB MÔN HỌC =================
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

            // numTinChi luôn có giá trị trong khoảng 1..15 nên không thể bỏ trống
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
                txtMaMon.ReadOnly = true; // Không cho sửa khóa chính
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

        // ================= TAB NGÂN HÀNG CÂU HỎI =================
        private void KhoiTaoTabCauHoi()
        {
            KhoiTaoCotCauHoi();
            NapDanhSachMonVaoCombo();
            NapDapAnDungVaoCombo();
            LoadTatCaCauHoi();
        }

        private void KhoiTaoCotCauHoi()
        {
            if (lsvCauHoi.Columns.Count > 0) return;

            lsvCauHoi.Columns.Add("Mã CH", 70);
            lsvCauHoi.Columns.Add("Môn", 90);
            lsvCauHoi.Columns.Add("Nội dung", 190);
            lsvCauHoi.Columns.Add("Đáp án", 65);
            lsvCauHoi.Columns.Add("Mức độ", 85);
        }

        private void NapDanhSachMonVaoCombo()
        {
            try
            {
                cmbMonCH.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbMonCH.DataSource = null;
                cmbMonCH.DisplayMember = "TenMon";
                cmbMonCH.ValueMember = "MaMon";

                List<MonDTO> dsMon = monbus.DocDSMon() ?? new List<MonDTO>();
                // Mục gợi ý ở đầu: chưa chọn môn nào
                dsMon.Insert(0, new MonDTO { MaMon = "", TenMon = "Chọn môn" });

                cmbMonCH.DataSource = dsMon;
                cmbMonCH.SelectedIndex = 0;
            }
            catch (Exception)
            {
                // Mất kết nối khi nạp môn thì để combo rỗng
            }
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
            LoadDanhSachCauHoiLenLuoi(cauhoibus.DocDSCauHoi());
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

            rdoCoBan.Checked = false;
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

        private void btnThemCH_Click(object sender, EventArgs e)
        {
            CauHoiDTO ch = TaoCauHoiTuForm(txtMaCH.Text);

            if (cauhoibus.ThemCauHoi(ch, out string thongBao))
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTatCaCauHoi();
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
                LoadTatCaCauHoi();
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
                LoadTatCaCauHoi();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemTatCaCH_Click(object sender, EventArgs e)
        {
            LoadTatCaCauHoi();
        }

        private void btnXemCaNhanCH_Click(object sender, EventArgs e)
        {
            LoadDanhSachCauHoiLenLuoi(cauhoibus.LocTheoGiaoVien(LayMaGVDangNhap()));
            LamMoiFormCauHoi();
        }

        private void FormGiaoVien_Load_1(object sender, EventArgs e)
        {

        }

        private void lsvMonHoc_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void txtTenMon_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCapNhatMon_Click_1(object sender, EventArgs e)
        {

        }
    }
}
