using System;
using System.Collections.Generic;
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
            BaiThiDTO? baiDangLam = baiThiDAO.LayBaiThiDangLam(maSV, maDe);

            if (baiDangLam != null)
            {
                int daTroiQua = (int)(DateTime.Now - baiDangLam.ThoiGianBatDau).TotalSeconds;
                int conLai = tongGiayDeThi - Math.Max(0, daTroiQua);

                var dapAnTam = baiThiDAO.LayDapAnDaLuuTam(baiDangLam.MaBT);
                foreach (var ch in dsCauHoi)
                {
                    if (dapAnTam.TryGetValue(ch.MaCH, out string? daChon))
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
    }
}