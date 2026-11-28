using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormAdmin : Form
    {
        AdminBUS adminBUS = new AdminBUS();
        private bool dangChonTuBang = false;
        string maDangChon = "";
        private List<KhoaDTO> danhSachKhoaDong = new List<KhoaDTO>();

        // Các ComboBox lọc Học kỳ & Năm học cho 2 Tab Thống kê
        

        public FormAdmin()
        {
            InitializeComponent();
            ApDungGiaoDienDongBo();

            this.Load -= FormAdmin_Load;
            this.Load += FormAdmin_Load;

            lsvDanhSach.SelectedIndexChanged -= lsvDanhSach_SelectedIndexChanged;
            lsvDanhSach.SelectedIndexChanged += lsvDanhSach_SelectedIndexChanged;

            this.MouseDown += FormAdmin_VungTrong_MouseDown;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel || ctrl is GroupBox || ctrl is Label)
                {
                    ctrl.MouseDown += FormAdmin_VungTrong_MouseDown;
                }
            }
        }

        private void ApDungGiaoDienDongBo()
        {
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.MinimumSize = new Size(1020, 640);

            void StyleButton(Button? btn, System.Drawing.Color bg, System.Drawing.Color fg, bool bold = true)
            {
                if (btn == null) return;
                btn.BackColor = bg;
                btn.ForeColor = fg;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new System.Drawing.Font("Segoe UI", 9F, bold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular);
                btn.Cursor = Cursors.Hand;
            }

            StyleButton(btnThem, System.Drawing.Color.DodgerBlue, System.Drawing.Color.White);
            StyleButton(btnCapNhat, System.Drawing.Color.SeaGreen, System.Drawing.Color.White);
            StyleButton(btnXoa, System.Drawing.Color.Firebrick, System.Drawing.Color.White);
            StyleButton(btnChonNhanSu, System.Drawing.Color.SteelBlue, System.Drawing.Color.White);

            foreach (Control c in GetAllControls(this))
            {
                if (c is Button btn && btn != btnThem && btn != btnCapNhat && btn != btnXoa && btn != btnChonNhanSu)
                {
                    string t = btn.Text.Trim();
                    if (t.Contains("Xuất", StringComparison.OrdinalIgnoreCase) || t.Contains("Excel", StringComparison.OrdinalIgnoreCase))
                        StyleButton(btn, System.Drawing.Color.ForestGreen, System.Drawing.Color.White);
                }
                else if (c is ListView lv)
                {
                    lv.BackColor = System.Drawing.Color.White;
                    lv.Font = new System.Drawing.Font("Segoe UI", 9.5F);
                    lv.FullRowSelect = true;
                    lv.GridLines = true;
                    lv.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (c is TextBox tb && tb.ReadOnly)
                {
                    tb.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
                    tb.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
                }
            }

            // --- CỘT 1: THÔNG TIN CƠ BẢN ---
            label1.Location = new Point(16, 18);
            txtMa.Location = new Point(88, 15);
            txtMa.Size = new Size(125, 23);
            btnChonNhanSu.Location = new Point(219, 15);
            btnChonNhanSu.Size = new Size(36, 23);

            label2.Location = new Point(16, 52);
            txtHo.Location = new Point(88, 49);
            txtHo.Size = new Size(125, 23);

            label3.Location = new Point(222, 52);
            txtTen.Location = new Point(256, 49);
            txtTen.Size = new Size(115, 23);

            label7.Location = new Point(16, 86);
            dtpNgaySinh.Location = new Point(88, 83);
            dtpNgaySinh.Size = new Size(125, 23);

            label4.Location = new Point(16, 120);
            rdoNam.Location = new Point(88, 118);
            rdoNu.Location = new Point(155, 118);

            // --- CỘT 2: VAI TRÒ - KHOA - LỚP ---
            panel1.Location = new Point(400, 10);
            panel1.Size = new Size(215, 48);
            label6.Location = new Point(4, 4);
            rdoGiaoVien.Location = new Point(4, 24);
            rdoSinhVien.Location = new Point(98, 24);

            label5.Location = new Point(404, 66);
            cmbKhoa.Location = new Point(404, 84);
            cmbKhoa.Size = new Size(205, 23);

            label9.Location = new Point(404, 112);
            cmbLop.Location = new Point(404, 130);
            cmbLop.Size = new Size(205, 23);

            // --- CỘT 3: MÔN PHỤ TRÁCH ---
            label8.Location = new Point(635, 12);
            clbMon.Location = new Point(635, 32);
            clbMon.Size = new Size(250, 122);
            clbMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // --- TAB CONTROL CHÍNH ---
            tabMain.Location = new Point(12, 166);
            tabMain.Size = new Size(Math.Max(820, this.ClientSize.Width - 24), Math.Max(420, this.ClientSize.Height - 178));
            tabMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            foreach (TabPage tp in tabMain.TabPages)
            {
                tp.BackColor = System.Drawing.Color.White;
                tp.Padding = new Padding(10);
            }

            SapXepCacTabBenTrong();
            tabMain.SizeChanged -= TabMain_SizeChanged_Layout;
            tabMain.SizeChanged += TabMain_SizeChanged_Layout;
        }
        private void TabMain_SizeChanged_Layout(object? sender, EventArgs e)
        {
            SapXepCacTabBenTrong();
        }
        private void SapXepCacTabBenTrong()
        {
            // 1. Tab Hồ sơ
            int wDanhSach = Math.Max(400, tabDanhSach.ClientSize.Width);
            int hDanhSach = Math.Max(220, tabDanhSach.ClientSize.Height);
            int nutWidth = 98;
            int gap = 12;

            lsvDanhSach.Location = new Point(10, 10);
            lsvDanhSach.Size = new Size(Math.Max(480, wDanhSach - nutWidth - gap * 2 - 10), Math.Max(200, hDanhSach - 20));
            lsvDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            int xNut = lsvDanhSach.Right + gap;
            btnThem.Location = new Point(xNut, 12);
            btnThem.Size = new Size(nutWidth, 32);
            btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnCapNhat.Location = new Point(xNut, 52);
            btnCapNhat.Size = new Size(nutWidth, 32);
            btnCapNhat.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnXoa.Location = new Point(xNut, 92);
            btnXoa.Size = new Size(nutWidth, 32);
            btnXoa.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // 2. Tab Tiến độ Sinh viên
            int wSV = Math.Max(560, tabTienDo.ClientSize.Width);
            int hSV = Math.Max(260, tabTienDo.ClientSize.Height);

            int col1LabelX = 16;
            int col1ControlX = 128;
            int col1Width = 120;

            int col2LabelX = 276;
            int col2ControlX = 392;
            int col2Width = 135;

            label11.AutoSize = false;
            label11.Location = new Point(col1LabelX, 20);
            label11.Size = new Size(106, 22);
            label11.TextAlign = ContentAlignment.MiddleLeft;

            txtTongTinChi.Location = new Point(col1ControlX, 18);
            txtTongTinChi.Size = new Size(col1Width, 24);

            label10.AutoSize = false;
            label10.Text = "Điểm trung bình:";
            label10.Location = new Point(col2LabelX, 20);
            label10.Size = new Size(112, 22);
            label10.TextAlign = ContentAlignment.MiddleLeft;

            txtDiemTBXepLoai.Location = new Point(col2ControlX, 18);
            txtDiemTBXepLoai.Size = new Size(col2Width, 24);

            label14.AutoSize = false;
            label14.Location = new Point(col1LabelX, 56);
            label14.Size = new Size(106, 22);
            label14.TextAlign = ContentAlignment.MiddleLeft;

            cboHocKySV.Location = new Point(col1ControlX, 54);
            cboHocKySV.Size = new Size(col1Width, 23);

            label15.AutoSize = false;
            label15.Location = new Point(col2LabelX, 56);
            label15.Size = new Size(112, 22);
            label15.TextAlign = ContentAlignment.MiddleLeft;

            cboNamHocSV.Location = new Point(col2ControlX, 54);
            cboNamHocSV.Size = new Size(col2Width, 23);

            lsvKetQuaMonHoc.Location = new Point(16, 92);
            lsvKetQuaMonHoc.Size = new Size(wSV - 32, Math.Max(150, hSV - 104));
            lsvKetQuaMonHoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 3. Tab Thống kê Giáo viên
            int wGV = Math.Max(500, tabThongKeGV.ClientSize.Width);
            int hGV = Math.Max(260, tabThongKeGV.ClientSize.Height);

            label12.Location = new Point(16, 22);
            txtTongSoDe.Location = new Point(138, 18);
            txtTongSoDe.Size = new Size(115, 24);

            label13.Location = new Point(278, 22);
            txtTongSoCauHoi.Location = new Point(452, 18);
            txtTongSoCauHoi.Size = new Size(125, 24);

            label16.Location = new Point(16, 58);
            cboHocKyGV.Location = new Point(138, 54);
            cboHocKyGV.Size = new Size(115, 23);

            label17.Location = new Point(278, 58);
            cboNamHocGV.Location = new Point(452, 54);
            cboNamHocGV.Size = new Size(125, 23);

            lsvThongKeDeThi.Location = new Point(16, 92);
            lsvThongKeDeThi.Size = new Size(wGV - 32, Math.Max(150, hGV - 104));
            lsvThongKeDeThi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            if (lsvThongKeDeThi.Columns.Count >= 4)
            {
                lsvThongKeDeThi.Columns[0].Width = 140;
                lsvThongKeDeThi.Columns[1].Width = 240;
                lsvThongKeDeThi.Columns[2].Width = 130;
                lsvThongKeDeThi.Columns[3].Width = 150;
            }
        }
        private static System.Collections.Generic.IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                yield return c;
                foreach (Control child in GetAllControls(c))
                    yield return child;
            }
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

            // 1. Đổi chữ "Thí sinh" -> "Sinh viên"
            rdoSinhVien.Text = "Sinh viên";

            // 2. Nới rộng các cột trên ListView
            if (lsvDanhSach.Columns.Count > 8)
            {
                lsvDanhSach.Columns[1].Width = 110; // Họ lót
                lsvDanhSach.Columns[4].Width = 155; // Khoa
                lsvDanhSach.Columns[5].Width = 150; // Môn
                lsvDanhSach.Columns[8].Width = 90;  // Vai trò
            }

            // 3. Khởi tạo bộ lọc Học kỳ & Năm học cho Tab Tiến độ SV và Tab Thống kê GV
            KhoiTaoBoLocHocKyVaNamHoc();

            // 4. Nạp dữ liệu động từ SQL
            LoadAllComboBoxDong();
            LoadTatCaNhanSuLenBang();

            
        }

        private void KhoiTaoBoLocHocKyVaNamHoc()
        {
            // 1. Lấy danh sách Học kỳ và Năm học (20xy-20yz) từ bảng Ky_Thi trong SQL Server
            List<string> dsHocKySQL = adminBUS.DocDSHocKy();
            if (dsHocKySQL.Count == 0)
                dsHocKySQL.AddRange(new[] { "Học kỳ 1", "Học kỳ 2", "Học kỳ 3" });
            dsHocKySQL.Insert(0, "Tất cả");

            List<string> dsNamHocSQL = adminBUS.DocDSNamHoc();
            if (dsNamHocSQL.Count == 0)
                dsNamHocSQL.AddRange(new[] { "2024-2025", "2025-2026", "2026-2027" });
            dsNamHocSQL.Insert(0, "Tất cả");

            // 2. Đổ dữ liệu vào ComboBox trên Tab Tiến độ Sinh viên (đã kéo thả)
            if (cboHocKySV != null && cboNamHocSV != null)
            {
                cboHocKySV.DropDownStyle = ComboBoxStyle.DropDownList;
                cboHocKySV.Items.Clear();
                cboHocKySV.Items.AddRange(dsHocKySQL.ToArray());
                if (cboHocKySV.Items.Count > 0) cboHocKySV.SelectedIndex = 0;

                cboNamHocSV.DropDownStyle = ComboBoxStyle.DropDownList;
                cboNamHocSV.Items.Clear();
                cboNamHocSV.Items.AddRange(dsNamHocSQL.ToArray());
                if (cboNamHocSV.Items.Count > 0) cboNamHocSV.SelectedIndex = 0;

                // Gắn sự kiện khi thay đổi lựa chọn bộ lọc
                cboHocKySV.SelectedIndexChanged -= CboBoLocSV_SelectedIndexChanged;
                cboHocKySV.SelectedIndexChanged += CboBoLocSV_SelectedIndexChanged;
                cboNamHocSV.SelectedIndexChanged -= CboBoLocSV_SelectedIndexChanged;
                cboNamHocSV.SelectedIndexChanged += CboBoLocSV_SelectedIndexChanged;
            }

            // 3. Đổ dữ liệu vào ComboBox trên Tab Thống kê Giáo viên (đã kéo thả)
            if (cboHocKyGV != null && cboNamHocGV != null)
            {
                cboHocKyGV.DropDownStyle = ComboBoxStyle.DropDownList;
                cboHocKyGV.Items.Clear();
                cboHocKyGV.Items.AddRange(dsHocKySQL.ToArray());
                if (cboHocKyGV.Items.Count > 0) cboHocKyGV.SelectedIndex = 0;

                cboNamHocGV.DropDownStyle = ComboBoxStyle.DropDownList;
                cboNamHocGV.Items.Clear();
                cboNamHocGV.Items.AddRange(dsNamHocSQL.ToArray());
                if (cboNamHocGV.Items.Count > 0) cboNamHocGV.SelectedIndex = 0;

                // Gắn sự kiện khi thay đổi lựa chọn bộ lọc
                cboHocKyGV.SelectedIndexChanged -= CboBoLocGV_SelectedIndexChangedTab3;
                cboHocKyGV.SelectedIndexChanged += CboBoLocGV_SelectedIndexChangedTab3;
                cboNamHocGV.SelectedIndexChanged -= CboBoLocGV_SelectedIndexChangedTab3;
                cboNamHocGV.SelectedIndexChanged += CboBoLocGV_SelectedIndexChangedTab3;
            }
        }
        private void CboBoLocSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(maDangChon) && rdoSinhVien.Checked)
                HienThiTienDoTinChiSinhVien(maDangChon);
        }

        private void CboBoLocGV_SelectedIndexChangedTab3(object sender, EventArgs e)
        {
            string maGV = !string.IsNullOrWhiteSpace(maDangChon) ? maDangChon : txtMa.Text.Trim();
            if (!string.IsNullOrWhiteSpace(maGV) && rdoGiaoVien.Checked)
                HienThiThongKeGiaoVien(maGV);
            else
                ResetThongKeGiaoVien();
        }



        private void LoadAllComboBoxDong()
        {
            danhSachKhoaDong = adminBUS.DocDSKhoa() ?? new List<KhoaDTO>();
            var dsMon = adminBUS.DocDSMon();
            var dsLop = adminBUS.DocDSLop();

            if (danhSachKhoaDong.Count > 0)
            {
                cmbKhoa.DataSource = new List<KhoaDTO>(danhSachKhoaDong);
                cmbKhoa.DisplayMember = "TenKhoa";
                cmbKhoa.ValueMember = "MaKhoa";
                cmbKhoa.SelectedIndex = -1;
            }

            if (dsLop != null && dsLop.Count > 0)
            {
                cmbLop.DataSource = new List<LopDTO>(dsLop);
                cmbLop.DisplayMember = "MaLop";
                cmbLop.ValueMember = "MaLop";
                cmbLop.SelectedIndex = -1;
            }

            clbMon.Items.Clear();
            if (dsMon != null)
            {
                foreach (var mon in dsMon)
                {
                    clbMon.Items.Add(mon.TenMon);
                }
            }
        }

        private string HienThiTenKhoaDayDu(string maHoacTenKhoa)
        {
            if (string.IsNullOrWhiteSpace(maHoacTenKhoa)) return "";
            string key = maHoacTenKhoa.Trim();

            if (danhSachKhoaDong != null)
            {
                foreach (var k in danhSachKhoaDong)
                {
                    if (k.MaKhoa.Equals(key, StringComparison.OrdinalIgnoreCase) ||
                        k.TenKhoa.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        return k.TenKhoa;
                    }
                }
            }
            return key;
        }

        private void LoadTatCaNhanSuLenBang()
        {
            AdminBUS.dsgv = null;
            AdminBUS.dssv = null;

            List<GiangVienDTO> dsGV = adminBUS.DocDSGV();
            List<SinhVienDTO> dsSV = adminBUS.DocDSSV();

            DoDuLieuGopLenListView(dsGV, dsSV);
        }

        private void FormAdmin_VungTrong_MouseDown(object sender, MouseEventArgs e)
        {
            LamSachControl();
            LoadTatCaNhanSuLenBang();
        }

        private void LamSachControl()
        {
            maDangChon = "";
            txtMa.Clear();
            txtHo.Clear();
            txtTen.Clear();
            txtMa.ReadOnly = false;
            dtpNgaySinh.Value = DateTime.Now;
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            if (cmbKhoa.Items.Count > 0) cmbKhoa.SelectedIndex = -1;
            cmbKhoa.Text = "";
            if (cmbLop.Items.Count > 0) cmbLop.SelectedIndex = -1;
            cmbLop.Text = "";
            BoChonTatCaMon();

            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                lsvDanhSach.SelectedItems.Clear();
            }

            if (lsvKetQuaMonHoc != null) lsvKetQuaMonHoc.Items.Clear();
            if (txtTongTinChi != null) txtTongTinChi.Text = "0 tín chỉ";
            if (txtDiemTBXepLoai != null) txtDiemTBXepLoai.Text = "0";

            ResetThongKeGiaoVien();
        }

        private void rdoGiaoVien_CheckedChanged(object sender, EventArgs e)
        {
            if (dangChonTuBang) return;

            if (rdoGiaoVien.Checked)
            {
                // Giữ nguyên dữ liệu các ô đã nhập, chỉ bật Khoa + Môn và tắt Lớp
                cmbKhoa.Enabled = true;
                clbMon.Enabled = true;

                cmbLop.Enabled = false;
                cmbLop.SelectedIndex = -1;
                cmbLop.Text = "";

                if (txtMa.ReadOnly)
                {
                    txtMa.ReadOnly = false;
                    maDangChon = "";
                }

                HienThiDanhSachGiaoVien();
            }
        }

        private void rdoSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            if (dangChonTuBang) return;

            if (rdoSinhVien.Checked)
            {
                // Giữ nguyên dữ liệu các ô đã nhập, chỉ bật Khoa + Lớp và tắt Môn
                cmbKhoa.Enabled = true;
                cmbLop.Enabled = true;

                clbMon.Enabled = false;
                BoChonTatCaMon();

                if (txtMa.ReadOnly)
                {
                    txtMa.ReadOnly = false;
                    maDangChon = "";
                }

                HienThiDanhSachSinhVien();
            }
        }

        private void DoDuLieuGVLenListView(List<GiangVienDTO> dsGV)
        {
            lsvDanhSach.Items.Clear();
            foreach (GiangVienDTO gv in dsGV)
            {
                ListViewItem item = new ListViewItem(gv.MaGV);
                item.SubItems.Add(gv.HoLot);
                item.SubItems.Add(gv.Ten);
                item.SubItems.Add(gv.GioiTinh);
                item.SubItems.Add(HienThiTenKhoaDayDu(gv.MaKhoa));
                item.SubItems.Add(gv.MaMon);
                item.SubItems.Add(gv.NgaySinh.ToString("dd/MM/yyyy"));
                item.SubItems.Add("");
                item.SubItems.Add("Giáo viên");
                lsvDanhSach.Items.Add(item);
            }
        }

        private void DoDuLieuSVLenListView(List<SinhVienDTO> dsSV)
        {
            lsvDanhSach.Items.Clear();
            foreach (SinhVienDTO sv in dsSV)
            {
                ListViewItem item = new ListViewItem(sv.MaSV);
                item.SubItems.Add(sv.HoLot);
                item.SubItems.Add(sv.Ten);
                item.SubItems.Add(sv.GioiTinh);
                item.SubItems.Add(HienThiTenKhoaDayDu(sv.MaKhoa));
                item.SubItems.Add("");
                item.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
                item.SubItems.Add(sv.Lop);
                item.SubItems.Add("Sinh viên");
                lsvDanhSach.Items.Add(item);
            }
        }

        private void HienThiDanhSachGiaoVien()
        {
            List<GiangVienDTO> ds = adminBUS.DocDSGV();
            DoDuLieuGVLenListView(ds);
        }

        private void HienThiDanhSachSinhVien()
        {
            List<SinhVienDTO> ds = adminBUS.DocDSSV();
            DoDuLieuSVLenListView(ds);
        }

        private void TickMonHocCuaGiaoVien(string monGV)
        {
            BoChonTatCaMon();
            if (string.IsNullOrWhiteSpace(monGV)) return;

            for (int i = 0; i < clbMon.Items.Count; i++)
            {
                string tenMonItem = clbMon.Items[i].ToString()!;
                if (monGV.IndexOf(tenMonItem, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    tenMonItem.IndexOf(monGV.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    clbMon.SetItemChecked(i, true);
                }
            }
        }

        private void ChonGiaTriComboBoxKhoa(string giaTriKhoa)
        {
            if (string.IsNullOrWhiteSpace(giaTriKhoa))
            {
                cmbKhoa.SelectedIndex = -1;
                return;
            }

            string key = giaTriKhoa.Trim();
            foreach (var item in cmbKhoa.Items)
            {
                if (item is KhoaDTO k &&
                    (k.TenKhoa.Equals(key, StringComparison.OrdinalIgnoreCase) ||
                     k.MaKhoa.Equals(key, StringComparison.OrdinalIgnoreCase)))
                {
                    cmbKhoa.SelectedValue = k.MaKhoa;
                    return;
                }
            }

            cmbKhoa.Text = key;
        }

        private void ChonGiaTriComboBoxLop(string giaTriLop)
        {
            if (string.IsNullOrWhiteSpace(giaTriLop))
            {
                cmbLop.SelectedIndex = -1;
                return;
            }
            cmbLop.SelectedValue = giaTriLop.Trim();
            if (cmbLop.SelectedIndex == -1)
            {
                cmbLop.Text = giaTriLop.Trim();
            }
        }

        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0) return;

            dangChonTuBang = true;
            try
            {
                ListViewItem item = lsvDanhSach.SelectedItems[0];

                string vaiTro = (item.SubItems.Count > 8 && !string.IsNullOrWhiteSpace(item.SubItems[8].Text))
                                ? item.SubItems[8].Text.Trim()
                                : (item.SubItems[0].Text.Trim().ToUpper().StartsWith("GV") ? "Giáo viên" : "Sinh viên");

                if (vaiTro == "Giáo viên")
                {
                    rdoGiaoVien.Checked = true;
                    cmbLop.Enabled = false;
                    clbMon.Enabled = true;
                }
                else
                {
                    rdoSinhVien.Checked = true;
                    cmbLop.Enabled = true;
                    clbMon.Enabled = false;
                }

                maDangChon = item.SubItems[0].Text.Trim();
                txtMa.Text = maDangChon;
                txtMa.ReadOnly = true;
                txtHo.Text = item.SubItems[1].Text;
                txtTen.Text = item.SubItems[2].Text;

                string gt = item.SubItems[3].Text.Trim();
                rdoNam.Checked = (gt == "Nam");
                rdoNu.Checked = (gt == "Nữ");

                string tenKhoaTrenBang = item.SubItems[4].Text.Trim();
                ChonGiaTriComboBoxKhoa(tenKhoaTrenBang);

                if (DateTime.TryParseExact(item.SubItems[6].Text.Trim(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                }

                if (vaiTro == "Giáo viên")
                {
                    cmbLop.SelectedIndex = -1;
                    cmbLop.Text = "";
                    TickMonHocCuaGiaoVien(item.SubItems[5].Text);
                    HienThiThongKeGiaoVien(txtMa.Text);
                }
                else
                {
                    string maLop = item.SubItems.Count > 7 ? item.SubItems[7].Text.Trim() : "";
                    ChonGiaTriComboBoxLop(maLop);

                    BoChonTatCaMon();
                    HienThiTienDoTinChiSinhVien(txtMa.Text);
                }
            }
            finally
            {
                dangChonTuBang = false;
            }
        }

        // ================= NÚT "..." TRA CỨU BẰNG BIẾN STATIC =================
        private void btnChonNhanSu_Click(object sender, EventArgs e)
        {
            FormTraCuu.GVDuocChon = null;
            FormTraCuu.SVDuocChon = null;

            FormTraCuu frm = new FormTraCuu();
            frm.ShowDialog();

            dangChonTuBang = true;
            try
            {
                if (FormTraCuu.GVDuocChon != null)
                {
                    GiangVienDTO gv = FormTraCuu.GVDuocChon;

                    rdoGiaoVien.Checked = true;
                    clbMon.Enabled = true;
                    cmbLop.Enabled = false;
                    cmbLop.SelectedIndex = -1;
                    cmbLop.Text = "";

                    maDangChon = gv.MaGV;
                    txtMa.Text = gv.MaGV;
                    txtMa.ReadOnly = true;
                    txtHo.Text = gv.HoLot;
                    txtTen.Text = gv.Ten;
                    dtpNgaySinh.Value = gv.NgaySinh;
                    rdoNam.Checked = (gv.GioiTinh == "Nam");
                    rdoNu.Checked = (gv.GioiTinh == "Nữ");
                    ChonGiaTriComboBoxKhoa(gv.MaKhoa);

                    TickMonHocCuaGiaoVien(gv.MaMon);
                    HienThiThongKeGiaoVien(gv.MaGV);
                    tabMain.SelectedTab = tabThongKeGV;
                }
                else if (FormTraCuu.SVDuocChon != null)
                {
                    SinhVienDTO sv = FormTraCuu.SVDuocChon;

                    rdoSinhVien.Checked = true;
                    cmbLop.Enabled = true;
                    clbMon.Enabled = false;
                    BoChonTatCaMon();

                    maDangChon = sv.MaSV;
                    txtMa.Text = sv.MaSV;
                    txtMa.ReadOnly = true;
                    txtHo.Text = sv.HoLot;
                    txtTen.Text = sv.Ten;
                    dtpNgaySinh.Value = sv.NgaySinh;
                    rdoNam.Checked = (sv.GioiTinh == "Nam");
                    rdoNu.Checked = (sv.GioiTinh == "Nữ");
                    ChonGiaTriComboBoxKhoa(sv.MaKhoa);
                    ChonGiaTriComboBoxLop(sv.Lop);

                    HienThiTienDoTinChiSinhVien(sv.MaSV);
                    tabMain.SelectedTab = tabTienDo;
                }
            }
            finally
            {
                dangChonTuBang = false;
            }
        }

        private void BoChonTatCaMon()
        {
            for (int i = 0; i < clbMon.Items.Count; i++)
            {
                clbMon.SetItemChecked(i, false);
            }
            clbMon.ClearSelected();
        }

        private List<string> LayDanhSachMonDaChon()
        {
            List<string> dsMon = new List<string>();
            foreach (var item in clbMon.CheckedItems)
            {
                dsMon.Add(item.ToString()!);
            }
            if (dsMon.Count == 0 && clbMon.SelectedItem != null)
            {
                dsMon.Add(clbMon.SelectedItem.ToString()!);
            }
            return dsMon;
        }

        private string LayChuoiMonDaChon()
        {
            return string.Join(", ", LayDanhSachMonDaChon());
        }

        private string LayMaKhoaTuComboBox()
        {
            if (cmbKhoa.SelectedValue != null)
                return cmbKhoa.SelectedValue.ToString()!.Trim();
            return cmbKhoa.Text.Trim();
        }

        private string LayMaLopTuComboBox()
        {
            if (cmbLop.SelectedValue != null)
                return cmbLop.SelectedValue.ToString()!.Trim();
            return cmbLop.Text.Trim();
        }

        // ================= NÚT THÊM (CÓ KIỂM TRA BÁO LỖI TRÙNG MÃ) =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMa.Text.Trim();
            string ho = txtHo.Text.Trim();
            string ten = txtTen.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = rdoNam.Checked ? "Nam" : (rdoNu.Checked ? "Nữ" : "");
            string khoa = LayMaKhoaTuComboBox();

            if (!rdoGiaoVien.Checked && !rdoSinhVien.Checked)
            {
                MessageBox.Show("Vui lòng chọn Vai trò (Giáo viên hoặc Sinh viên)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ho) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã, Họ, Tên và chọn Giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rdoGiaoVien.Checked)
            {
                if (adminBUS.KiemTraTrungMaGV(ma))
                {
                    MessageBox.Show($"Mã nhân sự '{ma}' đã tồn tại trong hệ thống! Vui lòng nhập mã khác.", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMa.Focus();
                    txtMa.SelectAll();
                    return;
                }

                string mon = LayChuoiMonDaChon();
                GiangVienDTO gvMoi = new GiangVienDTO(ma, ho, ten, ngaySinh, gioiTinh, khoa, mon);

                adminBUS.ThemGiaoVien(gvMoi);
                MessageBox.Show("Thêm giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTatCaNhanSuLenBang();
                LamSachControl();
            }
            else if (rdoSinhVien.Checked)
            {
                if (adminBUS.KiemTraTrungMaSV(ma))
                {
                    MessageBox.Show($"Mã nhân sự '{ma}' đã tồn tại trong hệ thống! Vui lòng nhập mã khác.", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMa.Focus();
                    txtMa.SelectAll();
                    return;
                }

                string lop = LayMaLopTuComboBox();
                SinhVienDTO svMoi = new SinhVienDTO(ma, ho, ten, ngaySinh, gioiTinh, khoa, lop);

                adminBUS.ThemSinhVien(svMoi);
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTatCaNhanSuLenBang();
                LamSachControl();
            }
        }

        // ================= NÚT CẬP NHẬT =================
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show("Vui lòng chọn một người từ danh sách trước khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMoi = txtMa.Text.Trim();
            string ho = txtHo.Text.Trim();
            string ten = txtTen.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = rdoNam.Checked ? "Nam" : (rdoNu.Checked ? "Nữ" : "");
            string khoa = LayMaKhoaTuComboBox();

            if (rdoGiaoVien.Checked)
            {
                if (!maMoi.Equals(maDangChon, StringComparison.OrdinalIgnoreCase) && adminBUS.KiemTraTrungMaGV(maMoi))
                {
                    MessageBox.Show($"Mã giáo viên mới '{maMoi}' đã bị trùng với người khác!", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMa.Focus();
                    txtMa.SelectAll();
                    return;
                }

                string mon = LayChuoiMonDaChon();
                GiangVienDTO gvCapNhat = new GiangVienDTO(maMoi, ho, ten, ngaySinh, gioiTinh, khoa, mon);

                adminBUS.CapNhatGiaoVien(maDangChon, gvCapNhat);
                MessageBox.Show("Cập nhật thông tin giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTatCaNhanSuLenBang();
                LamSachControl();
            }
            else if (rdoSinhVien.Checked)
            {
                if (!maMoi.Equals(maDangChon, StringComparison.OrdinalIgnoreCase) && adminBUS.KiemTraTrungMaSV(maMoi))
                {
                    MessageBox.Show($"Mã sinh viên mới '{maMoi}' đã bị trùng với người khác!", "Lỗi trùng mã", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMa.Focus();
                    txtMa.SelectAll();
                    return;
                }

                string lop = LayMaLopTuComboBox();
                SinhVienDTO svCapNhat = new SinhVienDTO(maMoi, ho, ten, ngaySinh, gioiTinh, khoa, lop);

                adminBUS.CapNhatSinhVien(maDangChon, svCapNhat);
                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTatCaNhanSuLenBang();
                LamSachControl();
            }
        }

        // ================= NÚT XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMa.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn một người từ danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa mã '{ma}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            if (rdoGiaoVien.Checked)
            {
                adminBUS.XoaGiaoVien(ma);
                MessageBox.Show("Đã xóa giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTatCaNhanSuLenBang();
                LamSachControl();
            }
            else if (rdoSinhVien.Checked)
            {
                adminBUS.XoaSinhVien(ma);
                MessageBox.Show("Đã xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTatCaNhanSuLenBang();
                LamSachControl();
            }
        }

        // ================= HÀM XỬ LÝ CHO BẢNG PHỤ (MAIN-SUB) KÈM LỌC HỌC KỲ & NĂM HỌC =================
        private void HienThiTienDoTinChiSinhVien(string maSV)
        {
            string hocKy = cboHocKySV.SelectedItem?.ToString() ?? "Tất cả";
            string namHoc = cboNamHocSV.SelectedItem?.ToString() ?? "Tất cả";

            List<TienDoHocTapDTO> kqht = adminBUS.LayKetQuaHocTap(maSV, hocKy, namHoc);

            lsvKetQuaMonHoc.Items.Clear();
            double tongDiem = 0;
            int tongTinChiDat = 0;
            int soMonDaThi = 0;

            foreach (var kq in kqht)
            {
                ListViewItem item = new ListViewItem(kq.MaMon);
                item.SubItems.Add(kq.TenMon);
                item.SubItems.Add(kq.TinChi.ToString());
                item.SubItems.Add(kq.DiemThi.ToString());

                string diemChu = TinhDiemChu(kq.DiemThi);
                item.SubItems.Add(diemChu);

                string ketQua = kq.DiemThi >= 4.0 ? "Đạt" : "Rớt";
                item.SubItems.Add(ketQua);

                lsvKetQuaMonHoc.Items.Add(item);

                if (kq.DiemThi >= 4.0) tongTinChiDat += kq.TinChi;
                tongDiem += kq.DiemThi;
                soMonDaThi++;
            }

            txtTongTinChi.Text = tongTinChiDat.ToString() + " tín chỉ";
            txtDiemTBXepLoai.Text = soMonDaThi > 0 ? Math.Round(tongDiem / soMonDaThi, 2).ToString() : "0";
        }

        private void ResetThongKeGiaoVien()
        {
            lsvThongKeDeThi.Items.Clear();
            txtTongSoDe.Text = "0 đề";
            txtTongSoCauHoi.Text = "0 câu";
        }

        private int DemSoCauHoiDongGopCuaGV(string maGV, string hocKy, string namHoc)
        {
            try
            {
                return adminBUS.DemSoCauHoiDongGopCuaGV(maGV, hocKy, namHoc);
            }
            catch
            {
                return 0;
            }
        }

        private void HienThiThongKeGiaoVien(string maGV)
        {
            if (string.IsNullOrWhiteSpace(maGV))
            {
                ResetThongKeGiaoVien();
                return;
            }

            string hocKy = cboHocKyGV.SelectedItem?.ToString() ?? "Tất cả";
            string namHoc = cboNamHocGV.SelectedItem?.ToString() ?? "Tất cả";

            lsvThongKeDeThi.Items.Clear();
            List<ThongKeDeThiDTO> dstk = adminBUS.LayThongKeGiaoVien(maGV.Trim(), hocKy, namHoc) ?? new List<ThongKeDeThiDTO>();

            foreach (var tk in dstk)
            {
                ListViewItem item = new ListViewItem(tk.MaDe);
                item.SubItems.Add(tk.TenMon);
                item.SubItems.Add(tk.SoCauHoi.ToString());
                item.SubItems.Add(tk.ThoiGian.ToString() + " phút");
                lsvThongKeDeThi.Items.Add(item);
            }

            int tongCauDongGop = DemSoCauHoiDongGopCuaGV(maGV, hocKy, namHoc);

            txtTongSoDe.Text = $"{dstk.Count} đề";
            txtTongSoCauHoi.Text = $"{tongCauDongGop} câu";
        }


        private string TinhDiemChu(double diemH10)
        {
            if (diemH10 >= 8.5) return "A";
            if (diemH10 >= 7.0) return "B";
            if (diemH10 >= 5.5) return "C";
            if (diemH10 >= 4.0) return "D";
            return "F";
        }

        private void DoDuLieuGopLenListView(List<GiangVienDTO> dsGV, List<SinhVienDTO> dsSV)
        {
            lsvDanhSach.Items.Clear();

            if (dsGV != null)
            {
                foreach (GiangVienDTO gv in dsGV)
                {
                    ListViewItem item = new ListViewItem(gv.MaGV);
                    item.SubItems.Add(gv.HoLot);
                    item.SubItems.Add(gv.Ten);
                    item.SubItems.Add(gv.GioiTinh);
                    item.SubItems.Add(HienThiTenKhoaDayDu(gv.MaKhoa));
                    item.SubItems.Add(gv.MaMon);
                    item.SubItems.Add(gv.NgaySinh.ToString("dd/MM/yyyy"));
                    item.SubItems.Add("");
                    item.SubItems.Add("Giáo viên");

                    lsvDanhSach.Items.Add(item);
                }
            }

            if (dsSV != null)
            {
                foreach (SinhVienDTO sv in dsSV)
                {
                    ListViewItem item = new ListViewItem(sv.MaSV);
                    item.SubItems.Add(sv.HoLot);
                    item.SubItems.Add(sv.Ten);
                    item.SubItems.Add(sv.GioiTinh);
                    item.SubItems.Add(HienThiTenKhoaDayDu(sv.MaKhoa));
                    item.SubItems.Add("");
                    item.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(sv.Lop);
                    item.SubItems.Add("Sinh viên");

                    lsvDanhSach.Items.Add(item);
                }
            }
        }
    }
}