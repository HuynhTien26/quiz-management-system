using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;
using ClosedXML.Excel;

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
            int thoiGianMacDinh = LayThoiGianMacDinh();
            int tongGiayDeThi = (thoiGianPhut > 0 ? thoiGianPhut : thoiGianMacDinh) * 60;
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
            double thangDiem = LayThangDiem();
            double tongDiem = tongSoCau > 0 ? Math.Round((double)soCauDung / tongSoCau * thangDiem, 2) : 0;

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

        // ==================== MỤC 10: THỐNG KÊ THEO THỜI GIAN & 3 BẢNG QUÝ ====================

        public List<BaoCaoDiemThiDTO> ThongKeDiemThiNangCao(
            string kyThi,
            string monHoc,
            string maLop,
            DateTime? tuNgay,
            DateTime? denNgay,
            int? thang,
            int? quy,
            int? nam)
        {
            if (tuNgay.HasValue && denNgay.HasValue && tuNgay.Value.Date > denNgay.Value.Date)
                throw new ArgumentException("Ngày bắt đầu không được lớn hơn ngày kết thúc.");

            return baiThiDAO.ThongKeDiemThiNangCao(kyThi, monHoc, maLop, tuNgay, denNgay, thang, quy, nam);
        }

        public TongHopThongKeDTO TinhTongHopThongKe(List<BaoCaoDiemThiDTO>? ds)
        {
            if (ds == null || ds.Count == 0)
                return new TongHopThongKeDTO();

            int tong = ds.Count;
            double diemDat = LayDiemDat();
            int dat = ds.Count(x => x.TongDiem >= diemDat);
            int chuaDat = tong - dat;

            return new TongHopThongKeDTO
            {
                TongSoBai = tong,
                SoBaiDat = dat,
                SoBaiChuaDat = chuaDat,
                TyLeDat = Math.Round((double)dat / tong * 100.0, 1),
                DiemTrungBinh = Math.Round(ds.Average(x => x.TongDiem), 2),
                DiemCaoNhat = Math.Round(ds.Max(x => x.TongDiem), 2)
            };
        }

        public List<ThongKeTheoQuyDTO> ThongKeTheoMonVaQuy(int nam, bool tinhDiemTrungBinh)
            => ThemDongTongCong(baiThiDAO.ThongKeTheoMonVaQuy(nam, tinhDiemTrungBinh), tinhDiemTrungBinh);

        public List<ThongKeTheoQuyDTO> ThongKeTheoLopVaQuy(int nam, bool tinhDiemTrungBinh)
            => ThemDongTongCong(baiThiDAO.ThongKeTheoLopVaQuy(nam, tinhDiemTrungBinh), tinhDiemTrungBinh);

        public List<ThongKeTheoQuyDTO> ThongKeTheoGiangVienVaQuy(int nam, bool tinhDiemTrungBinh)
            => ThemDongTongCong(baiThiDAO.ThongKeTheoGiangVienVaQuy(nam, tinhDiemTrungBinh), tinhDiemTrungBinh);

        private static List<ThongKeTheoQuyDTO> ThemDongTongCong(List<ThongKeTheoQuyDTO> ds, bool tinhDiemTrungBinh)
        {
            if (ds.Count == 0) return ds;

            if (!tinhDiemTrungBinh)
            {
                ds.Add(new ThongKeTheoQuyDTO
                {
                    TenDoiTuong = "TC (Tổng cộng)",
                    Q1 = ds.Sum(x => x.Q1),
                    Q2 = ds.Sum(x => x.Q2),
                    Q3 = ds.Sum(x => x.Q3),
                    Q4 = ds.Sum(x => x.Q4),
                    TC = ds.Sum(x => x.TC)
                });
            }
            else
            {
                static double TinhTBCoDuLieu(IEnumerable<double> values)
                {
                    var valid = values.Where(v => v > 0).ToList();
                    return valid.Count == 0 ? 0 : Math.Round(valid.Average(), 2);
                }

                ds.Add(new ThongKeTheoQuyDTO
                {
                    TenDoiTuong = "TC (Trung bình)",
                    Q1 = TinhTBCoDuLieu(ds.Select(x => x.Q1)),
                    Q2 = TinhTBCoDuLieu(ds.Select(x => x.Q2)),
                    Q3 = TinhTBCoDuLieu(ds.Select(x => x.Q3)),
                    Q4 = TinhTBCoDuLieu(ds.Select(x => x.Q4)),
                    TC = TinhTBCoDuLieu(ds.Select(x => x.TC))
                });
            }

            return ds;
        }

      // ==================== MỤC 8: TÌM KIẾM NÂNG CAO (AND, OR, NOT, >, >=, <, <=, <>) ====================

        public List<BaoCaoDiemThiDTO> LocDiemThiNangCao(
            List<BaoCaoDiemThiDTO> dsGoc,
            bool not1, string field1, string op1, string val1,
            string logic,
            bool not2, string field2, string op2, string val2)
        {
            if (dsGoc == null || dsGoc.Count == 0)
                return new List<BaoCaoDiemThiDTO>();

            if (string.IsNullOrWhiteSpace(val1))
                throw new ArgumentException("Vui lòng nhập giá trị cho điều kiện 1.");

            bool dungHaiDieuKien = logic == "AND" || logic == "OR";
            if (dungHaiDieuKien && string.IsNullOrWhiteSpace(val2))
                throw new ArgumentException("Vui lòng nhập giá trị cho điều kiện 2 khi chọn AND / OR.");

            var ketQua = new List<BaoCaoDiemThiDTO>();
            foreach (var item in dsGoc)
            {
                bool kq1 = KiemTraDieuKien(item, field1, op1, val1.Trim());
                if (not1) kq1 = !kq1;

                bool thoaMan;
                if (!dungHaiDieuKien)
                {
                    thoaMan = kq1;
                }
                else
                {
                    bool kq2 = KiemTraDieuKien(item, field2, op2, val2.Trim());
                    if (not2) kq2 = !kq2;

                    thoaMan = logic == "AND" ? (kq1 && kq2) : (kq1 || kq2);
                }

                if (thoaMan)
                    ketQua.Add(item);
            }

            return ketQua;
        }

        private static bool KiemTraDieuKien(BaoCaoDiemThiDTO item, string field, string op, string giaTriNhap)
        {
            // 1. Nhóm cột kiểu SỐ (Tổng điểm, Số câu đúng, Số câu sai)
            if (field == "Tổng điểm" || field == "Số câu đúng" || field == "Số câu sai")
            {
                string chuanSo = giaTriNhap.Replace(',', '.');
                if (!double.TryParse(chuanSo, NumberStyles.Any, CultureInfo.InvariantCulture, out double target))
                    throw new ArgumentException($"Cột '{field}' yêu cầu nhập giá trị số (VD: 5, 7.5, 8).");

                double actual = field switch
                {
                    "Tổng điểm" => item.TongDiem,
                    "Số câu đúng" => item.SoCauDung,
                    _ => item.SoCauSai
                };

                return op switch
                {
                    "=" => Math.Abs(actual - target) < 0.0001,
                    "<>" => Math.Abs(actual - target) >= 0.0001,
                    ">" => actual > target,
                    ">=" => actual >= target,
                    "<" => actual < target,
                    "<=" => actual <= target,
                    "Chứa" => actual.ToString(CultureInfo.InvariantCulture).Contains(chuanSo, StringComparison.OrdinalIgnoreCase),
                    _ => false
                };
            }

            // 2. Nhóm cột kiểu NGÀY (Ngày thi)
            if (field == "Ngày thi")
            {
                string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "dd-MM-yyyy" };
                if (!DateTime.TryParseExact(giaTriNhap, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime targetDate) &&
                    !DateTime.TryParse(giaTriNhap, out targetDate))
                {
                    throw new ArgumentException("Cột 'Ngày thi' yêu cầu nhập đúng định dạng ngày (VD: 26/11/2026).");
                }

                DateTime actualDate = item.NgayThi.Date;
                DateTime cmpDate = targetDate.Date;

                return op switch
                {
                    "=" => actualDate == cmpDate,
                    "<>" => actualDate != cmpDate,
                    ">" => actualDate > cmpDate,
                    ">=" => actualDate >= cmpDate,
                    "<" => actualDate < cmpDate,
                    "<=" => actualDate <= cmpDate,
                    "Chứa" => actualDate.ToString("dd/MM/yyyy").Contains(giaTriNhap, StringComparison.OrdinalIgnoreCase),
                    _ => false
                };
            }

            // 3. Nhóm cột kiểu CHUỖI (Mã SV, Họ tên, Lớp, Mã đề, Môn học, Kỳ thi)
            string actualStr = field switch
            {
                "Mã SV" => item.MaSV ?? "",
                "Họ và tên" => item.HoTenSV ?? "",
                "Lớp" => item.Lop ?? "",
                "Mã đề" => item.MaDe ?? "",
                "Môn học" => item.TenMon ?? "",
                "Kỳ thi" => item.KyThi ?? "",
                _ => ""
            };

            int cmp = string.Compare(actualStr, giaTriNhap, StringComparison.OrdinalIgnoreCase);
            return op switch
            {
                "=" => cmp == 0,
                "<>" => cmp != 0,
                ">" => cmp > 0,
                ">=" => cmp >= 0,
                "<" => cmp < 0,
                "<=" => cmp <= 0,
                "Chứa" => actualStr.Contains(giaTriNhap, StringComparison.OrdinalIgnoreCase),
                _ => false
            };
        }

        // ==================== MỤC 9: ĐỌC & CẬP NHẬT BẢNG THAM SỐ ====================

        public List<ThamSoDTO> LayDanhSachThamSo()
            => baiThiDAO.LayTatCaThamSo();

        public double LayThangDiem()
            => DocThamSoSoThuc("THANG_DIEM", 10.0, lonHonKhong: true);

        public double LayDiemDat()
            => DocThamSoSoThuc("DIEM_DAT", 5.0, lonHonKhong: false);

        public int LayThoiGianMacDinh()
        {
            double val = DocThamSoSoThuc("THOI_GIAN_MAC_DINH", 45, lonHonKhong: true);
            return (int)Math.Round(val);
        }

        private double DocThamSoSoThuc(string maSo, double macDinh, bool lonHonKhong)
        {
            try
            {
                string? raw = baiThiDAO.LayGiaTriThamSo(maSo);
                if (!string.IsNullOrWhiteSpace(raw))
                {
                    string chuan = raw.Trim().Replace(',', '.');
                    if (double.TryParse(chuan, NumberStyles.Any, CultureInfo.InvariantCulture, out double kq))
                    {
                        if ((lonHonKhong && kq > 0) || (!lonHonKhong && kq >= 0))
                            return kq;
                    }
                }
            }
            catch
            {
                // Nếu chưa chạy script tạo bảng Tham_So thì tự dùng giá trị mặc định để không bị crash
            }
            return macDinh;
        }

        public void CapNhatThamSo(string maSo, string giaTri)
        {
            if (string.IsNullOrWhiteSpace(maSo))
                throw new ArgumentException("Mã tham số không được để trống.");

            string ma = maSo.Trim().ToUpperInvariant();
            string val = (giaTri ?? "").Trim();

            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentException($"Giá trị của tham số '{ma}' không được để trống.");

            if (ma == "THANG_DIEM" || ma == "DIEM_DAT" || ma == "THOI_GIAN_MAC_DINH")
            {
                string chuan = val.Replace(',', '.');
                if (!double.TryParse(chuan, NumberStyles.Any, CultureInfo.InvariantCulture, out double so))
                    throw new ArgumentException($"Tham số '{ma}' phải là giá trị số hợp lệ.");

                if (ma == "THANG_DIEM" && so <= 0)
                    throw new ArgumentException("THANG_DIEM phải lớn hơn 0.");
                if (ma == "DIEM_DAT" && so < 0)
                    throw new ArgumentException("DIEM_DAT không được nhỏ hơn 0.");
                if (ma == "THOI_GIAN_MAC_DINH" && so <= 0)
                    throw new ArgumentException("THOI_GIAN_MAC_DINH phải lớn hơn 0.");

                val = chuan;
            }

            baiThiDAO.CapNhatThamSo(ma, val);
        }
   
        // ==================== XUẤT BÁO CÁO PDF ====================

        public void XuatFilePdf(List<BaoCaoDiemThiDTO> dsBaoCao)
        {
            if (dsBaoCao == null || dsBaoCao.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu điểm thi để xuất PDF!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF File (*.pdf)|*.pdf";
                sfd.FileName = $"BaoCaoDiemThi_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string pdfContent = TaoNoiDungPdfBangDiem(dsBaoCao);
                    File.WriteAllText(sfd.FileName, pdfContent, Encoding.ASCII);
                    MessageBox.Show("Xuất file PDF thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

      // ==================== MỤC 11: XUẤT EXCEL (.XLSX) CHUẨN 4 SHEET ====================

        public void XuatFileExcelXlsx(List<BaoCaoDiemThiDTO> dsBaoCao, int namThongKe, bool tinhDiemTB)
        {
            if (dsBaoCao == null || dsBaoCao.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu điểm thi để xuất Excel!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                sfd.FileName = $"BaoCaoDiemThi_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        // Sheet 1: Bảng điểm chi tiết
                        var ws = wb.Worksheets.Add("BangDiemChiTiet");
                        var tongHop = TinhTongHopThongKe(dsBaoCao);
                        double diemDat = LayDiemDat();

                        ws.Cell(1, 1).Value = "BÁO CÁO THỐNG KÊ ĐIỂM THI TRẮC NGHIỆM";
                        ws.Range("A1:N1").Merge().Style
                            .Font.SetBold(true)
                            .Font.SetFontSize(14)
                            .Font.SetFontColor(XLColor.DarkBlue)
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        ws.Cell(2, 1).Value =
                            $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm} | Tổng bài: {tongHop.TongSoBai} | Đạt (>={diemDat:0.##}): {tongHop.SoBaiDat} ({tongHop.TyLeDat:0.#}%) | Chưa đạt: {tongHop.SoBaiChuaDat} | Điểm TB: {tongHop.DiemTrungBinh:0.00}";
                        ws.Range("A2:N2").Merge().Style
                            .Font.SetItalic(true)
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        string[] headers =
                        {
                            "STT", "Mã Bài Thi", "Mã SV", "Họ và Tên", "Lớp", "Mã Đề",
                            "Môn Học", "Kỳ Thi", "Năm Học", "Số Câu Đúng", "Số Câu Sai",
                            "Tổng Điểm", "Xếp Loại", "Ngày Thi"
                        };

                        for (int c = 0; c < headers.Length; c++)
                        {
                            ws.Cell(4, c + 1).Value = headers[c];
                        }

                        var headerRange = ws.Range(4, 1, 4, headers.Length);
                        headerRange.Style.Font.SetBold(true);
                        headerRange.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#DBEAFE"));
                        headerRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        int row = 5;
                        for (int i = 0; i < dsBaoCao.Count; i++)
                        {
                            var item = dsBaoCao[i];
                            bool dat = item.TongDiem >= diemDat;

                            ws.Cell(row, 1).Value = i + 1;
                            ws.Cell(row, 2).Value = item.MaBT;
                            ws.Cell(row, 3).Value = item.MaSV;
                            ws.Cell(row, 4).Value = item.HoTenSV;
                            ws.Cell(row, 5).Value = item.Lop;
                            ws.Cell(row, 6).Value = item.MaDe;
                            ws.Cell(row, 7).Value = item.TenMon;
                            ws.Cell(row, 8).Value = item.KyThi;
                            ws.Cell(row, 9).Value = item.NamHoc;
                            ws.Cell(row, 10).Value = item.SoCauDung;
                            ws.Cell(row, 11).Value = item.SoCauSai;
                            ws.Cell(row, 12).Value = item.TongDiem;
                            ws.Cell(row, 12).Style.NumberFormat.Format = "0.00";
                            ws.Cell(row, 13).Value = dat ? "Đạt" : "Chưa đạt";
                            ws.Cell(row, 13).Style.Font.SetFontColor(dat ? XLColor.DarkGreen : XLColor.Firebrick);
                            ws.Cell(row, 14).Value = item.NgayThi.ToString("dd/MM/yyyy HH:mm");

                            row++;
                        }

                        var dataRange = ws.Range(5, 1, row - 1, headers.Length);
                        dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        ws.Columns().AdjustToContents();

                        // Sheet 2, 3, 4: 3 bảng thống kê theo Quý (Mục 10)
                        string nhanTieuChi = tinhDiemTB ? "Điểm trung bình" : "Số lượng bài thi";
                        ThemSheetThongKeQuy(wb, "ThongKe_Mon_Quy", $"THỐNG KÊ THEO MÔN HỌC & QUÝ ({namThongKe} - {nhanTieuChi})",
                            "Môn học", ThongKeTheoMonVaQuy(namThongKe, tinhDiemTB), tinhDiemTB);
                        ThemSheetThongKeQuy(wb, "ThongKe_Lop_Quy", $"THỐNG KÊ THEO LỚP SV & QUÝ ({namThongKe} - {nhanTieuChi})",
                            "Lớp sinh viên", ThongKeTheoLopVaQuy(namThongKe, tinhDiemTB), tinhDiemTB);
                        ThemSheetThongKeQuy(wb, "ThongKe_GV_Quy", $"THỐNG KÊ THEO GIẢNG VIÊN & QUÝ ({namThongKe} - {nhanTieuChi})",
                            "Giảng viên", ThongKeTheoGiangVienVaQuy(namThongKe, tinhDiemTB), tinhDiemTB);

                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Xuất file Excel (.xlsx) thành công (gồm 4 sheet)!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất file Excel: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void ThemSheetThongKeQuy(
            XLWorkbook wb,
            string tenSheet,
            string tieuDe,
            string tenCotDau,
            List<ThongKeTheoQuyDTO> ds,
            bool tinhDiemTB)
        {
            var ws = wb.Worksheets.Add(tenSheet);
            ws.Cell(1, 1).Value = tieuDe;
            ws.Range("A1:F1").Merge().Style
                .Font.SetBold(true)
                .Font.SetFontSize(12)
                .Font.SetFontColor(XLColor.DarkBlue)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            string[] headers = { tenCotDau, "Q1", "Q2", "Q3", "Q4", "TC" };
            for (int c = 0; c < headers.Length; c++)
                ws.Cell(3, c + 1).Value = headers[c];

            var hdr = ws.Range("A3:F3");
            hdr.Style.Font.SetBold(true);
            hdr.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#DBEAFE"));
            hdr.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            hdr.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            hdr.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            string fmt = tinhDiemTB ? "0.00" : "#,##0";
            int r = 4;
            for (int i = 0; i < ds.Count; i++)
            {
                var item = ds[i];
                ws.Cell(r, 1).Value = item.TenDoiTuong;
                ws.Cell(r, 2).Value = item.Q1;
                ws.Cell(r, 3).Value = item.Q2;
                ws.Cell(r, 4).Value = item.Q3;
                ws.Cell(r, 5).Value = item.Q4;
                ws.Cell(r, 6).Value = item.TC;

                ws.Range(r, 2, r, 6).Style.NumberFormat.Format = fmt;

                if (i == ds.Count - 1)
                {
                    var totalRow = ws.Range(r, 1, r, 6);
                    totalRow.Style.Font.SetBold(true);
                    totalRow.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#EFF6FF"));
                    totalRow.Style.Font.SetFontColor(XLColor.DarkBlue);
                }

                r++;
            }

            if (r > 4)
            {
                var body = ws.Range(4, 1, r - 1, 6);
                body.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                body.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }

            ws.Columns().AdjustToContents();
        }

        // ==================== MỤC 12: XUẤT PDF PHIẾU KẾT QUẢ BÀI THI (1 - N) ====================

        public void XuatPhieuBaiThiPdf(BaoCaoDiemThiDTO baiThi, List<ChiTietBaiLamDTO> dsChiTiet)
        {
            if (baiThi == null)
            {
                MessageBox.Show("Chưa chọn bài thi để xuất phiếu PDF!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF File (*.pdf)|*.pdf";
                sfd.FileName = $"PhieuKetQua_{baiThi.MaSV}_{baiThi.MaBT}.pdf";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                double diemDat = LayDiemDat();
                string xepLoai = baiThi.TongDiem >= diemDat ? "DAT" : "CHUA DAT";

                var lines = new List<string>
                {
                    "PHIEU KET QUA BAI THI TRAC NGHIEM (CHI TIET 1 - N)",
                    $"Ngay in phieu: {DateTime.Now:dd/MM/yyyy HH:mm}",
                    "==========================================================================",
                    $"Ma bai thi : {ChuanHoaAscii(baiThi.MaBT)}",
                    $"Sinh vien  : {ChuanHoaAscii(baiThi.MaSV)} - {ChuanHoaAscii(baiThi.HoTenSV)}   | Lop: {ChuanHoaAscii(baiThi.Lop)}",
                    $"Mon hoc    : {ChuanHoaAscii(baiThi.TenMon)}   | Ma de: {ChuanHoaAscii(baiThi.MaDe)}",
                    $"Ky thi     : {ChuanHoaAscii(baiThi.KyThi)} ({ChuanHoaAscii(baiThi.NamHoc)})   | Ngay thi: {baiThi.NgayThi:dd/MM/yyyy HH:mm}",
                    $"Ket qua    : Dung {baiThi.SoCauDung} / Sai {baiThi.SoCauSai} | Diem: {baiThi.TongDiem.ToString("0.00", CultureInfo.InvariantCulture)} | Xep loai: {xepLoai} (Moc dat >= {diemDat.ToString("0.##", CultureInfo.InvariantCulture)})",
                    "==========================================================================",
                    "STT | Ma CH    | Noi dung cau hoi (tom tat)            | Chon | Dung | KQ",
                    "--------------------------------------------------------------------------"
                };

                if (dsChiTiet == null || dsChiTiet.Count == 0)
                {
                    lines.Add("(Khong co du lieu chi tiet cau tra loi cho bai thi nay)");
                }
                else
                {
                    for (int i = 0; i < dsChiTiet.Count; i++)
                    {
                        var ct = dsChiTiet[i];
                        string stt = (i + 1).ToString().PadLeft(3);
                        string maCH = CatVaCanChuoi(ChuanHoaAscii(ct.MaCH), 8);
                        string noiDung = CatVaCanChuoi(ChuanHoaAscii(ct.NoiDungCauHoi).Replace("\r", " ").Replace("\n", " "), 37);
                        string chon = CatVaCanChuoi(string.IsNullOrWhiteSpace(ct.DapAnChon) ? "-" : ChuanHoaAscii(ct.DapAnChon), 4);
                        string dung = CatVaCanChuoi(ChuanHoaAscii(ct.DapAnDung), 4);
                        string kq = CatVaCanChuoi(ChuanHoaAscii(ct.KetQua), 4);

                        lines.Add($"{stt} | {maCH} | {noiDung} | {chon} | {dung} | {kq}");
                    }
                }

                lines.Add("--------------------------------------------------------------------------");
                lines.Add($"Tong cong: {(dsChiTiet?.Count ?? 0)} cau hoi | Tong diem: {baiThi.TongDiem.ToString("0.00", CultureInfo.InvariantCulture)} | Ket luan: {xepLoai}");

                string pdfContent = TaoNoiDungPdfNhieuTrang(lines, linesPerPage: 48);
                File.WriteAllText(sfd.FileName, pdfContent, Encoding.ASCII);

                MessageBox.Show("Xuất PDF Phiếu kết quả bài thi thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static string TaoNoiDungPdfNhieuTrang(List<string> allLines, int linesPerPage)
        {
            var pages = new List<List<string>>();
            for (int i = 0; i < allLines.Count; i += linesPerPage)
            {
                pages.Add(allLines.Skip(i).Take(linesPerPage).ToList());
            }
            if (pages.Count == 0) pages.Add(new List<string> { "" });

            var pdf = new StringBuilder();
            var offsets = new List<int> { 0 };

            void AppendObj(int id, string body)
            {
                offsets.Add(Encoding.ASCII.GetByteCount(pdf.ToString()));
                pdf.AppendLine($"{id} 0 obj");
                pdf.Append(body);
                if (!body.EndsWith("\n")) pdf.AppendLine();
                pdf.AppendLine("endobj");
            }

            var kids = new List<string>();
            for (int p = 0; p < pages.Count; p++)
            {
                kids.Add($"{4 + p * 2} 0 R");
            }

            pdf.AppendLine("%PDF-1.4");
            AppendObj(1, "<< /Type /Catalog /Pages 2 0 R >>");
            AppendObj(2, $"<< /Type /Pages /Kids [{string.Join(" ", kids)}] /Count {pages.Count} >>");
            AppendObj(3, "<< /Type /Font /Subtype /Type1 /BaseFont /Courier >>");

            for (int p = 0; p < pages.Count; p++)
            {
                int pageObjId = 4 + p * 2;
                int streamObjId = 5 + p * 2;

                var sb = new StringBuilder();
                sb.AppendLine("BT");
                sb.AppendLine("/F1 9.5 Tf");
                sb.AppendLine("36 760 Td");
                sb.AppendLine("14 TL");

                var pageLines = pages[p];
                for (int i = 0; i < pageLines.Count; i++)
                {
                    string safeLine = EscapePdfText(pageLines[i]);
                    sb.AppendLine(i == 0 ? $"({safeLine}) Tj" : $"T* ({safeLine}) Tj");
                }
                sb.AppendLine($"T* (--- Trang {p + 1}/{pages.Count} ---) Tj");
                sb.AppendLine("ET");

                string streamData = sb.ToString();
                int streamLength = Encoding.ASCII.GetByteCount(streamData);

                AppendObj(pageObjId,
                    $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Resources << /Font << /F1 3 0 R >> >> /Contents {streamObjId} 0 R >>");
                AppendObj(streamObjId,
                    $"<< /Length {streamLength} >>\nstream\n{streamData}endstream");
            }

            int xrefPos = Encoding.ASCII.GetByteCount(pdf.ToString());
            pdf.AppendLine("xref");
            pdf.AppendLine($"0 {offsets.Count}");
            pdf.AppendLine("0000000000 65535 f ");
            for (int i = 1; i < offsets.Count; i++)
            {
                pdf.AppendLine($"{offsets[i]:D10} 00000 n ");
            }

            pdf.AppendLine("trailer");
            pdf.AppendLine($"<< /Size {offsets.Count} /Root 1 0 R >>");
            pdf.AppendLine("startxref");
            pdf.AppendLine(xrefPos.ToString());
            pdf.Append("%%EOF");

            return pdf.ToString();
        }

        private static string ChuanHoaAscii(string? text)
        {
            string khongDau = BoDauTiengViet(text ?? "")
                .Replace('–', '-')
                .Replace('—', '-')
                .Replace('“', '"')
                .Replace('”', '"')
                .Replace('’', '\'');

            var sb = new StringBuilder(khongDau.Length);
            foreach (char c in khongDau)
            {
                sb.Append(c >= 32 && c <= 126 ? c : ' ');
            }
            return sb.ToString();
        }

        private static string CatVaCanChuoi(string text, int doDai)
        {
            string s = text ?? "";
            if (s.Length > doDai)
                return doDai > 3 ? s.Substring(0, doDai - 3) + "..." : s.Substring(0, doDai);
            return s.PadRight(doDai);
        }

        private string TaoNoiDungPdfBangDiem(List<BaoCaoDiemThiDTO> dsBaoCao)
        {
            var tongHop = TinhTongHopThongKe(dsBaoCao);
            var lines = new List<string>
            {
                "BAO CAO THONG KE DIEM THI",
                $"Ngay xuat: {DateTime.Now:dd/MM/yyyy HH:mm}",
                $"Tong bai: {tongHop.TongSoBai} | Dat: {tongHop.SoBaiDat} ({tongHop.TyLeDat:0.#}%) | Chua dat: {tongHop.SoBaiChuaDat} | Diem TB: {tongHop.DiemTrungBinh:0.00}",
                "------------------------------------------------------------------------",
                "Ma BT          | Ma SV    | Lop       | De Thi     | Dung/Sai | Diem",
                "------------------------------------------------------------------------"
            };

            foreach (var r in dsBaoCao)
            {
                string maBT = BoDauTiengViet(r.MaBT ?? "").PadRight(14).Substring(0, 14);
                string maSV = BoDauTiengViet(r.MaSV ?? "").PadRight(8).Substring(0, 8);
                string lop = BoDauTiengViet(r.Lop ?? "").PadRight(9).Substring(0, 9);
                string maDe = BoDauTiengViet(r.MaDe ?? "").PadRight(10).Substring(0, 10);
                string dungSai = $"{r.SoCauDung}/{r.SoCauSai}".PadRight(8);
                string diem = r.TongDiem.ToString("0.00", CultureInfo.InvariantCulture).PadLeft(4);

                lines.Add($"{maBT} | {maSV} | {lop} | {maDe} | {dungSai} | {diem}");
            }

            var streamBuilder = new StringBuilder();
            streamBuilder.AppendLine("BT");
            streamBuilder.AppendLine("/F1 10 Tf");
            streamBuilder.AppendLine("40 760 Td");
            streamBuilder.AppendLine("14 TL");

            for (int i = 0; i < lines.Count; i++)
            {
                string safeLine = EscapePdfText(lines[i]);
                streamBuilder.AppendLine(i == 0 ? $"({safeLine}) Tj" : $"T* ({safeLine}) Tj");
            }

            streamBuilder.AppendLine("ET");
            string streamData = streamBuilder.ToString();
            int streamLength = Encoding.ASCII.GetByteCount(streamData);

            var pdf = new StringBuilder();
            var offsets = new List<int> { 0 };

            void AppendObj(int id, string body)
            {
                offsets.Add(Encoding.ASCII.GetByteCount(pdf.ToString()));
                pdf.AppendLine($"{id} 0 obj");
                pdf.Append(body);
                if (!body.EndsWith("\n")) pdf.AppendLine();
                pdf.AppendLine("endobj");
            }

            pdf.AppendLine("%PDF-1.4");
            AppendObj(1, "<< /Type /Catalog /Pages 2 0 R >>");
            AppendObj(2, "<< /Type /Pages /Kids [3 0 R] /Count 1 >>");
            AppendObj(3, "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>");
            AppendObj(4, "<< /Type /Font /Subtype /Type1 /BaseFont /Courier >>");
            AppendObj(5, $"<< /Length {streamLength} >>\nstream\n{streamData}endstream");

            int xrefPos = Encoding.ASCII.GetByteCount(pdf.ToString());
            pdf.AppendLine("xref");
            pdf.AppendLine($"0 {offsets.Count}");
            pdf.AppendLine("0000000000 65535 f ");
            for (int i = 1; i < offsets.Count; i++)
            {
                pdf.AppendLine($"{offsets[i]:D10} 00000 n ");
            }

            pdf.AppendLine("trailer");
            pdf.AppendLine($"<< /Size {offsets.Count} /Root 1 0 R >>");
            pdf.AppendLine("startxref");
            pdf.AppendLine(xrefPos.ToString());
            pdf.Append("%%EOF");

            return pdf.ToString();
        }

        private static string EscapePdfText(string text)
        {
            return (text ?? "")
                .Replace("\\", "\\\\")
                .Replace("(", "\\(")
                .Replace(")", "\\)");
        }

        private static string BoDauTiengViet(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            string normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(normalized.Length);
            foreach (char c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString()
                .Normalize(NormalizationForm.FormC)
                .Replace('đ', 'd')
                .Replace('Đ', 'D');
        }
    }
}
