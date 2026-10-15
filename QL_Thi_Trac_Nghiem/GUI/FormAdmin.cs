using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.BUS;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormAdmin : Form
    {
        AdminBUS adminBUS = new AdminBUS();
        string maDangChon = "";

        public FormAdmin()
        {
            InitializeComponent();

            // Tự động gắn sự kiện khi bấm ra vùng trống trên nền Form thì làm sạch Control và tải lại danh sách
            this.MouseDown += FormAdmin_VungTrong_MouseDown;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel || ctrl is GroupBox || ctrl is Label)
                {
                    ctrl.MouseDown += FormAdmin_VungTrong_MouseDown;
                }
            }
        }

        // 1. Khởi tạo dữ liệu khi mở Form
        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // Tự động đổi nhãn "Thí sinh" thành "Sinh viên" trên giao diện
            rdoSinhVien.Text = "Sinh viên";

            // Bấm 1 lần vào tên môn là tự động tích dấu chọn luôn
            clbMon.CheckOnClick = true;

            cmbKhoa.Items.Clear();
            cmbKhoa.Items.AddRange(new string[] { "CNTT", "Kinh Tế", "Ngoại Ngữ", "Cơ Bản" });

            cmbLop.Items.Clear();
            cmbLop.Items.AddRange(new string[] { "DCT1231", "DCT1232", "DKT1231" });

            clbMon.Items.Clear();
            clbMon.Items.AddRange(new string[] { "Lập trình C#", "Cơ sở dữ liệu", "Toán cao cấp", "Tiếng Anh" });

            rdoGiaoVien.Checked = true;
        }

        // Khi bấm chuột ra vùng trống bên ngoài -> Làm sạch bộ lọc và hiện lại toàn bộ danh sách
        private void FormAdmin_VungTrong_MouseDown(object sender, MouseEventArgs e)
        {
            LamSachControl();
            if (rdoGiaoVien.Checked)
                HienThiDanhSachGiaoVien();
            else if (rdoSinhVien.Checked)
                HienThiDanhSachSinhVien();
        }

        // Hàm làm sạch toàn bộ các ô nhập liệu bên trái
        private void LamSachControl()
        {
            maDangChon = "";
            txtMa.Clear();
            txtHo.Clear();
            txtTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            cmbKhoa.SelectedIndex = -1;
            cmbKhoa.Text = "";
            cmbLop.SelectedIndex = -1;
            cmbLop.Text = "";
            BoChonTatCaMon();

            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                lsvDanhSach.SelectedItems.Clear();
            }
        }

        // 2. Chuyển đổi vai trò Giáo viên / Sinh viên
        private void rdoGiaoVien_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoGiaoVien.Checked)
            {
                clbMon.Enabled = true;
                cmbLop.Enabled = false;
                LamSachControl();
                HienThiDanhSachGiaoVien();
            }
        }

        private void rdoSinhVien_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSinhVien.Checked)
            {
                cmbLop.Enabled = true;
                clbMon.Enabled = false;
                LamSachControl();
                HienThiDanhSachSinhVien();
            }
        }

        // 3. Đổ dữ liệu lên ListView
        private void DoDuLieuLenListView(DataTable dt, bool laGiaoVien)
        {
            lsvDanhSach.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string ma = laGiaoVien ? row["Ma_GV"].ToString() : row["Ma_SV"].ToString();
                string mon = laGiaoVien ? row["Ma_Mon"].ToString() : "";
                string lop = laGiaoVien ? "" : row["Lop"].ToString();
                DateTime ngaySinh = Convert.ToDateTime(row["Ngay_sinh"]);

                // Cột 0: Mã
                ListViewItem item = new ListViewItem(ma);
                // Cột 1: Họ lót
                item.SubItems.Add(row["Ho_lot"].ToString());
                // Cột 2: Tên
                item.SubItems.Add(row["Ten"].ToString());
                // Cột 3: Giới tính
                item.SubItems.Add(row["Gioi_tinh"].ToString());
                // Cột 4: Khoa
                item.SubItems.Add(row["Ma_Khoa"].ToString());
                // Cột 5: Môn
                item.SubItems.Add(mon);
                // Cột 6: Ngày sinh
                item.SubItems.Add(ngaySinh.ToString("dd/MM/yyyy"));
                // Cột 7: Lớp
                item.SubItems.Add(lop);

                lsvDanhSach.Items.Add(item);
            }
        }

        private void HienThiDanhSachGiaoVien()
        {
            DataTable dt = adminBUS.LayDanhSachGiaoVien();
            DoDuLieuLenListView(dt, true);
        }

        private void HienThiDanhSachSinhVien()
        {
            DataTable dt = adminBUS.LayDanhSachSinhVien();
            DoDuLieuLenListView(dt, false);
        }

        // 4. Khi bấm chọn 1 dòng trên ListView
        private void lsvDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvDanhSach.SelectedItems.Count == 0)
            {
                LamSachControl();
                return;
            }

            ListViewItem item = lsvDanhSach.SelectedItems[0];

            maDangChon = item.SubItems[0].Text;
            txtMa.Text = item.SubItems[0].Text;
            txtHo.Text = item.SubItems[1].Text;
            txtTen.Text = item.SubItems[2].Text;

            // Cột 3: Giới tính
            string gioiTinh = item.SubItems[3].Text;
            rdoNam.Checked = (gioiTinh == "Nam");
            rdoNu.Checked = (gioiTinh == "Nữ");

            // Cột 4: Khoa
            cmbKhoa.Text = item.SubItems[4].Text;

            // Cột 5: Môn
            BoChonTatCaMon();
            string chuoiMon = item.SubItems[5].Text;
            if (!string.IsNullOrEmpty(chuoiMon))
            {
                for (int i = 0; i < clbMon.Items.Count; i++)
                {
                    string tenMon = clbMon.Items[i].ToString();
                    if (chuoiMon.Contains(tenMon))
                    {
                        clbMon.SetItemChecked(i, true);
                    }
                }
            }

            // Cột 6: Ngày sinh
            if (DateTime.TryParseExact(item.SubItems[6].Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ns))
            {
                dtpNgaySinh.Value = ns;
            }

            // Cột 7: Lớp
            cmbLop.Text = item.SubItems[7].Text;
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
                dsMon.Add(item.ToString());
            }
            // Hỗ trợ trường hợp người dùng chỉ bôi xanh dòng môn học mà chưa kịp tích dấu check
            if (dsMon.Count == 0 && clbMon.SelectedItem != null)
            {
                dsMon.Add(clbMon.SelectedItem.ToString());
            }
            return dsMon;
        }

        private string LayChuoiMonDaChon()
        {
            return string.Join(", ", LayDanhSachMonDaChon());
        }

        // ================= NÚT THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            string ma = txtMa.Text.Trim();
            string ho = txtHo.Text.Trim();
            string ten = txtTen.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = rdoNam.Checked ? "Nam" : (rdoNu.Checked ? "Nữ" : "");
            string khoa = cmbKhoa.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ho) || string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã, Họ, Tên và chọn Giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rdoGiaoVien.Checked)
            {
                if (adminBUS.KiemTraTrungMaGV(ma))
                {
                    MessageBox.Show("Mã giáo viên này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string mon = LayChuoiMonDaChon();
                if (adminBUS.ThemGiaoVien(ma, ho, ten, ngaySinh, gioiTinh, khoa, mon))
                {
                    MessageBox.Show("Thêm giáo viên thành công! (Mật khẩu mặc định: 123456)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachGiaoVien();
                    LamSachControl();
                }
            }
            else if (rdoSinhVien.Checked)
            {
                if (adminBUS.KiemTraTrungMaSV(ma))
                {
                    MessageBox.Show("Mã sinh viên này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string lop = cmbLop.Text.Trim();
                if (adminBUS.ThemSinhVien(ma, ho, ten, ngaySinh, gioiTinh, khoa, lop))
                {
                    MessageBox.Show("Thêm sinh viên thành công! (Mật khẩu mặc định: 123456)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachSinhVien();
                    LamSachControl();
                }
            }
        }

        // ================= NÚT CẬP NHẬT =================
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maDangChon))
            {
                MessageBox.Show("Vui lòng chọn một người từ danh sách bên phải trước khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMoi = txtMa.Text.Trim();
            string ho = txtHo.Text.Trim();
            string ten = txtTen.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = rdoNam.Checked ? "Nam" : (rdoNu.Checked ? "Nữ" : "");
            string khoa = cmbKhoa.Text.Trim();

            if (rdoGiaoVien.Checked)
            {
                if (maMoi != maDangChon && adminBUS.KiemTraTrungMaGV(maMoi))
                {
                    MessageBox.Show("Mã giáo viên mới này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string mon = LayChuoiMonDaChon();
                if (adminBUS.CapNhatGiaoVien(maDangChon, maMoi, ho, ten, ngaySinh, gioiTinh, khoa, mon))
                {
                    MessageBox.Show("Cập nhật thông tin giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachGiaoVien();
                    LamSachControl();
                }
            }
            else if (rdoSinhVien.Checked)
            {
                if (maMoi != maDangChon && adminBUS.KiemTraTrungMaSV(maMoi))
                {
                    MessageBox.Show("Mã sinh viên mới này đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string lop = cmbLop.Text.Trim();
                if (adminBUS.CapNhatSinhVien(maDangChon, maMoi, ho, ten, ngaySinh, gioiTinh, khoa, lop))
                {
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachSinhVien();
                    LamSachControl();
                }
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

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa mã '{ma}' và tài khoản đăng nhập của người này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            if (rdoGiaoVien.Checked)
            {
                if (adminBUS.XoaGiaoVien(ma))
                {
                    MessageBox.Show("Đã xóa giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachGiaoVien();
                    LamSachControl();
                }
            }
            else if (rdoSinhVien.Checked)
            {
                if (adminBUS.XoaSinhVien(ma))
                {
                    MessageBox.Show("Đã xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiDanhSachSinhVien();
                    LamSachControl();
                }
            }
        }

        // ================= NÚT TÌM (LỌC THEO TẤT CẢ CONTROL) =================
        private void btnTim_Click(object sender, EventArgs e)
        {
            // Bỏ chọn dòng hiện tại trên ListView để tránh xung đột khi đang lọc
            if (lsvDanhSach.SelectedItems.Count > 0)
            {
                lsvDanhSach.SelectedItems.Clear();
            }

            string ma = txtMa.Text.Trim();
            string ho = txtHo.Text.Trim();
            string ten = txtTen.Text.Trim();
            string gioiTinh = rdoNam.Checked ? "Nam" : (rdoNu.Checked ? "Nữ" : "");
            string khoa = cmbKhoa.Text.Trim();
            string lop = cmbLop.Text.Trim();
            List<string> dsMon = LayDanhSachMonDaChon();

            if (rdoGiaoVien.Checked)
            {
                DataTable dt = adminBUS.TimGiaoVien(ma, ho, ten, gioiTinh, khoa, dsMon);
                DoDuLieuLenListView(dt, true);
            }
            else if (rdoSinhVien.Checked)
            {
                DataTable dt = adminBUS.TimSinhVien(ma, ho, ten, gioiTinh, khoa, lop);
                DoDuLieuLenListView(dt, false);
            }
        }
    }
}