using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using QL_Thi_Trac_Nghiem.DAO;
using QL_Thi_Trac_Nghiem.DTO;

namespace QL_Thi_Trac_Nghiem.BUS
{
    public class CauHoiBUS
    {
        private const int MaCHToiDa = 20;
        private const int MaMonToiDa = 20;
        private const int MaGVToiDa = 20;
        private const int NoiDungToiDa = 1000;
        private const int DapAnToiDa = 500;

        public const string MaGVAdmin = "ADMIN";

        public static readonly string[] MucDoHopLe = { "Cơ bản", "Vận dụng", "Nâng cao" };

        private readonly CauHoiDAO cauHoiDAO = new CauHoiDAO();

        // Bộ nhớ đệm ArrayList phục vụ tìm kiếm/lọc trong tầng BUS
        public static ArrayList arrCauHoi = new ArrayList();

        // ================= ĐỌC DỮ LIỆU =================
        public List<CauHoiDTO> DocDSCauHoi()
        {
            try
            {
                List<CauHoiDTO> list = cauHoiDAO.GetCauHoi();
                DongBoCache(list);
                return list;
            }
            catch (Exception)
            {
                return new List<CauHoiDTO>();
            }
        }

        // ================= LỌC DỮ LIỆU =================
        public List<CauHoiDTO> LocTheoMon(string maMon)
        {
            try
            {
                List<CauHoiDTO> list = cauHoiDAO.GetCauHoi(maMon, null, null);
                DongBoCache(list);
                return list;
            }
            catch (Exception) { return new List<CauHoiDTO>(); }
        }

        public List<CauHoiDTO> LocTheoGiaoVien(string maGV)
        {
            try
            {
                List<CauHoiDTO> list;
                if (string.IsNullOrWhiteSpace(maGV)) list = cauHoiDAO.GetCauHoi();
                else if (LaAdmin(maGV)) list = cauHoiDAO.GetCauHoiChuaGanGiaoVien();
                else list = cauHoiDAO.GetCauHoi(null, maGV, null);

                DongBoCache(list);
                return list;
            }
            catch (Exception)
            {
                return new List<CauHoiDTO>();
            }
        }

        public List<CauHoiDTO> LocTheoMucDo(string mucDo, string? maMon = null)
        {
            if (!LaMucDoHopLe(mucDo)) return new List<CauHoiDTO>();

            try
            {
                List<CauHoiDTO> list = cauHoiDAO.GetCauHoi(maMon, null, mucDo);
                DongBoCache(list);
                return list;
            }
            catch (Exception) { return new List<CauHoiDTO>(); }
        }

        public List<CauHoiDTO> Loc(string? maMon, string? maGV, string? mucDo = null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mucDo) && !LaMucDoHopLe(mucDo))
                    return new List<CauHoiDTO>();

                if (LaAdmin(maGV))
                {
                    List<CauHoiDTO> ketQua = new List<CauHoiDTO>();
                    foreach (CauHoiDTO c in cauHoiDAO.GetCauHoiChuaGanGiaoVien())
                    {
                        if (!string.IsNullOrWhiteSpace(maMon) && c.MaMon != maMon.Trim()) continue;
                        if (!string.IsNullOrWhiteSpace(mucDo) && c.MucDo != mucDo.Trim()) continue;
                        ketQua.Add(c);
                    }
                    DongBoCache(ketQua);
                    return ketQua;
                }

