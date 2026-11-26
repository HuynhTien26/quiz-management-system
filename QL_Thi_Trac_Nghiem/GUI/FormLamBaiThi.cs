using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.BUS;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.GUI
{
    public partial class FormLamBaiThi : Form
    {
        private readonly BaiThiBUS baiThiBUS = new BaiThiBUS();
        private List<CauHoiThiDTO> dsCauHoi = new List<CauHoiThiDTO>();
        private List<Button> dsNutCauHoi = new List<Button>();

        private string maSV = "";
        private string maDe = "";
        private string tenMon = "";
        private string maBT = "";
        private int thoiGianConLaiGiay = 0;
        private int viTriCauHienTai = 0;
        private DateTime thoiGianBatDau;
        private bool dangCapNhatGiaoDien = false;
        private bool daNopBai = false;

        public BaiThiDTO KetQuaSauKhiNop { get; private set; } = null;

        public FormLamBaiThi()
        {
            InitializeComponent();
        }

        public FormLamBaiThi(string maSV, string maDe, string tenMon, int thoiGianPhut) : this()
        {
            this.maSV = maSV;
            this.maDe = maDe;
            this.tenMon = tenMon;
            this.thoiGianConLaiGiay = (thoiGianPhut > 0 ? thoiGianPhut : 45) * 60;
            this.KeyPreview = true;

            GanSuKienControl();
            this.Load += FormLamBaiThi_Load;
            this.KeyDown += FormLamBaiThi_KeyDown;
            this.FormClosing += FormLamBaiThi_FormClosing;
        }

        private void GanSuKienControl()
        {
            timerDemNguoc.Tick += TimerDemNguoc_Tick;
            btnCauTruoc.Click += BtnCauTruoc_Click;
            btnCauTiep.Click += BtnCauTiep_Click;
            btnNopBai.Click += BtnNopBai_Click;

            rdoChonA.CheckedChanged += RdoChonDapAn_CheckedChanged;
            rdoChonB.CheckedChanged += RdoChonDapAn_CheckedChanged;
            rdoChonC.CheckedChanged += RdoChonDapAn_CheckedChanged;
            rdoChonD.CheckedChanged += RdoChonDapAn_CheckedChanged;
        }

        // ==================== SỬA TỪ ĐÂY (KHÔI PHỤC BÀI THI) ====================
        private void FormLamBaiThi_Load(object sender, EventArgs e)
        {
            txtTenMonThi.Text = tenMon;
            txtMaDeThi.Text = maDe;

            dsCauHoi = baiThiBUS.LayDanhSachCauHoiTheoDe(maDe);
            if (dsCauHoi == null || dsCauHoi.Count == 0)
            {
                MessageBox.Show(
                    "Đề thi này hiện chưa có câu hỏi nào trong Cơ sở dữ liệu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                daNopBai = true;
                this.Close();
                return;
            }

            int thoiGianPhut = Math.Max(1, thoiGianConLaiGiay / 60);
            BaiThiDTO baiThiHienTai = baiThiBUS.BatDauHoacTiepTucBaiThi(
                maSV, maDe, tenMon, thoiGianPhut, dsCauHoi,
                out thoiGianConLaiGiay, out bool laKhoiPhuc);

            maBT = baiThiHienTai.MaBT;
            thoiGianBatDau = baiThiHienTai.ThoiGianBatDau;

            KhoiTaoThanhChuyenCau();
            HienThiCauHoi(0);
            CapNhatTienDoLamBai();
            CapNhatNhanDongHo();

            if (laKhoiPhuc && thoiGianConLaiGiay <= 0)
            {
                MessageBox.Show(
                    "Bài thi trước đó của bạn đã hết thời gian làm bài. Hệ thống sẽ tự động thu bài và chấm điểm ngay.",
                    "Hết giờ làm bài",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ThucHienNopBai();
                return;
            }

            if (laKhoiPhuc)
            {
                MessageBox.Show(
                    "Hệ thống đã khôi phục lại bài làm và các đáp án đã lưu tạm trước đó của bạn.",
                    "Khôi phục bài thi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            timerDemNguoc.Start();
        }
        // ==================== HẾT PHẦN SỬA ====================

        private void KhoiTaoThanhChuyenCau()
        {
            flpDanhSachCau.Controls.Clear();
            dsNutCauHoi.Clear();

            for (int i = 0; i < dsCauHoi.Count; i++)
            {
                int index = i;
                Button btn = new Button
                {
                    Text = (i + 1).ToString(),
                    Width = 44,
                    Height = 36,
                    Margin = new Padding(4),
                    BackColor = Color.WhiteSmoke,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderColor = Color.Silver;
                btn.Click += (s, e) => HienThiCauHoi(index);
                flpDanhSachCau.Controls.Add(btn);
                dsNutCauHoi.Add(btn);
            }
        }

        private void HienThiCauHoi(int index)
        {
            if (index < 0 || index >= dsCauHoi.Count) return;

            dangCapNhatGiaoDien = true;
            viTriCauHienTai = index;
            CauHoiThiDTO ch = dsCauHoi[index];

            lblCauSo.Text = $"{index + 1} / {dsCauHoi.Count}";
            txtNoiDungCauHoi.Text = ch.NoiDung;

            rdoChonA.Text = ch.DapAnA;
            rdoChonB.Text = ch.DapAnB;
            rdoChonC.Text = ch.DapAnC;
            rdoChonD.Text = ch.DapAnD;

            rdoChonA.Checked = ch.DapAnDaChon == "A";
            rdoChonB.Checked = ch.DapAnDaChon == "B";
            rdoChonC.Checked = ch.DapAnDaChon == "C";
            rdoChonD.Checked = ch.DapAnDaChon == "D";

            btnCauTruoc.Enabled = !daNopBai && index > 0;
            btnCauTiep.Enabled = !daNopBai && index < dsCauHoi.Count - 1;

            CapNhatMauNutChuyenCau();
            dangCapNhatGiaoDien = false;
        }

        private void RdoChonDapAn_CheckedChanged(object sender, EventArgs e)
        {
            if (dangCapNhatGiaoDien || daNopBai) return;
            if (sender is RadioButton rdo && !rdo.Checked) return;

            string dapAn = "";
            if (rdoChonA.Checked) dapAn = "A";
            else if (rdoChonB.Checked) dapAn = "B";
            else if (rdoChonC.Checked) dapAn = "C";
            else if (rdoChonD.Checked) dapAn = "D";

            CauHoiThiDTO chHienTai = dsCauHoi[viTriCauHienTai];
            chHienTai.DapAnDaChon = dapAn;

            try
            {
                baiThiBUS.LuuTamDapAn(maBT, chHienTai.MaCH, dapAn);
            }
            catch
            {
                // Giữ trạng thái trên bộ nhớ tạm nếu kết nối tạm thời ngắt quãng
            }

            CapNhatMauNutChuyenCau();
            CapNhatTienDoLamBai();
        }

        private void CapNhatMauNutChuyenCau()
        {
            for (int i = 0; i < dsNutCauHoi.Count; i++)
            {
                if (i == viTriCauHienTai)
                {
                    dsNutCauHoi[i].BackColor = Color.DodgerBlue;
                    dsNutCauHoi[i].ForeColor = Color.White;
                }
                else if (!string.IsNullOrEmpty(dsCauHoi[i].DapAnDaChon))
                {
                    dsNutCauHoi[i].BackColor = Color.LightGreen;
                    dsNutCauHoi[i].ForeColor = Color.Black;
                }
                else
                {
                    dsNutCauHoi[i].BackColor = Color.WhiteSmoke;
                    dsNutCauHoi[i].ForeColor = Color.Black;
                }
            }
        }

        private void CapNhatTienDoLamBai()
        {
            int soCauDaLam = dsCauHoi.FindAll(c => !string.IsNullOrEmpty(c.DapAnDaChon)).Count;
            lblTienDoLamBai.Text = $"Đã trả lời: {soCauDaLam} / {dsCauHoi.Count} câu";
        }

        private void FormLamBaiThi_KeyDown(object sender, KeyEventArgs e)
        {
            if (daNopBai || dsCauHoi.Count == 0) return;

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
            {
                rdoChonA.Checked = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.B || e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                rdoChonB.Checked = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.C || e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                rdoChonC.Checked = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                rdoChonD.Checked = true;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left && viTriCauHienTai > 0)
            {
                HienThiCauHoi(viTriCauHienTai - 1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right && viTriCauHienTai < dsCauHoi.Count - 1)
            {
                HienThiCauHoi(viTriCauHienTai + 1);
                e.Handled = true;
            }
        }

        private void BtnCauTruoc_Click(object sender, EventArgs e)
        {
            if (viTriCauHienTai > 0) HienThiCauHoi(viTriCauHienTai - 1);
        }

        private void BtnCauTiep_Click(object sender, EventArgs e)
        {
            if (viTriCauHienTai < dsCauHoi.Count - 1) HienThiCauHoi(viTriCauHienTai + 1);
        }

        private void TimerDemNguoc_Tick(object sender, EventArgs e)
        {
            if (thoiGianConLaiGiay > 0)
            {
                thoiGianConLaiGiay--;
                CapNhatNhanDongHo();
            }
            else
            {
                timerDemNguoc.Stop();
                KhoaThaoTacKhiNop();
                MessageBox.Show(
                    "Đã hết thời gian làm bài! Hệ thống sẽ tự động thu bài và chấm điểm.",
                    "Hết giờ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ThucHienNopBai();
            }
        }

        private void CapNhatNhanDongHo()
        {
            int phut = thoiGianConLaiGiay / 60;
            int giay = thoiGianConLaiGiay % 60;
            lblThoiGianConLai.Text = $"{phut:D2}:{giay:D2}";
            lblThoiGianConLai.ForeColor = thoiGianConLaiGiay <= 60 ? Color.Red : Color.DarkBlue;
        }

        private void KhoaThaoTacKhiNop()
        {
            groupBox1.Enabled = false;
            btnCauTruoc.Enabled = false;
            btnCauTiep.Enabled = false;
            btnNopBai.Enabled = false;
        }

        private void BtnNopBai_Click(object sender, EventArgs e)
        {
            if (daNopBai) return;

            int soCauChuaLam = dsCauHoi.FindAll(c => string.IsNullOrEmpty(c.DapAnDaChon)).Count;
            string thongBao = soCauChuaLam > 0
                ? $"Bạn còn {soCauChuaLam} câu hỏi chưa chọn đáp án.\nBạn có chắc chắn muốn nộp bài sớm không?"
                : "Bạn đã hoàn thành tất cả câu hỏi. Bạn có chắc chắn muốn nộp bài?";

            if (MessageBox.Show(thongBao, "Xác nhận nộp bài", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                timerDemNguoc.Stop();
                KhoaThaoTacKhiNop();
                ThucHienNopBai();
            }
        }

        private void ThucHienNopBai()
        {
            if (daNopBai) return;
            daNopBai = true;
            KhoaThaoTacKhiNop();

            KetQuaSauKhiNop = baiThiBUS.ChamDiemVaNopBai(maBT, maSV, maDe, tenMon, thoiGianBatDau, dsCauHoi);

            MessageBox.Show(
                $"Nộp bài thành công!\n\n" +
                $"- Mã bài thi: {KetQuaSauKhiNop.MaBT}\n" +
                $"- Số câu đúng: {KetQuaSauKhiNop.SoCauDung} / {dsCauHoi.Count}\n" +
                $"- Số câu sai: {KetQuaSauKhiNop.SoCauSai}\n" +
                $"- Tổng điểm: {KetQuaSauKhiNop.TongDiem:0.00} điểm",
                "Kết quả chấm điểm tự động",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormLamBaiThi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!daNopBai && dsCauHoi.Count > 0)
            {
                var confirm = MessageBox.Show(
                    "Bạn đang trong thời gian làm bài. Nếu thoát bây giờ hệ thống sẽ tự động nộp bài hiện tại. Bạn có muốn thoát?",
                    "Cảnh báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    timerDemNguoc.Stop();
                    ThucHienNopBai();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
