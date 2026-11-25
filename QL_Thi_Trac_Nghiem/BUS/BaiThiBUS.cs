using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    public class BaiThiBUS
    {
        private readonly BaiThiDAO baiThiDAO = new BaiThiDAO();

        public List<ThongKeDeThiDTO> LayDanhSachDeThiChoSV(string kyThi, string namHoc, string monHoc)
            => baiThiDAO.LayDanhSachDeThiChoSV(kyThi, namHoc, monHoc);

        public List<CauHoiThiDTO> LayDanhSachCauHoiTheoDe(string maDe)
            => baiThiDAO.LayDanhSachCauHoiTheoDe(maDe);

        public string BatDauBaiThi(string maSV, string maDe, DateTime tgBatDau)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new ArgumentException("Mã sinh viên không được để trống.", nameof(maSV));
            if (string.IsNullOrWhiteSpace(maDe))
                throw new ArgumentException("Mã đề thi không được để trống.", nameof(maDe));

            return baiThiDAO.BatDauBaiThi(maSV.Trim(), maDe.Trim(), tgBatDau);
        }

        /// <summary>
        /// EXAM-BE: Khởi tạo bài thi mới hoặc khôi phục bài đang làm dở (kèm đáp án đã lưu tạm và thời gian còn lại).
        /// </summary>
        public BaiThiDTO BatDauHoacTiepTucBaiThi(
            string maSV,
            string maDe,
            string tenMon,
            int thoiGianPhut,
            List<CauHoiThiDTO> dsCauHoi,
            out int thoiGianConLaiGiay,
            out bool laKhoiPhuc)
        {
            int tongGiayDeThi = (thoiGianPhut > 0 ? thoiGianPhut : 45) * 60;
            BaiThiDTO baiDangLam = baiThiDAO.LayBaiThiDangLam(maSV, maDe);

            if (baiDangLam != null)
            {
                int daTroiQua = (int)(DateTime.Now - baiDangLam.ThoiGianBatDau).TotalSeconds;
                int conLai = tongGiayDeThi - Math.Max(0, daTroiQua);

                var dapAnTam = baiThiDAO.LayDapAnDaLuuTam(baiDangLam.MaBT);
                foreach (var ch in dsCauHoi)
                {
                    if (dapAnTam.TryGetValue(ch.MaCH, out string daChon))
                        ch.DapAnDaChon = daChon;
                }

                baiDangLam.TenMon = tenMon;
                thoiGianConLaiGiay = Math.Max(0, conLai);
                laKhoiPhuc = true;
                return baiDangLam;
            }

            DateTime now = DateTime.Now;
            string maBTMoi = BatDauBaiThi(maSV, maDe, now);

            thoiGianConLaiGiay = tongGiayDeThi;
            laKhoiPhuc = false;

            return new BaiThiDTO
            {
                MaBT = maBTMoi,
                MaSV = maSV,
                MaDe = maDe,
                TenMon = tenMon,
                ThoiGianBatDau = now,
                TrangThai = "Đang làm"
            };
        }


        public void LuuTamDapAn(string maBT, string maCH, string dapAnChon)
        {
            if (string.IsNullOrWhiteSpace(maBT) || string.IsNullOrWhiteSpace(maCH))
                return;

            string chuanHoa = (dapAnChon ?? "").Trim().ToUpper();
            if (chuanHoa != "" && chuanHoa != "A" && chuanHoa != "B" && chuanHoa != "C" && chuanHoa != "D")
                return;

            baiThiDAO.LuuTamDapAn(maBT.Trim(), maCH.Trim(), chuanHoa);
        }

        public void LuuTamToanBoBaiLam(string maBT, List<CauHoiThiDTO> dsCauHoi)
        {
            if (string.IsNullOrWhiteSpace(maBT) || dsCauHoi == null) return;
            baiThiDAO.LuuTamToanBoBaiLam(maBT.Trim(), dsCauHoi);
        }

        // GRADE-BE: Tự động so khớp đáp án, đếm số câu đúng/sai, tính thang điểm 10 và lưu CSDL
        public BaiThiDTO ChamDiemVaNopBai(string maBT, string maSV, string maDe, string tenMon, DateTime tgBatDau, List<CauHoiThiDTO> dsCauHoi)
        {
            var danhSachAnToan = dsCauHoi ?? new List<CauHoiThiDTO>();
            int tongSoCau = danhSachAnToan.Count;
            int soCauDung = 0;

            foreach (var ch in danhSachAnToan)
            {
                string dapAnChon = (ch.DapAnDaChon ?? "").Trim().ToUpperInvariant();
                string dapAnDung = (ch.DapAnDung ?? "").Trim().ToUpperInvariant();

                if (!string.IsNullOrEmpty(dapAnChon) && !string.IsNullOrEmpty(dapAnDung) && dapAnChon == dapAnDung)
                {
                    soCauDung++;
                }
            }

            int soCauSai = Math.Max(0, tongSoCau - soCauDung);
            double tongDiem = tongSoCau > 0 ? Math.Round((double)soCauDung / tongSoCau * 10.0, 2) : 0;

            BaiThiDTO ketQua = new BaiThiDTO
            {
                MaBT = maBT,
                MaSV = maSV,
                MaDe = maDe,
                TenMon = tenMon,
                ThoiGianBatDau = tgBatDau,
                ThoiGianKetThuc = DateTime.Now,
                SoCauDung = soCauDung,
                SoCauSai = soCauSai,
                TongDiem = tongDiem,
                TrangThai = "Đã nộp"
            };

            baiThiDAO.LuuKetQuaChamDiem(ketQua, danhSachAnToan);
            return ketQua;
        }

        public List<BaiThiDTO> LayLichSuBaiThiSV(string maSV)
            => baiThiDAO.LayLichSuBaiThiSV(maSV);

        public List<ChiTietBaiLamDTO> LayChiTietBaiLam(string maBT)
            => baiThiDAO.LayChiTietBaiLam(maBT);

        public List<BaoCaoDiemThiDTO> ThongKeDiemThi(string kyThi, string monHoc, string maLop)
            => baiThiDAO.ThongKeDiemThi(kyThi, monHoc, maLop);

        // Xuất danh sách điểm ra file Excel (.csv chuẩn UTF-8 BOM mở trực tiếp bằng Microsoft Excel không lỗi font)
        public void XuatFileExcel(List<BaoCaoDiemThiDTO> dsBaoCao)
        {
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
        public class KetQuaChamDiemDTO
        {
            public int TongSoCau { get; set; }
            public int SoCauDung { get; set; }
            public int SoCauSai { get; set; }
            public double DiemThang10 { get; set; }
        }

        public KetQuaChamDiemDTO ChamDiemTuDong(List<CauHoiDTO> danhSachCauHoi, Dictionary<string, string>? dapAnSinhVien)
        {
            int tongCau = danhSachCauHoi?.Count ?? 0;
            int soDung = 0;

            if (tongCau > 0 && danhSachCauHoi != null)
            {
                foreach (var ch in danhSachCauHoi)
                {
                    string dapAnDung = (ch.DapAnDung ?? "").Trim().ToUpperInvariant();
                    string dapAnChon = "";

                    if (dapAnSinhVien != null && dapAnSinhVien.TryGetValue(ch.MaCH, out string? chon))
                    {
                        dapAnChon = (chon ?? "").Trim().ToUpperInvariant();
                    }

                    if (!string.IsNullOrEmpty(dapAnDung) && dapAnChon == dapAnDung)
                    {
                        soDung++;
                    }
                }
            }

            int soSai = Math.Max(0, tongCau - soDung);
            double diem = tongCau > 0 ? Math.Round((double)soDung / tongCau * 10.0, 2) : 0.0;

            return new KetQuaChamDiemDTO
            {
                TongSoCau = tongCau,
                SoCauDung = soDung,
                SoCauSai = soSai,
                DiemThang10 = diem
            };
        }
    }
}