                List<CauHoiDTO> list = cauHoiDAO.GetCauHoi(maMon, maGV, mucDo);
                DongBoCache(list);
                return list;
            }
            catch (Exception)
            {
                return new List<CauHoiDTO>();
            }
        }

        // ================= TÌM KIẾM TRÊN ARRAYLIST =================
        // Tìm thuần trên bộ nhớ đệm, không truy vấn CSDL.
        // tuKhoa rỗng => trả toàn bộ cache hiện tại (giữ nguyên phạm vi đang xem).
        public List<CauHoiDTO> TimKiemCauHoi(string tuKhoa)
        {
            string kw = (tuKhoa ?? "").Trim();
            List<CauHoiDTO> ketQua = new List<CauHoiDTO>();

            foreach (object item in arrCauHoi)
            {
                if (item is CauHoiDTO c)
                {
                    if (kw.Length == 0
                        || Chua(kw, c.MaCH)
                        || Chua(kw, c.TenMon)
                        || Chua(kw, c.NoiDung)
                        || Chua(kw, c.MaMon)
                        || Chua(kw, c.MucDo))
                    {
                        ketQua.Add(c);
                    }
                }
            }

            return ketQua;
        }

        private static bool Chua(string tuKhoa, string? giaTri)
        {
            return (giaTri ?? "").IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void DongBoCache(List<CauHoiDTO> ds)
        {
            arrCauHoi = new ArrayList(ds);
        }

        // ================= THỐNG KÊ =================
        public Dictionary<string, int> ThongKeTheoMucDo(string? maMon = null)
        {
            Dictionary<string, int> ketQua = TaoThongKeRong();

            try
            {
                Dictionary<string, int> thongKe = cauHoiDAO.GetThongKeMucDo(maMon);
                foreach (KeyValuePair<string, int> kv in thongKe)
                {
                    ketQua[kv.Key] = kv.Value;
                }
            }
            catch (Exception)
            {
                // Mất kết nối thì trả về 3 mức độ với giá trị 0
            }

            return ketQua;
        }

        // ================= NGHIỆP VỤ THÊM =================
        public bool ThemCauHoi(CauHoiDTO c, out string thongBao)
        {
            if (!ChuanHoaVaKiemTra(c, out thongBao)) return false;

            try
            {
                if (cauHoiDAO.KiemTraTrungMaCauHoi(c.MaCH))
                {
                    thongBao = $"Mã câu hỏi '{c.MaCH}' đã tồn tại trong hệ thống!";
                    return false;
                }

                bool kq = cauHoiDAO.InsertCauHoi(c);
                thongBao = kq ? "Thêm câu hỏi thành công!" : "Thêm câu hỏi thất bại!";
                if (kq) arrCauHoi.Clear();
                return kq;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                thongBao = "Mã môn hoặc mã giảng viên không tồn tại. Vui lòng kiểm tra lại!";
                return false;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                thongBao = "Mã câu hỏi đã tồn tại trong hệ thống!";
                return false;
            }
            catch (SqlException ex) when (ex.Number == 8152 || ex.Number == 2628)
            {
                thongBao = "Dữ liệu nhập vào vượt quá độ dài cho phép của CSDL!";
                return false;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                return false;
            }
        }

        // ================= NGHIỆP VỤ CẬP NHẬT =================
        public bool CapNhatCauHoi(CauHoiDTO c, out string thongBao)
        {
            if (!ChuanHoaVaKiemTra(c, out thongBao)) return false;

            try
            {
                bool kq = cauHoiDAO.UpdateCauHoi(c);
                thongBao = kq
                    ? "Cập nhật câu hỏi thành công!"
                    : "Không tìm thấy câu hỏi có mã tương ứng!";
                if (kq) arrCauHoi.Clear();
                return kq;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                thongBao = "Mã môn hoặc mã giảng viên không tồn tại. Vui lòng kiểm tra lại!";
                return false;
            }
            catch (SqlException ex) when (ex.Number == 8152 || ex.Number == 2628)
            {
                thongBao = "Dữ liệu nhập vào vượt quá độ dài cho phép của CSDL!";
                return false;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                return false;
            }
        }

        // ================= NGHIỆP VỤ XÓA =================
        public bool XoaCauHoi(string maCh, out string thongBao)
        {
            if (string.IsNullOrWhiteSpace(maCh))
            {
                thongBao = "Vui lòng chọn câu hỏi cần xóa!";
                return false;
            }

            maCh = maCh.Trim();

            try
            {
                bool kq = cauHoiDAO.DeleteCauHoi(maCh);
                thongBao = kq
                    ? "Xóa câu hỏi thành công!"
                    : "Không tìm thấy câu hỏi có mã tương ứng!";
                if (kq) arrCauHoi.Clear();
                return kq;
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                thongBao = "Câu hỏi đang được sử dụng, không thể xóa!";
                return false;
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                return false;
            }
        }

        // ================= KIỂM TRA DỮ LIỆU ĐẦU VÀO =================
        private bool ChuanHoaVaKiemTra(CauHoiDTO c, out string thongBao)
        {
            if (!ChuanHoaVaKiemTraCoBan(c, out thongBao)) return false;

            // Admin không phải giảng viên: lưu Ma_GV = NULL và bỏ qua kiểm tra tồn tại
            if (LaAdmin(c.MaGV))
            {
                c.MaGV = "";
            }

            try
            {
                if (!string.IsNullOrEmpty(c.MaMon) && !cauHoiDAO.KiemTraTonTaiMon(c.MaMon))
                {
                    thongBao = $"Mã môn '{c.MaMon}' không tồn tại trong hệ thống!";
                    return false;
                }

                if (!string.IsNullOrEmpty(c.MaGV) && !cauHoiDAO.KiemTraTonTaiGiangVien(c.MaGV))
                {
                    thongBao = $"Mã giảng viên '{c.MaGV}' không tồn tại trong hệ thống!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối hoặc thực thi CSDL: " + ex.Message;
                return false;
            }

            thongBao = "";
            return true;
        }

        // Phần kiểm tra thuần (không truy vấn CSDL), dùng chung cho thêm/sửa và nhập từ Excel
        private bool ChuanHoaVaKiemTraCoBan(CauHoiDTO c, out string thongBao)
        {
            if (c == null)
            {
                thongBao = "Dữ liệu câu hỏi không hợp lệ!";
                return false;
            }

            c.MaCH = (c.MaCH ?? "").Trim();
            c.MaMon = (c.MaMon ?? "").Trim();
            c.MaGV = (c.MaGV ?? "").Trim();
            c.NoiDung = (c.NoiDung ?? "").Trim();
            c.DapAnA = (c.DapAnA ?? "").Trim();
            c.DapAnB = (c.DapAnB ?? "").Trim();
            c.DapAnC = (c.DapAnC ?? "").Trim();
            c.DapAnD = (c.DapAnD ?? "").Trim();
            c.DapAnDung = (c.DapAnDung ?? "").Trim().ToUpper();
            c.MucDo = (c.MucDo ?? "").Trim();

            if (string.IsNullOrWhiteSpace(c.MaCH) || string.IsNullOrWhiteSpace(c.MaMon)
                || string.IsNullOrWhiteSpace(c.NoiDung)
                || string.IsNullOrWhiteSpace(c.DapAnA) || string.IsNullOrWhiteSpace(c.DapAnB)
                || string.IsNullOrWhiteSpace(c.DapAnC) || string.IsNullOrWhiteSpace(c.DapAnD))
            {
                thongBao = "Mã câu hỏi, Môn, Nội dung và 4 đáp án không được để trống!";
                return false;
            }

            if (c.MaCH.Length > MaCHToiDa)
            {
                thongBao = $"Mã câu hỏi không được vượt quá {MaCHToiDa} ký tự!";
                return false;
            }

            if (c.MaCH.Contains(" "))
            {
                thongBao = "Mã câu hỏi không được chứa khoảng trắng!";
                return false;
            }

            if (c.MaMon.Length > MaMonToiDa)
            {
                thongBao = $"Mã môn không được vượt quá {MaMonToiDa} ký tự!";
                return false;
            }

            if (c.MaGV.Length > MaGVToiDa)
            {
                thongBao = $"Mã giảng viên không được vượt quá {MaGVToiDa} ký tự!";
                return false;
            }

            if (c.NoiDung.Length > NoiDungToiDa)
            {
                thongBao = $"Nội dung câu hỏi không được vượt quá {NoiDungToiDa} ký tự!";
                return false;
            }

            if (c.DapAnA.Length > DapAnToiDa || c.DapAnB.Length > DapAnToiDa
                || c.DapAnC.Length > DapAnToiDa || c.DapAnD.Length > DapAnToiDa)
            {
                thongBao = $"Mỗi đáp án không được vượt quá {DapAnToiDa} ký tự!";
                return false;
            }

            if (c.DapAnDung != "A" && c.DapAnDung != "B" && c.DapAnDung != "C" && c.DapAnDung != "D")
            {
                thongBao = "Đáp án đúng phải là A, B, C hoặc D!";
                return false;
            }

            if (!LaMucDoHopLe(c.MucDo))
            {
                thongBao = "Mức độ chỉ được là 'Cơ bản', 'Vận dụng' hoặc 'Nâng cao'!";
                return false;
            }

            thongBao = "";
            return true;
        }

        // ================= NHẬP / XUẤT FILE =================
        // Nhập hàng loạt câu hỏi từ Excel. Dòng trùng mã/lỗi được bỏ qua và báo cáo lại.
        public bool NhapTuExcel(string duongDan, string maGVDangNhap, string maMonMacDinh,
                                 out List<KetQuaImportDTO> ketQua, out string thongBao)
        {
            ketQua = new List<KetQuaImportDTO>();

            List<(int Dong, CauHoiDTO CauHoi)> danhSachTho = ExcelCauHoiCodec.DocFile(duongDan, out thongBao);
            if (danhSachTho.Count == 0) return false;

            HashSet<string> tapMaDaCo;
            HashSet<string> tapMaMon;
            Dictionary<string, string> tenMonTheoMa;
            try
            {
                tapMaDaCo = cauHoiDAO.GetTatCaMaCauHoi();
                tapMaMon = cauHoiDAO.GetTatCaMaMon();
                tenMonTheoMa = cauHoiDAO.GetTenMonTheoMaMon();
            }
            catch (Exception ex)
            {
                thongBao = "Lỗi kết nối CSDL: " + ex.Message;
                return false;
            }

            int soThanhCong = 0;
            int soDong = danhSachTho.Count;

            for (int i = 0; i < soDong; i++)
            {
                int dongExcel = danhSachTho[i].Dong;
                CauHoiDTO c = danhSachTho[i].CauHoi;

                if (string.IsNullOrWhiteSpace(c.MaMon)) c.MaMon = (maMonMacDinh ?? "").Trim();
                c.MaMon = QuyDoiTenMonSangMa(c.MaMon, tenMonTheoMa, tapMaMon);
                c.MaGV = (maGVDangNhap ?? "").Trim();
                if (LaAdmin(c.MaGV)) c.MaGV = "";
                c.MucDo = ChuanHoaMucDo(c.MucDo);

                if (!ChuanHoaVaKiemTraCoBan(c, out string loiDong))
                {
                    ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false, loiDong));
                    continue;
                }

                if (!tapMaMon.Contains(c.MaMon))
                {
                    ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false,
                        $"Mã môn '{c.MaMon}' không tồn tại trong hệ thống"));
                    continue;
                }

                if (tapMaDaCo.Contains(c.MaCH))
                {
                    ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false,
                        $"Mã câu hỏi '{c.MaCH}' đã tồn tại"));
                    continue;
                }

                try
                {
                    if (cauHoiDAO.InsertCauHoi(c))
                    {
                        tapMaDaCo.Add(c.MaCH);
                        soThanhCong++;
                        ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, true, ""));
                    }
                    else
                    {
                        ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false, "Thêm thất bại"));
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false,
                        "Mã môn hoặc mã giảng viên không tồn tại"));
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false,
                        $"Mã câu hỏi '{c.MaCH}' đã tồn tại"));
                }
                catch (SqlException ex) when (ex.Number == 8152 || ex.Number == 2628)
                {
                    ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false,
                        "Dữ liệu vượt quá độ dài cho phép của CSDL"));
                }
                catch (Exception ex)
                {
                    ketQua.Add(TaoKetQuaImport(dongExcel, c.MaCH, false, ex.Message));
                }
            }

            if (soThanhCong > 0) arrCauHoi.Clear();

            thongBao = $"Nhập thành công {soThanhCong}/{soDong} dòng.";
            return soThanhCong > 0;
        }

        // Ô môn trong file có thể là mã môn (CSDL) hoặc tên môn (Cơ sở dữ liệu).
        // Nếu là mã hợp lệ thì giữ nguyên; nếu là tên thì tra sang mã; không khớp gì thì trả về nguyên văn để báo lỗi.
        private static string QuyDoiTenMonSangMa(string? giaTri, Dictionary<string, string> tenMonTheoMa,
                                                  HashSet<string> tapMaMon)
        {
            string giaTriChuan = (giaTri ?? "").Trim();
            if (string.IsNullOrEmpty(giaTriChuan)) return "";
            if (tapMaMon.Contains(giaTriChuan)) return giaTriChuan;

            foreach (KeyValuePair<string, string> kv in tenMonTheoMa)
            {
                if (string.Equals(kv.Key, giaTriChuan, StringComparison.OrdinalIgnoreCase))
                {
                    return kv.Value;
                }
            }

            return giaTriChuan;
        }

        private static KetQuaImportDTO TaoKetQuaImport(int dong, string maCH, bool thanhCong, string thongBao)
        {
            return new KetQuaImportDTO
            {
                Dong = dong,
                MaCH = (maCH ?? "").Trim(),
                ThanhCong = thanhCong,
                ThongBao = thongBao
            };
        }

        // Chuẩn hóa mức độ nhập tay về đúng 3 giá trị hợp lệ của CSDL
        private static string ChuanHoaMucDo(string? mucDo)
        {
            if (string.IsNullOrWhiteSpace(mucDo)) return "Cơ bản";

            string giaTri = ChuanHoaKhongDau(mucDo);

            if (giaTri == "COBAN" || giaTri == "DE") return "Cơ bản";
            if (giaTri == "VANDUNG" || giaTri == "TRUNGBINH") return "Vận dụng";
            if (giaTri == "NANGCAO" || giaTri == "KHO") return "Nâng cao";

            return mucDo.Trim();
        }

        private static string ChuanHoaKhongDau(string giaTri)
        {
            string khongDau = giaTri.Trim().Normalize(System.Text.NormalizationForm.FormD);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (char ch in khongDau)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch)
                    != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            string s = sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
            return s.Replace("đ", "d").Replace("Đ", "D")
                    .Replace(" ", "").ToUpperInvariant();
        }

        private static bool LaAdmin(string? maGV)
        {
            return !string.IsNullOrWhiteSpace(maGV)
                && maGV.Trim().Equals(MaGVAdmin, StringComparison.OrdinalIgnoreCase);
        }

        private static bool LaMucDoHopLe(string? mucDo)
        {
            if (string.IsNullOrWhiteSpace(mucDo)) return false;

            string giaTri = mucDo.Trim();
            foreach (string hopLe in MucDoHopLe)
            {
                if (hopLe == giaTri) return true;
            }
            return false;
        }

        private static Dictionary<string, int> TaoThongKeRong()
        {
            Dictionary<string, int> ketQua = new Dictionary<string, int>();
            foreach (string mucDo in MucDoHopLe)
            {
                ketQua[mucDo] = 0;
            }
            return ketQua;
        }
    }
}
